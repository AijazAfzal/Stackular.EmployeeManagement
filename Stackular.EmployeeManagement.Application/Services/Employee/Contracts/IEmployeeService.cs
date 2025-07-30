using Stackular.EmployeeManagement.Application.Services.Common.Dto;
using Stackular.EmployeeManagement.Application.Services.Employee.Commands;
using Stackular.EmployeeManagement.Application.Services.Employee.Dto;
using Stackular.EmployeeManagement.Application.Services.Employee.Queries;

namespace Stackular.EmployeeManagement.Application.Services.Employee.Contracts
{
    public interface IEmployeeService
    {
        Task<EmployeeDto> AddEmployee(AddEmployeeCommand command, CancellationToken ct);
        Task UpdateEmployee(UpdateEmployeeCommand command, CancellationToken ct);
        Task<EmployeeDto> GetEmployee(GetEmployeeByIdQuery query, CancellationToken ct);
        Task<IEnumerable<EmployeeDto>> SearchEmployeesByName(SearchEmployeeQuery query, CancellationToken ct);

        Task<IEnumerable<EmployeeDto>> GetEmployeesByDepartmentName(SearchEmployeesByDepartmentNameQuery query, CancellationToken ct);
        Task<PagedResponseDto<EmployeeDto>> GetPagedEmployees(GetEmployeePagedQuery query, CancellationToken ct);
        Task DeleteEmployee(DeleteEmployeeCommand command, CancellationToken ct);
    }
}
