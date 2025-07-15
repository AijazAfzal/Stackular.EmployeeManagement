using Stackular.EmployeeManagement.Application.Services.Common.Dto;
using Stackular.EmployeeManagement.Application.Services.Employee.Commands;
using Stackular.EmployeeManagement.Application.Services.Employee.Dto;
using Stackular.EmployeeManagement.Application.Services.Employee.Queries;

namespace Stackular.EmployeeManagement.Application.Services.Employee.Contracts
{
    public interface IEmployeeService
    {
        Task<EmployeeDto> AddEmployee(AddEmployeeCommand request, CancellationToken ct);
        Task UpdateEmployee(Guid id, UpdateEmployeeCommand request, CancellationToken ct);
        Task<EmployeeDto> GetEmployee(Guid id, CancellationToken ct);
        Task<IEnumerable<EmployeeDto>> SearchEmployeesByName(SearchEmployeeQuery query, CancellationToken ct);

        Task<IEnumerable<EmployeeDto>> GetEmployeesByDepartmentName(SearchEmployeesByDepartmentNameQuery query, CancellationToken ct);
        Task<PagedResponseDto<EmployeeDto>> GetPagedEmployees(EmployeePagedQuery query, CancellationToken ct);
        Task DeleteEmployee(Guid id, CancellationToken ct);
    }
}
