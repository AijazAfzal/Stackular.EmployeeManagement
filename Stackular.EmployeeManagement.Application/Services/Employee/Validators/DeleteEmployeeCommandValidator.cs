using Stackular.EmployeeManagement.Application.Services.Employee.Commands;

namespace Stackular.EmployeeManagement.Application.Services.Employee.Validators
{
    public class DeleteEmployeeCommandValidator : AbstractValidator<DeleteEmployeeCommand>
    {
        public DeleteEmployeeCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotNull().WithMessage("Employee ID must not be empty.");
        }
    }
}
