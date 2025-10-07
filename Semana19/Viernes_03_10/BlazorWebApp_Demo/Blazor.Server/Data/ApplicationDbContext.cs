using Blazor.Server.Models;
using Microsoft.EntityFrameworkCore;

namespace Blazor.Server.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Department> Departments { get; set; }
        public DbSet<Employee> Employees { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Department>(entity =>
            {
                entity.HasKey(x => x.Id);
                entity.ToTable("Departments");
                entity.Property(x => x.Name).HasMaxLength(50).IsUnicode(false);
            });

            modelBuilder.Entity<Employee>(entity =>
            {
                entity.HasKey(x => x.Id);
                entity.ToTable("Employees");
                entity.Property(x => x.Name).HasMaxLength(255).IsUnicode(false);
                entity.Property(x => x.Salary).HasColumnType("decimal(18,2)");
                entity.Property(x => x.DateContract).IsRequired();
                entity.HasOne(x => x.Department)
                      .WithMany(x => x.Employees)
                      .HasForeignKey(x => x.IdDepartment)
                      .OnDelete(DeleteBehavior.Cascade);
            });
        }
    }
}
