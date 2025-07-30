using Stackular.EmployeeManagement.Application.Services.Common;
using Stackular.EmployeeManagement.Application.Services.Common.Contracts;
using Stackular.EmployeeManagement.Application.Services.Employee.Contracts;

namespace Stackular.EmployeeManagement.Application.Services.Employee.Commands
{
    public class DeleteEmployeeCommandHandler(IEmployeeService service) : IRequestHandler<DeleteEmployeeCommand, Unit>
    {
        public async Task<Unit> HandleAsync(DeleteEmployeeCommand command, CancellationToken cancellationToken = default)
        {
            await service.DeleteEmployee(command, cancellationToken);
            return Unit.Value;
        }
    }
}
