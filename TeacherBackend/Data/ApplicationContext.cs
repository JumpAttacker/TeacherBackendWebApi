using Microsoft.EntityFrameworkCore;
using TeacherBackend.Model;

namespace TeacherBackend.Data
{
    public sealed class ApplicationContext : DbContext
    {
        public DbSet<UserModel> Users { get; set; }
        public DbSet<Lesson> Lessons { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=DSRKZN237;Database=TeacherCRM;Trusted_Connection=True;");
        }
    }
}