using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        public async Task<OkObjectResult> Registration([FromBody] RegistrationModel registrationData)
        {
            var userWithDatEmailInDb = await _unitOfWork.UserModelRepository.GetAll()
                .FirstOrDefaultAsync(x => x.Email == registrationData.Email);
            if (userWithDatEmailInDb != null)
            {
                return Ok(new {Error = "current email is already using"});
            }

            var subjects = await _unitOfWork.SubjectRepository.GetAll()
                .Where(x => registrationData.WantToLearn.Contains(x.Name)).ToListAsync();

            var userModel = new UserModel
            {
                Email = registrationData.Email,
                Password = registrationData.Password,
                ClassNumber = registrationData.ClassNumber,
                FirstName = registrationData.FirstName,
                SecondName = registrationData.SecondName,
                Role = "user",
            };
            List<UserModelSubject> userSubject = subjects.Select(s => new UserModelSubject {Subject = s, UserModel = userModel})
                .ToList();

            userModel.Subjects.AddRange(userSubject);

            await _unitOfWork.UserModelRepository.Create(userModel);

            await _unitOfWork.SaveAsync();
            var users = await _unitOfWork.UserModelRepository.GetAll().Include(x=>x.Subjects).ToListAsync();
            var jwtToken = _userService.GenerateJwtToken(userModel);

            return Ok(
                new
                {
                    Message = "Успешная регистрация!",
                    Login = registrationData.Email,
                    Token = jwtToken
                }
            );
        }
    }
}