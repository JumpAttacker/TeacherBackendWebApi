using System;
using TeacherBackend.Model;

namespace TeacherBackend.Data
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<UserModel> UserModelRepository { get; }
        IRepository<Lesson> LessonRepository { get; }
        IRepository<Subject> LessonSubjectRepository { get; }
        void Save();
    }
}