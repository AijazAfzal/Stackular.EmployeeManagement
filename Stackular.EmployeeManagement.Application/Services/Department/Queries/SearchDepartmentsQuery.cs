namespace Stackular.EmployeeManagement.Application.Services.Department.Queries
{
    public record SearchDepartmentsQuery
    {
        public string? Name { get; set; }
        public int MaxDepartments { get; set; }
    }
}
