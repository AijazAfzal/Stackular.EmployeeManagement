namespace Stackular.EmployeeManagement.Application.Services.Department.Commands
{
    public record DeleteDepartmentCommand
    {
        public Guid Id { get; set; }
    }
}
