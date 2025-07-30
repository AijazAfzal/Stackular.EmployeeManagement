using Stackular.EmployeeManagement.Application.Services.Common.Contracts;
using Stackular.EmployeeManagement.Application.Services.Common.Dto;
using Stackular.EmployeeManagement.Application.Services.Employee.Contracts;
using Stackular.EmployeeManagement.Application.Services.Employee.Dto;

namespace Stackular.EmployeeManagement.Application.Services.Employee.Queries
{
    public class GetEmployeePagedQueryHandler(IEmployeeService service) : IRequestHandler<GetEmployeePagedQuery, PagedResponseDto<EmployeeDto>>
    {
        public async Task<PagedResponseDto<EmployeeDto>> HandleAsync(GetEmployeePagedQuery query, CancellationToken cancellationToken = default)
        {
            return await service.GetPagedEmployees(query, cancellationToken);
        }
    }
}
