using Stackular.EmployeeManagement.Application.Services.Common.Contracts;
using Stackular.EmployeeManagement.Application.Services.Employee.Contracts;
using Stackular.EmployeeManagement.Application.Services.Employee.Dto;

namespace Stackular.EmployeeManagement.Application.Services.Employee.Queries
{
    public class GetEmployeeByIdQueryHandler(IEmployeeService service) : IRequestHandler<GetEmployeeByIdQuery, EmployeeDto>
    {
        public async Task<EmployeeDto> HandleAsync(GetEmployeeByIdQuery query, CancellationToken cancellationToken = default)
        {
            return await service.GetEmployee(query, cancellationToken);
        }
    }
}
