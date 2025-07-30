using Stackular.EmployeeManagement.Application.Services.Common.Contracts;
using Stackular.EmployeeManagement.Application.Services.Employee.Contracts;
using Stackular.EmployeeManagement.Application.Services.Employee.Dto;

namespace Stackular.EmployeeManagement.Application.Services.Employee.Queries
{
    public class SearchEmployeesByDepartmentNameQueryHandler(IEmployeeService service) : IRequestHandler<SearchEmployeesByDepartmentNameQuery, IEnumerable<EmployeeDto>>
    {
        public async Task<IEnumerable<EmployeeDto>> HandleAsync(SearchEmployeesByDepartmentNameQuery query, CancellationToken cancellationToken = default)
        {
            return await service.GetEmployeesByDepartmentName(query, cancellationToken);
        }
    }
}
