namespace TeacherBackend.Model
{
    public class UserModel : Entity
    {
        public string Login { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public int Age { get; set; }
        public UserType UserType { get; set; }
        public string Role { get; set; }
    }

    public class RegistrationUserModel
    {
        public string Login { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public int Age { get; set; }
        public UserType UserType { get; set; }
        public int ClassNumber { get; set; }
        public string[] Lessions { get; set; }
    }
}