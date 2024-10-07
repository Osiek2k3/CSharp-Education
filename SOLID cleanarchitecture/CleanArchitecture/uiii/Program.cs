using Microsoft.AspNetCore.Components.Authorization;
using System.Reflection;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using uiii.Contracts;
using uiii.Providers;
using uiii.Services.Base;
using uiii.Services;
using uiii;
using Blazored.LocalStorage;
using uiii.Handlers;

public class Program
{
    private static async Task Main(string[] args)
    {
        var builder = WebAssemblyHostBuilder.CreateDefault(args);
        builder.RootComponents.Add<App>("#app");
        builder.RootComponents.Add<HeadOutlet>("head::after");

        //builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

        builder.Services.AddTransient<JwtAuthorizationMessageHandler>();
        builder.Services.AddHttpClient<IClient, Client>(client => client.BaseAddress = new Uri("https://localhost:7082"))
            .AddHttpMessageHandler<JwtAuthorizationMessageHandler>();

     //   builder.Services.blazoredToast;
        builder.Services.AddBlazoredLocalStorage();
        builder.Services.AddAuthorizationCore();
        //builder.Services.AddScoped<ApiAuthenticationStateProvider>();
        builder.Services.AddScoped<AuthenticationStateProvider, ApiAuthenticationStateProvider>();

        builder.Services.AddScoped<ILeaveTypeService, LeaveTypeService>();
        builder.Services.AddScoped<ILeaveAllocationService, LeaveAllocationService>();
        builder.Services.AddScoped<ILeaveRequestService, LeaveRequestService>();
        builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();

        builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());

        await builder.Build().RunAsync();
    }
}