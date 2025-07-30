using Stackular.EmployeeManagement.Application.Services.Department.Commands;

namespace Stackular.EmployeeManagement.Application.Services.Department.Validators
{
    public class DeleteDepartmentCommandValidator : AbstractValidator<DeleteDepartmentCommand>
    {
        public DeleteDepartmentCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Department ID must not be empty.");
        }
    }
}
