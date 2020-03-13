using System;
using System.Threading.Tasks;
using TeacherBackend.Model;

namespace TeacherBackend.Data
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<UserModel> UserModelRepository { get; }
        IRepository<Lesson> LessonRepository { get; }
        IRepository<Subject> SubjectRepository { get; }
        void Save();
        Task SaveAsync();
    }
}