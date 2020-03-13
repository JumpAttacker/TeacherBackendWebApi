using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TeacherBackend.Model;

namespace TeacherBackend.Data
{
    public class Repository<TEntity> : IRepository<TEntity>
        where TEntity : class, IEntity
    {
        private readonly ApplicationContext _context;

        public Repository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task Create(TEntity entity)
        {
            await _context.Set<TEntity>().AddAsync(entity);
        }

        public void Delete(TEntity entity)
        {
            _context.Set<TEntity>().Remove(entity);
        }

        public async Task Delete(int id)
        {
            var entityToDelete = await _context.Set<TEntity>().FirstOrDefaultAsync(e => e.Id == id);
            if (entityToDelete != null) _context.Set<TEntity>().Remove(entityToDelete);
        }

        public async Task Edit(TEntity entity)
        {
            var editedEntity = await _context.Set<TEntity>().FirstOrDefaultAsync(e => e.Id == entity.Id);
            editedEntity = entity;
        }

        public Task<TEntity> GetById(int id)
        {
            return _context.Set<TEntity>().FirstOrDefaultAsync(e => e.Id == id);
        }

        public DbSet<TEntity> GetAll()
        {
            return _context.Set<TEntity>();
        }

        public async Task SaveChanges()
        {
            await _context.SaveChangesAsync();
        }
    }
}