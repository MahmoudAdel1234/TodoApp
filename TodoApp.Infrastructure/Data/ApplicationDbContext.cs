using Microsoft.EntityFrameworkCore;
using TodoApp.Core.Entities;

namespace TodoApp.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Todo> Todos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Todo>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Title).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Description).HasMaxLength(500);
                entity.Property(e => e.Status).IsRequired();
                entity.Property(e => e.Priority).IsRequired();
                entity.Property(e => e.CreatedDate).IsRequired();
                entity.Property(e => e.LastModifiedDate).IsRequired();
            });
        }
    }
} 