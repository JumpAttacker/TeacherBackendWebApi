using System;
using TeacherBackend.Model;

namespace TeacherBackend.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private IRepository<UserModel> _userModelRepository;
        private IRepository<Lesson> _lessonRepository;
        private IRepository<LessonSubject> _lessonSubjectRepository;

        public UnitOfWork()
        {
            Context = new ApplicationContext();
        }

        private ApplicationContext Context { get; set; }
        public IRepository<UserModel> UserModelRepository => _userModelRepository ??= new Repository<UserModel>(Context);
        public IRepository<Lesson> LessonRepository => _lessonRepository ??= new Repository<Lesson>(Context);
        public IRepository<LessonSubject> LessonSubjectRepository =>  _lessonSubjectRepository ??= new Repository<LessonSubject>(Context);
        
        public void Save()
        {
            Context.SaveChanges();
        }
 
        private bool _disposed = false;
 
        public virtual void Dispose(bool disposing)
        {
            if (_disposed) return;
            if (disposing)
            {
                Context.Dispose();
            }
            _disposed = true;
        }
 
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}