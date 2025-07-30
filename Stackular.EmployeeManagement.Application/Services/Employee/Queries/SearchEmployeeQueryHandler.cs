using Stackular.EmployeeManagement.Application.Services.Common.Contracts;
using Stackular.EmployeeManagement.Application.Services.Employee.Contracts;
using Stackular.EmployeeManagement.Application.Services.Employee.Dto;

namespace Stackular.EmployeeManagement.Application.Services.Employee.Queries
{
    public class SearchEmployeeQueryHandler(IEmployeeService service) : IRequestHandler<SearchEmployeeQuery, IEnumerable<EmployeeDto>>
    {
        public async Task<IEnumerable<EmployeeDto>> HandleAsync(SearchEmployeeQuery request, CancellationToken cancellationToken = default)
        {
            return await service.SearchEmployeesByName(request, cancellationToken);
        }
    }
}
