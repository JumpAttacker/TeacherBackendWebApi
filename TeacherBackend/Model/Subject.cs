using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Newtonsoft.Json;

namespace TeacherBackend.Model
{
    public class Subject : Entity
    {
        public Subject()
        {
            Users = new List<UserModelSubject>();
        }

        public string Name { get; set; }
        public List<UserModelSubject> Users { get; set; }
    }
}