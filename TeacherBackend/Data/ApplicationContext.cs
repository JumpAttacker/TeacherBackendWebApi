using Microsoft.EntityFrameworkCore;
using TeacherBackend.Model;

namespace TeacherBackend.Data
{
    public sealed class ApplicationContext : DbContext
    {
        public DbSet<UserModel> Users { get; set; }
        public DbSet<Lesson> Lessons { get; set; }
         
        public ApplicationContext()
        {
            // Database.EnsureDeleted();
            // Database.EnsureCreated();
        }
 
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=DESKTOP-QAPRRT8\\SQLEXPRESS;Database=TeacherCRM;Trusted_Connection=True;");
        }
    }
}