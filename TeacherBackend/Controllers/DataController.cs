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

namespace TeacherBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class DataController : Controller
    {
        private readonly ILogger<LoginController> _logger;
        private readonly IUnitOfWork _unitOfWork;

        // GET
        public DataController(ILogger<LoginController> logger, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        [HttpGet("GetSubjects")]
        [AllowAnonymous]
        public async Task<IEnumerable<string>> GetSubjects()
        {
            var allList = (await _unitOfWork.SubjectRepository.GetAll().ToListAsync()).Select(x => x.Name);

            return allList;
        }

        [HttpGet("GetAllUsers")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> GetAllUsers()
        {
            // var allList = await _unitOfWork.UserModelRepository.GetAll().ToListAsync();
            //
            // if (allList.Count <= 1)
            // {
            //     var user1 = new UserModel
            //     {
            //         Email = "test1",
            //         Role = "user",
            //         Password = "test",
            //         ClassNumber = 0,
            //         FirstName = "",
            //         SecondName = "",
            //     };
            //
            //     var user2 = new UserModel
            //     {
            //         Email = "test2",
            //         Role = "user",
            //         Password = "test",
            //         ClassNumber = 0,
            //         FirstName = "",
            //         SecondName = "",
            //     };
            //
            //     var subject1 = new Subject
            //     {
            //         Name = "Subject one",
            //     };
            //
            //     var subject2 = new Subject
            //     {
            //         Name = "Subject two",
            //     };
            //
            //     var userSubject = new UserModelSubject
            //     {
            //         Subject = subject1,
            //         UserModel = user1
            //     };
            //
            //     var userSubject2 = new UserModelSubject
            //     {
            //         Subject = subject2,
            //         UserModel = user1
            //     };
            //
            //     var userSubject3 = new UserModelSubject
            //     {
            //         Subject = subject1,
            //         UserModel = user2
            //     };
            //
            //     user1.Subjects.Add(userSubject);
            //     user1.Subjects.Add(userSubject2);
            //     user2.Subjects.Add(userSubject3);
            //
            //     await _unitOfWork.UserModelRepository.Create(user1);
            //     await _unitOfWork.UserModelRepository.Create(user2);
            //
            //     await _unitOfWork.SubjectRepository.Create(subject1);
            //     await _unitOfWork.SubjectRepository.Create(subject2);
            //
            //     await _unitOfWork.SaveAsync();
            // }

            // var subjects = _unitOfWork.SubjectRepository.GetAll().ToListAsync();

            var users = await _unitOfWork.UserModelRepository.GetAll()
                // .Include(x => x.Subjects)
                // .ThenInclude(x => x.Subject)
                .Select(x => new
                {
                    x.Email,
                    x.Password,
                    x.Role,
                    x.ClassNumber,
                    x.FirstName,
                    x.SecondName,
                    x.RegistrationTime,
                    subjects = new List<string>(x.Subjects.Select(z => z.Subject.Name))
                }).ToListAsync();
            
            return Ok(
                new
                {
                    list = users
                }
            );
        }
    }
}