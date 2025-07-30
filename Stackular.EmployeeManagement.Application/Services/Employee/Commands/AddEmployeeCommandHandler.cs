using Stackular.EmployeeManagement.Application.Services.Common.Contracts;
using Stackular.EmployeeManagement.Application.Services.Employee.Contracts;
using Stackular.EmployeeManagement.Application.Services.Employee.Dto;

namespace Stackular.EmployeeManagement.Application.Services.Employee.Commands
{
    public class AddEmployeeCommandHandler(IEmployeeService service) : IRequestHandler<AddEmployeeCommand, EmployeeDto>
    {
        public async Task<EmployeeDto> HandleAsync(AddEmployeeCommand command, CancellationToken cancellationToken = default)
        {
            return await service.AddEmployee(command, cancellationToken);
        }
    }
}
