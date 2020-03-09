using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using TeacherBackend.Data;
using TeacherBackend.Model;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace TeacherBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : Controller
    {
        private readonly IConfiguration _config;
        private readonly IUnitOfWork _unitOfWork;

        // GET
        public LoginController(IConfiguration config, IUnitOfWork unitOfWork)
        {
            _config = config;
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public IActionResult Login(string name, string password)
        {
            var login = new UserModel();
            login.UserName = name;
            login.Password = password;
            IActionResult result = Unauthorized();

            var user = AuthenticatedUser(login);

            if (user != null)
            {
                var jwtToken = GenerateJwtToken(user);
                result = Ok((new {token = jwtToken}));
            }

            return result;
        }

        [HttpGet("fake")]
        public async Task<List<UserModel>> FakeUser()
        {
            var user1 = new UserModel() {UserName = "Tom", Age = 33};
            var user2 = new UserModel() {UserName = "Jerry", Age = 31};

            _unitOfWork.UserModelRepository.Create(user1);
            _unitOfWork.UserModelRepository.Create(user2);
            
            _unitOfWork.Save();

            var users = await _unitOfWork.UserModelRepository.GetAll().ToListAsync();
            foreach (var u in users)
            {
                Console.WriteLine($"{u.Id}.{u.UserName} - {u.Age}");
            }

            return users;
        }

        private string GenerateJwtToken(UserModel user)
        {
            string str = _config["Jwt:Key"];
            string issuer = _config["Jwt:Issuer"];
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(str));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var token = new JwtSecurityToken(issuer: issuer, audience: issuer, claims,
                expires: DateTime.Now.AddMinutes(120),
                signingCredentials: credentials);

            var encodeToken = new JwtSecurityTokenHandler().WriteToken(token);
            return encodeToken;
        }

        private UserModel AuthenticatedUser(UserModel login)
        {
            UserModel user = null;
            if (login.UserName == "admin" && login.Password == "admin")
            {
                user = new UserModel {UserName = "Admin", Email = "admin@gmail.com", Password = "admin"};
            }

            return user;
        }

        [Authorize]
        [HttpPost("Post")]
        public string Post()
        {
            if (!(HttpContext.User.Identity is ClaimsIdentity identity))
                return "Error";
            var claims = identity.Claims.ToList();
            var userName = claims[0].Value;
            return "Welcome to: " + userName;
        }

        [Authorize]
        [HttpGet("GetValue")]
        public string[] Get()
        {
            return new[] {"Value1", "Value2", "Value3"};
        }
    }
}