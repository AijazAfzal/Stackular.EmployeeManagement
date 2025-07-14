using Stackular.EmployeeManagement.Application.Services.Department.Dto;

namespace Stackular.EmployeeManagement.Application.Services.Department.Mappers
{
    public static class DepatmentMapper
    {
        public static DepartmentDto ToDto(this Domain.Entities.Department entity)
        {
            if (entity is null)
            {
                return new DepartmentDto();
            }

            return new DepartmentDto
            {
                Id = entity.Id,
                Name = entity.Name
            };
        }

        public static Domain.Entities.Department ToEntity(this DepartmentDto dto)
        {
            if (dto is null)
            {
                return new Domain.Entities.Department();
            }

            return new Domain.Entities.Department
            {
                Id = dto.Id,
                Name = dto.Name
            };
        }
    }
}
