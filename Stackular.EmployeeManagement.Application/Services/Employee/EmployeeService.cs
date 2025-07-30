using Stackular.EmployeeManagement.Application.Exceptions;
using Stackular.EmployeeManagement.Application.Interfaces.Infrastructure;
using Stackular.EmployeeManagement.Application.Services.Common.Contracts;
using Stackular.EmployeeManagement.Application.Services.Common.Dto;
using Stackular.EmployeeManagement.Application.Services.Employee.Commands;
using Stackular.EmployeeManagement.Application.Services.Employee.Contracts;
using Stackular.EmployeeManagement.Application.Services.Employee.Dto;
using Stackular.EmployeeManagement.Application.Services.Employee.Mappers;
using Stackular.EmployeeManagement.Application.Services.Employee.Queries;

namespace Stackular.EmployeeManagement.Application.Services.Employee
{
    public class EmployeeService(IApplicationDbContext context, IModelValidationService modelValidationService, IEmailService emailService, ISortingService sortingService) : IEmployeeService
    {
        public async Task<EmployeeDto> AddEmployee(AddEmployeeCommand request, CancellationToken ct)
        {
            modelValidationService.Validate(request);

            var employee = new Domain.Entities.Employee
            {
                Id = Guid.NewGuid(),
                Name = request.Name,
                EmailAddress = request.EmailAddress,
                Dob = request.Dob,
                DepartmentId = request.DepartmentId
            };

            await context.Employees.AddAsync(employee, ct);
            await context.SaveChangesAsync(ct);

            emailService.SendEmail();

            return EmployeeMappper.ToDto(employee);
        }

        public async Task<EmployeeDto> GetEmployee(GetEmployeeByIdQuery query, CancellationToken ct)
        {
            var employee = await context.Employees.FindAsync([query.Id], ct);

            if (employee is null)
            {
                throw new NotFoundException($"Employee with ID {query.Id} not found.");
            }

            return EmployeeMappper.ToDto(employee);
        }

        public async Task<IEnumerable<EmployeeDto>> GetEmployeesByDepartmentName(SearchEmployeesByDepartmentNameQuery query, CancellationToken ct)
        {
            modelValidationService.Validate(query);

            var employees = await context.Employees
                .Where(x => query.DepartmentName == null || (x.Department.Name != null && x.Department.Name.Contains(query.DepartmentName)))
                .Take(query.MaxEmployees)
                .ToListAsync(ct);

            return employees.Select(EmployeeMappper.ToDto);
        }

        public async Task<PagedResponseDto<EmployeeDto>> GetPagedEmployees(GetEmployeePagedQuery query, CancellationToken ct)
        {
            modelValidationService.Validate(query);

            IQueryable<Domain.Entities.Employee> employeesQuery = context.Employees;

            if (!string.IsNullOrWhiteSpace(query.SearchTerm))
            {
                employeesQuery = employeesQuery.Where(x => x.Name != null && x.Name.Contains(query.SearchTerm));
            }

            var totalCount = await employeesQuery.CountAsync(ct);

            employeesQuery = sortingService.ApplySorting(employeesQuery, query.SortBy, query.SortDirection);

            var pagedEmployees = await employeesQuery
                .Skip((query.PageNumber - 1) * query.PageSize)
                .Take(query.PageSize)
                .ToListAsync(ct);

            var totalPages = (int)Math.Ceiling(totalCount / (double)query.PageSize);

            return new PagedResponseDto<EmployeeDto>
            {
                Items = pagedEmployees.Select(EmployeeMappper.ToDto),
                TotalCount = totalCount,
                PageNumber = query.PageNumber,
                PageSize = query.PageSize,
                TotalPages = totalPages
            };
        }

        public async Task<IEnumerable<EmployeeDto>> SearchEmployeesByName(SearchEmployeeQuery query, CancellationToken ct)
        {
            modelValidationService.Validate(query);

            var employees = await context.Employees
                .Where(x => query.Name == null || (x.Name != null && x.Name.Contains(query.Name)))
                .Take(query.MaxEmployees)
                .ToListAsync(ct);

            return employees.Select(EmployeeMappper.ToDto);
        }

        public async Task UpdateEmployee(UpdateEmployeeCommand command, CancellationToken ct)
        {
            modelValidationService.Validate(command);

            var employee = await context.Employees.FindAsync([command.Id], ct)
                           ?? throw new NotFoundException($"Employee with ID {command.Id} not found.");

            employee.Name = command.Name;
            employee.EmailAddress = command.EmailAddress;
            employee.Dob = command.Dob;
            employee.DepartmentId = command.DepartmentId;

            await context.SaveChangesAsync(ct);
        }

        public async Task DeleteEmployee(DeleteEmployeeCommand command, CancellationToken ct)
        {
            var employee = await context.Employees.FindAsync([command.Id], ct);
            if (employee is null)
            {
                throw new NotFoundException($"Employee with ID {command.Id} not found.");
            }
            context.Employees.Remove(employee);
            await context.SaveChangesAsync(ct);
        }
    }
}
