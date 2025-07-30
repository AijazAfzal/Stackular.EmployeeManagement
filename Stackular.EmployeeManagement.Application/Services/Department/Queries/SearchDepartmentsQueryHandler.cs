using Stackular.EmployeeManagement.Application.Services.Common.Contracts;
using Stackular.EmployeeManagement.Application.Services.Department.Contracts;
using Stackular.EmployeeManagement.Application.Services.Department.Dto;

namespace Stackular.EmployeeManagement.Application.Services.Department.Queries
{
    public class SearchDepartmentsQueryHandler(IDepartmentService service) : IRequestHandler<SearchDepartmentsQuery, IEnumerable<DepartmentDto>>
    {
        public Task<IEnumerable<DepartmentDto>> HandleAsync(SearchDepartmentsQuery request, CancellationToken cancellationToken = default)
        {
            return service.SearchDepartmentsByName(request, cancellationToken);
        }
    }
}
