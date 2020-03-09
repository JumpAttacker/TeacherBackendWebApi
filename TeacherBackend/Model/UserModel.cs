﻿namespace TeacherBackend.Model
{
    public class UserModel : Entity
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public int Age { get; set; }
        public UserType UserType { get; set; }
    }
}