using Stackular.EmployeeManagement.Application.Services.Department.Commands;

namespace Stackular.EmployeeManagement.Application.Services.Department.Validators
{
    public class UpdateDepartmentCommandValidator : AbstractValidator<UpdateDepartmentCommand>
    {
        public UpdateDepartmentCommandValidator()
        {
            RuleFor(x => x.Name)
                .NotNull().WithMessage("Name is required.")
                .NotEmpty().WithMessage("Name cannot be empty.");
        }
    }
}
