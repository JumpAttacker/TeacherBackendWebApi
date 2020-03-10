using System.ComponentModel.DataAnnotations;

namespace TeacherBackend.Model
{
    public class UserModel : Entity
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public int Age { get; set; }
        public UserType UserType { get; set; }
        public string Role { get; set; }
    }
    
    public class AuthenticateModel
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }
    }
}