using Stackular.EmployeeManagement.Application.Services.Common.Contracts;
using Stackular.EmployeeManagement.Application.Services.Department.Commands;
using Stackular.EmployeeManagement.Application.Services.Department.Contracts;
using Stackular.EmployeeManagement.Application.Services.Department.Dto;

public class AddDepartmentCommandHandler(IApplicationDbContext context, IModelValidationService modelValidationService, IDepartmentService service) : IRequestHandler<AddDepartmentCommand, DepartmentDto>
{
    public async Task<DepartmentDto> HandleAsync(AddDepartmentCommand command, CancellationToken cancellationToken = default)
    {
       return await service.AddDepartment(command, cancellationToken);

    }
}
