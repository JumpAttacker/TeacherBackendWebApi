using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using TeacherBackend.Model;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace TeacherBackend.Services
{
    public class UserService : IUserService
    {
        private readonly IConfiguration _config;

        public UserService(IConfiguration config)
        {
            _config = config;
        }

        public string GenerateJwtToken(UserModel user)
        {
            var str = _config["Jwt:Key"];
            var issuer = _config["Jwt:Issuer"];
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(str));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            // var claims = new[]
            // {
            //     new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
            //     new Claim(JwtRegisteredClaimNames.Email, user.Email),
            //     new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            // };

            var identity = GetIdentity(user);
            
            var token = new JwtSecurityToken(issuer, issuer, identity.Claims,
                expires: DateTime.Now.AddDays(2),
                notBefore: DateTime.UtcNow,
                signingCredentials: credentials);

            var encodeToken = new JwtSecurityTokenHandler().WriteToken(token);

            
            return encodeToken;
        }
        
        private ClaimsIdentity GetIdentity(UserModel person)
        {
            if (person == null) return null;
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, person.Login),
                new Claim(ClaimsIdentity.DefaultRoleClaimType, person.Role),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };
            var claimsIdentity =
                new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType,
                    ClaimsIdentity.DefaultRoleClaimType);
            return claimsIdentity;
        }
    }
}