namespace Stackular.EmployeeManagement.Application.Services.Employee.Queries
{
    public record SearchEmployeesByDepartmentNameQuery
    {
        public string DepartmentName { get; set; }

        public int MaxEmployees { get; set; }
    }
}
