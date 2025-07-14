namespace Stackular.EmployeeManagement.Application.Services.Employee.Dto
{
    public class EmployeeDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public string EmailAddress { get; set; }

        public DateTime Dob { get; set; }

        public Guid DepartmentId { get; set; }
    }
}
