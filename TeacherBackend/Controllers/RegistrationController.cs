using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using TeacherBackend.Data;
using TeacherBackend.Services;

namespace TeacherBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegistrationController
    {
        private readonly ILogger<LoginController> _logger;
        private readonly IConfiguration _config;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserService _userService;

        public RegistrationController(ILogger<LoginController> logger, IConfiguration config, IUnitOfWork unitOfWork,
            IUserService userService)
        {
            _logger = logger;
            _config = config;
            _unitOfWork = unitOfWork;
            _userService = userService;
        }

        [HttpPost]
        public IActionResult Registration()
        {
            return null;
        }
    }
}