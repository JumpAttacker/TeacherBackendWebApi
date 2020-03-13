using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TeacherBackend.Model;

namespace TeacherBackend.Data
{
    public interface IRepository<TEntity> where TEntity : class
    {
        Task Create(TEntity entity);
        void Delete(TEntity entity);
        Task Delete(int id);
        Task Edit(TEntity entity);

        //read side (could be in separate Readonly Generic Repository)
        Task<TEntity> GetById(int id);
        DbSet<TEntity> GetAll();

        //separate method SaveChanges can be helpful when using this pattern with UnitOfWork
        Task SaveChanges();
    }
}