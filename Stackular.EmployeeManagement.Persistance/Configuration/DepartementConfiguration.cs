using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Stackular.EmployeeManagement.Domain.Entities;

namespace Stackular.EmployeeManagement.Persistance.Configuration
{
    public class DepartementConfiguration : IEntityTypeConfiguration<Department>
    {
        public void Configure(EntityTypeBuilder<Department> builder)
        {
            builder.Property(t => t.Name)
                .HasMaxLength(200)
                .IsRequired();
        }
    }
}
