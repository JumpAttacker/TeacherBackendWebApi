using System.Collections.Generic;

namespace TeacherBackend.Model
{
    public class RegistrationModel
    {
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string Password { get; set; }
        public int ClassNumber { get; set; }
        public List<string> WantToLearn { get; set; }
    }
}