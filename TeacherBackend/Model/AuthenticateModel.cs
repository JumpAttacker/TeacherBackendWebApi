﻿using System.ComponentModel.DataAnnotations;

namespace TeacherBackend.Model
{
    public class AuthenticateModel
    {
        [Required] public string Email { get; set; }

        [Required] public string Password { get; set; }
    }
}