namespace Stackular.EmployeeManagement.Domain.Entities
{
    public class Department : BaseEntity
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public ICollection<Employee> Employees { get; set; } = new List<Employee>();
    }
}
