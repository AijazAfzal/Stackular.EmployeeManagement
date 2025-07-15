using Stackular.EmployeeManagement.Application.Services.Common.Dto;
using Stackular.EmployeeManagement.Application.Services.Department.Commands;
using Stackular.EmployeeManagement.Application.Services.Department.Dto;
using Stackular.EmployeeManagement.Application.Services.Department.Queries;

namespace Stackular.EmployeeManagement.Application.Services.Department.Contracts
{
    public interface IDepartmentService
    {
        Task<DepartmentDto> AddDepartment(AddDepartmentCommand request, CancellationToken ct);
        Task UpdateDepartment(Guid id, UpdateDepartmentCommand request, CancellationToken ct);
        Task<DepartmentDto> GetDepartmentById(Guid id, CancellationToken ct);
        Task<IEnumerable<DepartmentDto>> SearchDepartmentsByName(SearchDepartmentsQuery query, CancellationToken ct);
        Task<PagedResponseDto<DepartmentDto>> GetPagedDepartments(DepartmentPagedQuery query, CancellationToken ct);
        Task DeleteDepartment(Guid id, CancellationToken ct);
    }
}
