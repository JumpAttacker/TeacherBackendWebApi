using System;
using TeacherBackend.Model;

namespace TeacherBackend.Data
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<UserModel> UserModelRepository { get; }
        IRepository<Lesson> LessonRepository { get; }
        IRepository<LessonSubject> LessonSubjectRepository { get; }
        void Save();
    }
}