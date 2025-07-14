using Stackular.EmployeeManagement.Application.Interfaces.Persistance;
using Stackular.EmployeeManagement.Domain.Entities;
using System.Reflection;

namespace Stackular.EmployeeManagement.Persistance.Data
{
    public class EmployeeManagementDbContext : DbContext, IApplicationDbContext
    {
        public EmployeeManagementDbContext() { }
        public EmployeeManagementDbContext(DbContextOptions<EmployeeManagementDbContext> options)
           : base(options)
        {
        }

        public DbSet<Department> Departments => Set<Department>();
        public DbSet<Employee> Employees => Set<Employee>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=EmployeeManagementApp;Trusted_Connection=True;TrustServerCertificate=true");
            }
            base.OnConfiguring(optionsBuilder);
        }
    }
}
