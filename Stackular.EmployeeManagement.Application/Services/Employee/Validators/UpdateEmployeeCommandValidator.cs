using Stackular.EmployeeManagement.Application.Services.Employee.Commands;

namespace Stackular.EmployeeManagement.Application.Services.Employee.Validators
{
    public class UpdateEmployeeCommandValidator : AbstractValidator<UpdateEmployeeCommand>
    {
        public UpdateEmployeeCommandValidator()
        {
            RuleFor(x => x.Name)
                .NotNull().WithMessage("Name is required.")
                .NotEmpty().WithMessage("Name cannot be empty.");
        }
}   }
