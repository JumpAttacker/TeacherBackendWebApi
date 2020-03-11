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
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserModel>()
                .Property(b => b.RegistrationTime)
                .HasDefaultValueSql("getdate()");
            
            modelBuilder.Entity<UserModelSubject>()
                .HasKey(t => new { t.SubjectId, t.UserModelId });

            modelBuilder.Entity<UserModelSubject>()
                .HasOne(pt => pt.Subject)
                .WithMany(p => p.UserModelSubjects)
                .HasForeignKey(pt => pt.UserModelId);

            modelBuilder.Entity<UserModelSubject>()
                .HasOne(pt => pt.UserModel)
                .WithMany(t => t.WantToLearn)
                .HasForeignKey(pt => pt.SubjectId);
        }
    }
}