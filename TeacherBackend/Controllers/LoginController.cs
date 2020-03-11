using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using TeacherBackend.Data;
using TeacherBackend.Model;
using TeacherBackend.Services;

namespace TeacherBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : Controller
    {
        private readonly IConfiguration _config;
        private readonly ILogger<LoginController> _logger;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserService _userService;

        private readonly List<UserModel> _users = new List<UserModel>
        {
            new UserModel {Login = "admin", Password = "admin", Role = "admin"},
            new UserModel {Login = "test", Password = "test", Role = "user"}
        };

        // GET
        public LoginController(ILogger<LoginController> logger, IConfiguration config, IUnitOfWork unitOfWork,
            IUserService userService)
        {
            _logger = logger;
            _config = config;
            _unitOfWork = unitOfWork;
            _userService = userService;
        }

        [HttpGet]
        public IActionResult Login(string loginOrMail, string password)
        {
            var authenticateModel = new AuthenticateModel {LoginOrMail = loginOrMail, Password = password};

            var user = AuthenticatedUser(authenticateModel);

            if (user == null) return Ok(new {Error = "Incorrect login or password"});

            var jwtToken = _userService.GenerateJwtToken(user);

            return Ok(
                new
                {
                    user.Login,
                    Token = jwtToken
                }
            );
        }

        [HttpGet("fake")]
        public async Task<List<UserModel>> FakeUser()
        {
            var user1 = new UserModel {Login = "Tom", Age = 33};
            var user2 = new UserModel {Login = "Jerry", Age = 31};

            _unitOfWork.UserModelRepository.Create(user1);
            _unitOfWork.UserModelRepository.Create(user2);

            _unitOfWork.Save();

            var users = await _unitOfWork.UserModelRepository.GetAll().ToListAsync();
            foreach (var u in users) Console.WriteLine($"{u.Id}.{u.Login} - {u.Age}");

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

        private UserModel AuthenticatedUser(AuthenticateModel authData)
        {
            var user = _users.FirstOrDefault(x =>
                (x.Login == authData.LoginOrMail || x.Email == authData.LoginOrMail) &&
                x.Password == authData.Password);
            //TODO: get user from db
            return user;
        }

        [Authorize(Roles = "admin")]
        [HttpPost("Post")]
        public string Post()
        {
            if (!(HttpContext.User.Identity is ClaimsIdentity identity))
                return "Error";
            var claims = identity.Claims.ToList();

            var user = User;
            var userName = claims[0].Value;
            return "Welcome to: " + userName + " " + User.Identity.Name;
            ;
        }

        [Authorize]
        [HttpGet("GetValue")]
        public string[] Get()
        {
            return new[] {"Value1", "Value2", "Value3"};
        }
    }
}