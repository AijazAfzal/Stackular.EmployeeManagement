using Stackular.EmployeeManagement.Application.Services.Common.Dto;
using Stackular.EmployeeManagement.Application.Services.Department.Commands;
using Stackular.EmployeeManagement.Application.Services.Department.Dto;
using Stackular.EmployeeManagement.Application.Services.Department.Queries;

namespace Stackular.EmployeeManagement.Application.Services.Department.Contracts
{
    public interface IDepartmentService
    {
        Task<DepartmentDto> AddDepartment(AddDepartmentCommand command, CancellationToken ct);
        Task UpdateDepartment(UpdateDepartmentCommand command, CancellationToken ct);
        Task<DepartmentDto> GetDepartmentById(GetDepartmentByIdQuery query, CancellationToken ct);
        Task<IEnumerable<DepartmentDto>> SearchDepartmentsByName(SearchDepartmentsQuery query, CancellationToken ct);
        Task<PagedResponseDto<DepartmentDto>> GetPagedDepartments(GetDepartmentPagedQuery query, CancellationToken ct);
        Task DeleteDepartment(DeleteDepartmentCommand command, CancellationToken ct);
    }
}
