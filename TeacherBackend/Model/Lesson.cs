using System;

namespace TeacherBackend.Model
{
    public class Lesson : Entity
    {
        public string Name { get; set; }
        public DateTime StartTime { get; set; }
        public int Minutes { get; set; }
        public int Price { get; set; }
        public Subject LessonSubject { get; set; }
        public UserModel Teacher { get; set; }
        public UserModel Student { get; set; }
    }
}