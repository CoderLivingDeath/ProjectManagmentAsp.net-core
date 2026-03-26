using Microsoft.EntityFrameworkCore;
using ProjectManagment.Data.Entity;

namespace ProjectManagment.Data.Context
{
    public class AppDbContext : DbContext
    {
        public DbSet<Project> projects { get; set; }
        public DbSet<Employee> employees { get; set; }
        public DbSet<Company> companies { get; set; }
        public DbSet<Position> positions { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlite(
                    "Server=.;Database=ProjectManagmentDb;Trusted_Connection=True;TrustServerCertificate=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Project>()
                .HasMany(p => p.Employees)
                .WithMany(e => e.Projects)
                .UsingEntity("project_employees");

            modelBuilder.Entity<Employee>()
                .HasMany(e => e.Positions)
                .WithMany(p => p.Employees)
                .UsingEntity("employee_positions");
        }
    }
}
