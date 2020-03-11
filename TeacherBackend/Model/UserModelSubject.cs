namespace TeacherBackend.Model
{
    public class UserModelSubject
    {
        public int SubjectId { get; set; }
        public Subject Subject { get; set; }
        
        public int UserModelId { get; set; }
        public UserModel UserModel { get; set; }
    }
}