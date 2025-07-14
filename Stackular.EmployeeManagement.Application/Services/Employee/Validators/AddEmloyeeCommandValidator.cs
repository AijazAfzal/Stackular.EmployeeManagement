using Stackular.EmployeeManagement.Application.Services.Employee.Commands;

namespace Stackular.EmployeeManagement.Application.Services.Employee.Validators
{
    public class AddEmloyeeCommandValidator : AbstractValidator<AddEmployeeCommand>
    {
        public AddEmloyeeCommandValidator()
        {
            RuleFor(x => x.Name)
                .NotNull().WithMessage("Name is required.")
                .NotEmpty().WithMessage("Name cannot be empty.");

            RuleFor(x => x.EmailAddress)
                .NotNull().WithMessage("Email Address is required.")
                .NotEmpty().WithMessage("Email Address cannot be empty.");

            RuleFor(x => x.DepartmentId)
                .NotNull().WithMessage("DepartmentId is required.")
                .NotEmpty().WithMessage("DepartmentId cannot be empty.");

            RuleFor(x => x.Dob)
                .NotNull().WithMessage("Date of Birth is required.")
                .NotEmpty().WithMessage("Date of Birth cannot be empty.")
                .LessThan(DateTime.Now).WithMessage("Date of Birth must be in the past.");
        }
    }
}
