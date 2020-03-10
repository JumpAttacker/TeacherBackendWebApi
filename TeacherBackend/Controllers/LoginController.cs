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
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using TeacherBackend.Data;
using TeacherBackend.Model;
using TeacherBackend.Services;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace TeacherBackend.Controllers
{
    public static class Role
    {
        public const string Admin = "Admin";
        public const string User = "User";
    }
    
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : Controller
    {
        private readonly ILogger<LoginController> _logger;
        private readonly IConfiguration _config;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserService _userService;

        // GET
        public LoginController(ILogger<LoginController> logger,IConfiguration config, IUnitOfWork unitOfWork, IUserService userService)
        {
            _logger = logger;
            _config = config;
            _unitOfWork = unitOfWork;
            _userService = userService;
        }

        [HttpGet]
        public IActionResult Login(string login, string password)
        {
            var authenticateModel = new AuthenticateModel {Username = login, Password = password};
            IActionResult result = Unauthorized();

            var user = AuthenticatedUser(authenticateModel);

            if (user == null) return result;
            
            var jwtToken = _userService.GenerateJwtToken(user);
            result = Ok(new {token = jwtToken});

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

/*
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
*/

        private UserModel AuthenticatedUser(AuthenticateModel login)
        {
            UserModel user = null;
            if (login.Username == "admin" && login.Password == "admin")
            {
                user = new UserModel {UserName = "Admin", Email = "admin@gmail.com", Password = "admin"};
            }
//TODO: get user from db
            return user;
        }

        [Authorize]
        [HttpPost("Post")]
        public string Post()
        {
            if (!(HttpContext.User.Identity is ClaimsIdentity identity))
                return "Error";
            var claims = identity.Claims.ToList();
            foreach (var claim in claims)
            {
                Console.WriteLine(claim.Value);
                _logger.LogDebug(claim.Value);
            }

            var user = User;
            var userName = claims[0].Value;
            return "Welcome to: " + userName+" "+User.Identity.Name;;
        }

        [Authorize]
        [HttpGet("GetValue")]
        public string[] Get()
        {
            return new[] {"Value1", "Value2", "Value3"};
        }
    }
}