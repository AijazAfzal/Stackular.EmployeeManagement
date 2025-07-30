using Stackular.EmployeeManagement.Application.Services.Common;
using Stackular.EmployeeManagement.Application.Services.Common.Contracts;
using Stackular.EmployeeManagement.Application.Services.Department.Contracts;

namespace Stackular.EmployeeManagement.Application.Services.Department.Commands
{
    public class UpdateDepartmentCommandHandler(IApplicationDbContext context, IModelValidationService modelValidationService, IDepartmentService service) : IRequestHandler<UpdateDepartmentCommand,Unit>
    {
        public async Task<Unit> HandleAsync(UpdateDepartmentCommand command, CancellationToken cancellationToken = default)
        {
            await service.UpdateDepartment(command, cancellationToken);
            return Unit.Value;
        }
    }
}
