using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Stackular.EmployeeManagement.Domain.Entities;

namespace Stackular.EmployeeManagement.Persistance.Configuration
{
    public class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.Property(e => e.Name)
                 .HasMaxLength(100)
                 .IsRequired();

            builder.Property(e => e.EmailAddress)
                .IsRequired();

            builder.Property(e => e.Dob)
                .IsRequired();

            builder.HasOne(e => e.Department)
                .WithMany(d => d.Employees)
                .HasForeignKey(e => e.DepartmentId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
