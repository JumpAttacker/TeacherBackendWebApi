using TeacherBackend.Model;

namespace TeacherBackend.Services
{
    public interface IUserService
    {
        string GenerateJwtToken(UserModel user);
    }
}