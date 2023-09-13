using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Employee_Management.Models
{
    public class EmployeeContext : IdentityDbContext
    {
        public EmployeeContext(DbContextOptions<EmployeeContext> options) 
            : base(options)
        {
        }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Task> Tasks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Employee>()
                .HasKey(e => e.Id);
            modelBuilder.Entity<Employee>()
                .HasOne<IdentityUser>()
                .WithOne()
                .HasForeignKey<Employee>(e => e.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Employee>()
                .HasOne(e => e.Department)
                .WithMany(d => d.Employees)
                .HasForeignKey(e => e.DepartmentId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Department>()
                .HasKey(d => d.Id);
            modelBuilder.Entity<Department>()
                .HasOne(d => d.Manager)
                .WithOne(/*d => d.Department*/)
                .HasForeignKey<Department>(d => d.ManagerID)
                .OnDelete(DeleteBehavior.NoAction);
            
            modelBuilder.Entity<Task>()
                .HasKey(t => t.Id);
            modelBuilder.Entity<Task>()
                .HasOne(t => t.Employee)
                .WithMany(e => e.Tasks)
                .HasForeignKey(t => t.EmployeeId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
