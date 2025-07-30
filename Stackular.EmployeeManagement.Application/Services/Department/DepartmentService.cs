using Stackular.EmployeeManagement.Application.Exceptions;
using Stackular.EmployeeManagement.Application.Interfaces.Infrastructure;
using Stackular.EmployeeManagement.Application.Services.Common.Contracts;
using Stackular.EmployeeManagement.Application.Services.Common.Dto;
using Stackular.EmployeeManagement.Application.Services.Department.Commands;
using Stackular.EmployeeManagement.Application.Services.Department.Contracts;
using Stackular.EmployeeManagement.Application.Services.Department.Dto;
using Stackular.EmployeeManagement.Application.Services.Department.Mappers;
using Stackular.EmployeeManagement.Application.Services.Department.Queries;

namespace Stackular.EmployeeManagement.Application.Services.Department
{
    public class DepartmentService(IApplicationDbContext context, IModelValidationService modelValidationService, IEmailService emailService, ISortingService sortingService) : IDepartmentService
    {
        public async Task<DepartmentDto> AddDepartment(AddDepartmentCommand request, CancellationToken ct)
        {
            modelValidationService.Validate(request);

            var department = new Domain.Entities.Department
            {
                Id = Guid.NewGuid(),
                Name = request.Name
            };

            await context.Departments.AddAsync(department, ct);
            await context.SaveChangesAsync(ct);


            emailService.SendEmail();

            return DepatmentMapper.ToDto(department);
        }

        public async Task<DepartmentDto> GetDepartmentById(GetDepartmentByIdQuery query, CancellationToken ct)
        {
            var department = await context.Departments.FindAsync([query.Id], ct);

            if (department is null)
            {
                throw new NotFoundException($"Department with ID {query.Id} not found.");
            }

            return DepatmentMapper.ToDto(department);
        }

        public async Task<PagedResponseDto<DepartmentDto>> GetPagedDepartments(GetDepartmentPagedQuery query, CancellationToken ct)
        {
            modelValidationService.Validate(query);

            IQueryable<Domain.Entities.Department> departmentsQuery = context.Departments;

            if (!string.IsNullOrWhiteSpace(query.SearchTerm))
            {
                departmentsQuery = departmentsQuery.Where(x => x.Name != null && x.Name.Contains(query.SearchTerm));
            }

            var totalCount = await departmentsQuery.CountAsync(ct);

            departmentsQuery =  sortingService.ApplySorting(departmentsQuery, query.SortBy, query.SortDirection);

            var pagedItems = await departmentsQuery
                .Skip((query.PageNumber - 1) * query.PageSize)
                .Take(query.PageSize)
                .ToListAsync(ct);

            var totalPages = (int)Math.Ceiling(totalCount / (double)query.PageSize);

            return new PagedResponseDto<DepartmentDto>
            {
                Items = pagedItems.Select(DepatmentMapper.ToDto),
                TotalCount = totalCount,
                PageNumber = query.PageNumber,
                PageSize = query.PageSize,
                TotalPages = totalPages
            };
        }

        public async Task<IEnumerable<DepartmentDto>> SearchDepartmentsByName(SearchDepartmentsQuery query, CancellationToken ct)
        {
            modelValidationService.Validate(query);

            var departments = await context.Departments
                .Where(x => query.Name == null || (x.Name != null && x.Name.Contains(query.Name)))
                .Take(query.MaxDepartments)
                .ToListAsync(ct);

            return departments.Select(DepatmentMapper.ToDto);
        }

        public async Task UpdateDepartment(UpdateDepartmentCommand command, CancellationToken ct)
        {
            modelValidationService.Validate(command);

            var department = await context.Departments.FindAsync([command.Id], ct)
                           ?? throw new NotFoundException($"Department with ID {command.Id} not found.");

            department.Name = command.Name;

            await context.SaveChangesAsync(ct);
        }

        public async Task DeleteDepartment(DeleteDepartmentCommand command, CancellationToken ct)
        {
            var department = await context.Departments.FindAsync([command.Id], ct);
            if (department is null)
            {
                throw new NotFoundException($"Department with ID {command.Id} not found.");
            }
            context.Departments.Remove(department);
            await context.SaveChangesAsync(ct);
        }
    }
}
