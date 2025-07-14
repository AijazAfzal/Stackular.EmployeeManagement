using Stackular.EmployeeManagement.Application.Services.Common.Queries;

namespace Stackular.EmployeeManagement.Application.Services.Department.Queries
{
    public class DepartmentPagedQuery : PagedBaseQuery
    {
        public string? SearchTerm { get; set; }
    }
}
