using Elearning.Database;
using Elearning.Database.Models;
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

        public ElearningContext(DbContextOptions options) : base(options)
        {
        }

        public string ConnectionString { get; }

        public DbSet<Course> Courses { get; set; }
        public DbSet<Lesson> Lessons { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Quiz> Quizs { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Resource> Resources { get; set; }
        public DbSet<UserProgress> UserProgresses { get; set; }
        public DbSet<User> Users { get; set; }

        public void Migrate()
        {
            this.Database.Migrate();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=(localdb)\\ProjectsV13;Initial Catalog=Example;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
        }
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer(ResourceFile.connectionString);
        //}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}