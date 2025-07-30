using Stackular.EmployeeManagement.Application.Services.Common.Contracts;
using Stackular.EmployeeManagement.Application.Services.Common.Dto;
using Stackular.EmployeeManagement.Application.Services.Department.Contracts;
using Stackular.EmployeeManagement.Application.Services.Department.Dto;

namespace Stackular.EmployeeManagement.Application.Services.Department.Queries
{
    public class GetDepartmentPagedQueryHandler(IDepartmentService service) : IRequestHandler<GetDepartmentPagedQuery, PagedResponseDto<DepartmentDto>>
    {
        public async Task<PagedResponseDto<DepartmentDto>> HandleAsync(GetDepartmentPagedQuery request, CancellationToken cancellationToken = default)
        {
            return await service.GetPagedDepartments(request, cancellationToken);
        }
    }
}
