using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        private readonly ILogger<LoginController> _logger;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserService _userService;

        // GET
        public LoginController(ILogger<LoginController> logger, IUnitOfWork unitOfWork,
            IUserService userService)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> Login(string email, string password)
        {
            var authenticateModel = new AuthenticateModel {Email = email, Password = password};

            var user =await AuthenticatedUser(authenticateModel);

            if (user == null) return Ok(new {Error = "Incorrect login or password"});

            var jwtToken = _userService.GenerateJwtToken(user);

            return Ok(
                new
                {
                    Login = user.Email,
                    Token = jwtToken
                }
            );
        }

        [HttpGet("fake")]
        public async Task<List<UserModel>> FakeUser()
        {
            var user1 = new UserModel {Email = "Tom"};
            var user2 = new UserModel {Email = "Jerry"};

            _unitOfWork.UserModelRepository.Create(user1);
            _unitOfWork.UserModelRepository.Create(user2);

            _unitOfWork.Save();

            var users = await _unitOfWork.UserModelRepository.GetAll().ToListAsync();
            foreach (var u in users) Console.WriteLine($"{u.Id}.{u.Email}");

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

        private async Task<UserModel> AuthenticatedUser(AuthenticateModel authData)
        {
            var allUsers = await _unitOfWork.UserModelRepository.GetAll().ToListAsync();
            var user = await _unitOfWork.UserModelRepository.GetAll().FirstOrDefaultAsync(x => x.Email == authData.Email &&
                                                                                         x.Password == authData.Password);
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