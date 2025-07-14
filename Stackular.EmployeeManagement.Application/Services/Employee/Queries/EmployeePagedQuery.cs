using Stackular.EmployeeManagement.Application.Services.Common.Queries;

namespace Stackular.EmployeeManagement.Application.Services.Employee.Queries
{
    public class EmployeePagedQuery : PagedBaseQuery
    {
        public string? SearchTerm { get; set; }
    }
}
