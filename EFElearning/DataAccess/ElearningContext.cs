using ElearningDatabase.Models;
using Microsoft.EntityFrameworkCore;

namespace ElearningDatabase
{
    public class ElearningContext : DbContext
    {
        public ElearningContext()
        {
        }

        public ElearningContext(DbContextOptions<ElearningContext> options) : base(options)
        {
        }

        public string ConnectionString { get; }

        public DbSet<Course> Courses { get; set; }
        public DbSet<Lesson> Lessons { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Quiz> Quizs { get; set; }
        public DbSet<Resource> Resources { get; set; }
        public DbSet<UserProgress> UserProgresses { get; set; }
        public DbSet<User> Users { get; set; }

        public void Migrate()
        {
            this.Database.Migrate();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}