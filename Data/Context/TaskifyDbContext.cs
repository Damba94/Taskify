using Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Reflection;

namespace Data.Context
{
    public class TaskifyDbContext : DbContext
    {
        private readonly ILogger<TaskifyDbContext> _logger;
        public TaskifyDbContext(DbContextOptions<TaskifyDbContext> options,
            ILogger<TaskifyDbContext> logger) : base(options)
        {
            _logger = logger;
        }

        public DbSet<Assigment> Assigments { get; set; }

        public DbSet<Board> Boards { get; set; }

        public DbSet<BoardUser> BoardUsers { get; set; }

        public DbSet<Tag> Tags { get; set; }

        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(modelBuilder);
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.LogTo(log => _logger.LogInformation(log));
        }
    }
}
