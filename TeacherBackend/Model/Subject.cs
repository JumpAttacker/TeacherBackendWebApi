using System.Collections.Generic;

namespace TeacherBackend.Model
{
    public class Subject : Entity
    {
        public string Name { get; set; }
        public List<UserModelSubject> UserModelSubjects { get; set; }
    }
}