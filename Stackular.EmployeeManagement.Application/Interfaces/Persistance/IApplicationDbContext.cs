using Stackular.EmployeeManagement.Domain.Entities;
namespace Stackular.EmployeeManagement.Application.Interfaces.Persistance
{
    public interface IApplicationDbContext
    {
        DbSet<Employee> Employees { get; }

        DbSet<Department> Departments { get; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
