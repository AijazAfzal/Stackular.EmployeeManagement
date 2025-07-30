using Stackular.EmployeeManagement.Application.Services.Common.Queries;

namespace Stackular.EmployeeManagement.Application.Services.Department.Queries
{
    public class GetDepartmentPagedQuery : PagedBaseQuery
    {
        public string? SearchTerm { get; set; }
    }
}
