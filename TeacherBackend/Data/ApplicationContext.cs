using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Logging;
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
            optionsBuilder.UseLoggerFactory(MyLoggerFactory);
        }

        public static readonly ILoggerFactory MyLoggerFactory = LoggerFactory.Create(builder =>
        {
            builder.AddFilter((category, level) => category == DbLoggerCategory.Database.Command.Name
                                                   && level == LogLevel.Information)
                .AddConsole();
        });

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
            modelBuilder.Entity<UserModel>()
                .Property(b => b.RegistrationTime)
                .HasDefaultValueSql("getdate()");
            
            modelBuilder.Entity<UserModel>()
                .HasIndex(u => u.Email)
                .IsUnique();

            modelBuilder.Entity<UserModelSubject>()
                .HasKey(t => new {t.SubjectId, t.UserModelId});

            modelBuilder.Entity<UserModelSubject>()
                .HasOne(pt => pt.Subject)
                .WithMany(p => p.Users)
                .HasForeignKey(pt => pt.SubjectId);

            modelBuilder.Entity<UserModelSubject>()
                .HasOne(pt => pt.UserModel)
                .WithMany(t => t.Subjects)
                .HasForeignKey(pt => pt.UserModelId);
        }
    }
}