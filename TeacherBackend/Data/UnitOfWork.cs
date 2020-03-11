using System;
using TeacherBackend.Model;

namespace TeacherBackend.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private bool _disposed;
        private IRepository<Lesson> _lessonRepository;
        private IRepository<Subject> _lessonSubjectRepository;
        private IRepository<UserModel> _userModelRepository;

        public UnitOfWork()
        {
            Context = new ApplicationContext();
        }

        private ApplicationContext Context { get; }

        public IRepository<UserModel> UserModelRepository =>
            _userModelRepository ??= new Repository<UserModel>(Context);

        public IRepository<Lesson> LessonRepository => _lessonRepository ??= new Repository<Lesson>(Context);

        public IRepository<Subject> LessonSubjectRepository =>
            _lessonSubjectRepository ??= new Repository<Subject>(Context);

        public void Save()
        {
            Context.SaveChanges();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public virtual void Dispose(bool disposing)
        {
            if (_disposed) return;
            if (disposing) Context.Dispose();
            _disposed = true;
        }
    }
}