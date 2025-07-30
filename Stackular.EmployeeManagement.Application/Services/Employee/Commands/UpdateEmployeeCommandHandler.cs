using Stackular.EmployeeManagement.Application.Services.Common;
using Stackular.EmployeeManagement.Application.Services.Common.Contracts;
using Stackular.EmployeeManagement.Application.Services.Employee.Contracts;

namespace Stackular.EmployeeManagement.Application.Services.Employee.Commands
{
    public class UpdateEmployeeCommandHandler(IEmployeeService service) : IRequestHandler<UpdateEmployeeCommand, Unit>
    {
        public async Task<Unit> HandleAsync(UpdateEmployeeCommand command, CancellationToken cancellationToken = default)
        {
            await service.UpdateEmployee(command, cancellationToken);
            return Unit.Value;
        }
    }
}
