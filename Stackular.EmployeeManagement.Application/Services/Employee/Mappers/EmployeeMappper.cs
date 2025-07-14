using Stackular.EmployeeManagement.Application.Services.Employee.Dto;

namespace Stackular.EmployeeManagement.Application.Services.Employee.Mappers
{
    public static class EmployeeMappper
    {
        public static EmployeeDto ToDto(this Domain.Entities.Employee entity)
        {
            if (entity is null)
            {
                return new EmployeeDto();
            }

            return new EmployeeDto
            {
                Id = entity.Id,
                Name = entity.Name,
                DepartmentId = entity.DepartmentId,
                Dob = entity.Dob,
                EmailAddress = entity.EmailAddress
            };
        }

        public static Domain.Entities.Employee ToEntity(this EmployeeDto dto)
        {
            if (dto is null)
            {
                return new Domain.Entities.Employee();
            }

            return new Domain.Entities.Employee
            {
                Id = dto.Id,
                Name = dto.Name,
                DepartmentId = dto.DepartmentId,
                Dob = dto.Dob,
                EmailAddress = dto.EmailAddress
            };
        }
    }
}
