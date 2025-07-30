namespace Stackular.EmployeeManagement.Application.Services.Department.Queries
{
    public record GetDepartmentByIdQuery
    {
        public Guid Id { get; set; }
    }
}
