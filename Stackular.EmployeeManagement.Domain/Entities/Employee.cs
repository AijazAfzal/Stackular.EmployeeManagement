namespace Stackular.EmployeeManagement.Domain.Entities
{
    public class Employee : BaseEntity
    {
        public Guid  Id { get; set; }
        public string Name { get; set; }

        public string EmailAddress { get; set; }

        public DateTime Dob { get; set; }

        public Guid DepartmentId { get; set; }

        public Department Department { get; set; }
    }
}
