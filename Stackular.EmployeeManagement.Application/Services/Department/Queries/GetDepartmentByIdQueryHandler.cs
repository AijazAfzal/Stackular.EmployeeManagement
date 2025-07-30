using Stackular.EmployeeManagement.Application.Services.Common.Contracts;
using Stackular.EmployeeManagement.Application.Services.Department.Contracts;
using Stackular.EmployeeManagement.Application.Services.Department.Dto;

namespace Stackular.EmployeeManagement.Application.Services.Department.Queries
{
    public class GetDepartmentByIdQueryHandler(IDepartmentService service) : IRequestHandler<GetDepartmentByIdQuery, DepartmentDto>
    {
        public async Task<DepartmentDto> HandleAsync(GetDepartmentByIdQuery request, CancellationToken cancellationToken = default)
        {
            return await service.GetDepartmentById(request, cancellationToken);
        }
    }
}
