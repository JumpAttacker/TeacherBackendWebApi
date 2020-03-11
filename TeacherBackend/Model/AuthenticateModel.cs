using System.ComponentModel.DataAnnotations;

namespace TeacherBackend.Model
{
    public class AuthenticateModel
    {
        [Required] public string LoginOrMail { get; set; }

        [Required] public string Password { get; set; }
    }
}