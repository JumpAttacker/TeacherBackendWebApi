using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace TeacherBackend.Model
{
    public class UserModel : Entity
    {
        public UserModel()
        {
            Subjects = new List<UserModelSubject>();
        }

        [Required]  
        public string Email { get; set; }
        [Required]  
        public string FirstName { get; set; }
        [Required]  
        public string SecondName { get; set; }
        [Required]  
        public string Password { get; set; }
        [Required]  
        public string Role { get; set; }
        [Required]  
        public int ClassNumber { get; set; }
        [Required]  
        public List<UserModelSubject> Subjects { get; set; }
        public DateTime RegistrationTime { get; set; }
    }
}