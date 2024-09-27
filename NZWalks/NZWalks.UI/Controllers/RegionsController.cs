using Microsoft.AspNetCore.Mvc;
using NZWalks.UI.Models;
using NZWalks.UI.Models.Dto;
using System.Net.Http.Headers;
using System.Reflection;
using System.Text;
using System.Text.Json;

namespace NZWalks.UI.Controllers
{
    public class RegionsController : Controller
    {
        private readonly IHttpClientFactory httpClientFactory;

        public RegionsController(IHttpClientFactory httpClientFactory)
        {
            this.httpClientFactory = httpClientFactory;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginRequestDto loginRequestDto)
        {
            var client = httpClientFactory.CreateClient();

            var httpRequestMessage = new HttpRequestMessage()
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri("https://localhost:7215/api/auth/login"),
                Content = new StringContent(JsonSerializer.Serialize(loginRequestDto), Encoding.UTF8, "application/json")
            };

            var httpResponseMessage = await client.SendAsync(httpRequestMessage);

            if (httpResponseMessage.IsSuccessStatusCode)
            {
                var responseString = await httpResponseMessage.Content.ReadAsStringAsync();

                HttpContext.Response.Cookies.Append("AuthToken", responseString, new CookieOptions
                {
                    HttpOnly = true,
                    Secure = true,
                    Expires = DateTime.UtcNow.AddDays(1)
                });

                return RedirectToAction("Index", "Regions");
            }

            ModelState.AddModelError(string.Empty, "Invalid login attempt.");
            return View(loginRequestDto);
        }



        private void AddAuthTokenToClient(HttpClient client)
        {
            if (HttpContext.Request.Cookies.TryGetValue("AuthToken", out var token))
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }
        }

        [HttpPost]
        public IActionResult Logout()
        {
            // Usunięcie tokenu z plików cookie
            HttpContext.Response.Cookies.Delete("AuthToken");

            // Możesz także przekierować użytkownika do strony logowania lub innej odpowiedniej strony
            return RedirectToAction("Login");
        }


        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<RegionDto> response = new List<RegionDto>();

            try
            {
                var client = httpClientFactory.CreateClient();
                AddAuthTokenToClient(client);

                var httpResponseMessage = await client.GetAsync("https://localhost:7215/api/regions");

                httpResponseMessage.EnsureSuccessStatusCode();

                response.AddRange(await httpResponseMessage.Content.ReadFromJsonAsync<IEnumerable<RegionDto>>());
            }
            catch (Exception ex)
            {

            }

            return View(response);
        }


        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Add(AddRegionViewModel model)
        {
            var client = httpClientFactory.CreateClient();

            var httpRequestMessage = new HttpRequestMessage()
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri("https://localhost:7215/api/regions"),
                Content = new StringContent(JsonSerializer.Serialize(model), Encoding.UTF8, "application/json")
            };

            var httpResponseMessage = await client.SendAsync(httpRequestMessage);
            httpResponseMessage.EnsureSuccessStatusCode();

            var respose = await httpResponseMessage.Content.ReadFromJsonAsync<RegionDto>();

            if (respose is not null)
            {
                return RedirectToAction("Index", "Regions");
            }

            return View();
        }


        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var client = httpClientFactory.CreateClient();

            var response = await client.GetFromJsonAsync<RegionDto>($"https://localhost:7215/api/regions/{id.ToString()}");

            if (response is not null)
            {
                return View(response);
            }

            return View(null);
        }


        [HttpPost]
        public async Task<IActionResult> Edit(RegionDto request)
        {
            var client = httpClientFactory.CreateClient();

            var httpRequestMessage = new HttpRequestMessage()
            {
                Method = HttpMethod.Put,
                RequestUri = new Uri($"https://localhost:7215/api/regions/{request.Id}"),
                Content = new StringContent(JsonSerializer.Serialize(request), Encoding.UTF8, "application/json")
            };

            var httpResponseMessage = await client.SendAsync(httpRequestMessage);
            httpResponseMessage.EnsureSuccessStatusCode();

            var respose = await httpResponseMessage.Content.ReadFromJsonAsync<RegionDto>();

            if (respose is not null)
            {
                return RedirectToAction("Index", "Regions");
            }

            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Delete(RegionDto request)
        {
            try
            {
                var client = httpClientFactory.CreateClient();

                var httpResponseMessage = await client.DeleteAsync($"https://localhost:7215/api/regions/{request.Id}");

                httpResponseMessage.EnsureSuccessStatusCode();

                return RedirectToAction("Index", "Regions");
            }
            catch (Exception ex)
            {
            }

            return View("Edit");
        }
    }
}
