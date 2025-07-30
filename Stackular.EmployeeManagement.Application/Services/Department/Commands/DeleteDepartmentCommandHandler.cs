using Stackular.EmployeeManagement.Application.Services.Common;
using Stackular.EmployeeManagement.Application.Services.Common.Contracts;
using Stackular.EmployeeManagement.Application.Services.Department.Contracts;

namespace Stackular.EmployeeManagement.Application.Services.Department.Commands
{
    public class DeleteDepartmentCommandHandler(IDepartmentService service) : IRequestHandler<DeleteDepartmentCommand, Unit>
    {
        public async Task<Unit> HandleAsync(DeleteDepartmentCommand request, CancellationToken cancellationToken = default)
        {
            await service.DeleteDepartment(request, cancellationToken);
            return Unit.Value;
        }
    }
}
