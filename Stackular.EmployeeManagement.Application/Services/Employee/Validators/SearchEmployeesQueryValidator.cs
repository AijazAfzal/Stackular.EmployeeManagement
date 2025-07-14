using Stackular.EmployeeManagement.Application.Services.Employee.Queries;

namespace Stackular.EmployeeManagement.Application.Services.Employee.Validators
{
    public class SearchEmployeesQueryValidator : AbstractValidator<SearchEmployeeQuery>
    {
        public SearchEmployeesQueryValidator()
        {
            RuleFor(x => x.Name)
                .Must(name => name == null || name.Length > 0)
                .WithMessage("Name cannot be empty if provided.");

            RuleFor(x => x.MaxEmployees)
                .GreaterThan(0)
                .WithMessage("MaxItems must be greater than zero.");
        }
    }
}
