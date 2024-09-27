using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repositories;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly ITokenRepsitory tokenRepsitory;

        public AuthController(UserManager<IdentityUser> userManager,ITokenRepsitory tokenRepsitory) 
        {
            this.userManager = userManager;
            this.tokenRepsitory = tokenRepsitory;
        }
        
        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequestDto registerRequestDto)
        {
            var indentityUser = new IdentityUser
            {
                UserName = registerRequestDto.UserName,
                Email = registerRequestDto.UserName
            };
            var identityResult = await userManager.CreateAsync(indentityUser, registerRequestDto.Password);

            if(identityResult.Succeeded)
            {
                if(registerRequestDto.Roles.Any() && registerRequestDto.Roles !=null)
                {
                    identityResult = await userManager.AddToRolesAsync(indentityUser, registerRequestDto.Roles);

                    if(identityResult.Succeeded)
                    {
                        return Ok("User was registered, Please login");
                    }
                }
            }
            return BadRequest("Somthing went wrong");
        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto loginRequestDto)
        {
            var user = await userManager.FindByEmailAsync(loginRequestDto.UserName);

            if(user != null)
            {
                var checkPasswordResult = await userManager
                    .CheckPasswordAsync(user,loginRequestDto.Password);
                if (checkPasswordResult)
                {
                    var roles = await userManager.GetRolesAsync(user);
                    if (roles != null)
                    {
                        var jwtToken = tokenRepsitory.CreateJWTToken(user, roles.ToList());

                        var response = new LoginResponseDto
                        {
                            JwtToken = jwtToken,
                        };
                        return Ok(jwtToken);
                    }

                }
            }
            return BadRequest("Username or Password incorrect");
        }
    }
}
