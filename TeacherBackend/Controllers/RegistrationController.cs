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
    public class RegistrationController : Controller
    {
        private readonly ILogger<LoginController> _logger;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserService _userService;

        public RegistrationController(ILogger<LoginController> logger, IUnitOfWork unitOfWork,
            IUserService userService)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
            _userService = userService;
        }

        [HttpPost]
        public IActionResult Registration(UserModel registrationData)
        {
            var userWithDatEmailInDb = _unitOfWork.UserModelRepository.GetAll()
                .FirstOrDefaultAsync(x => x.Email == registrationData.Email);
            if (userWithDatEmailInDb != null)
            {
                return Ok(new {Error = "current email is already using"});
            }

            _unitOfWork.UserModelRepository.Create(registrationData);

            _unitOfWork.Save();

            var jwtToken = _userService.GenerateJwtToken(registrationData);

            return Ok(
                new
                {
                    Login = registrationData.Email,
                    Token = jwtToken
                }
            );
        }
    }
}