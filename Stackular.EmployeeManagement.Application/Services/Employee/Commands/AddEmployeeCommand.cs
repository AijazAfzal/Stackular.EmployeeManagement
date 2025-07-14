namespace Stackular.EmployeeManagement.Application.Services.Employee.Commands
{
    public record AddEmployeeCommand
    {
        public string Name { get; set; }

        public string EmailAddress { get; set; }

        public DateTime Dob { get; set; }

        public Guid DepartmentId { get; set; }
    }
}
