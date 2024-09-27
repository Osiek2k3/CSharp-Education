﻿using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace NZWalks.API.Repositories
{
    public class TokenRepository : ITokenRepsitory
    {
        private readonly IConfiguration configuration;
        public TokenRepository(IConfiguration configuration) {
            this.configuration = configuration;
            
        }
        public string CreateJWTToken(IdentityUser user, List<string> roles)
        {
            var clamis = new List<Claim>();

            clamis.Add(new Claim(ClaimTypes.Email, user.Email));

            foreach (var role in roles)
            {
                clamis.Add(new Claim(ClaimTypes.Role, role));
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]));

            var credentials = new SigningCredentials(key,SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                configuration["Jwt:Issuer"],
                configuration["Jwt:Audience"],
                clamis,
                expires: DateTime.Now.AddMinutes(15),
                signingCredentials : credentials
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
