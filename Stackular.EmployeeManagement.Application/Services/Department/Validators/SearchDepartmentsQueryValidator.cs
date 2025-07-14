using Stackular.EmployeeManagement.Application.Services.Department.Queries;

namespace Stackular.EmployeeManagement.Application.Services.Department.Validators
{
    public class SearchDepartmentsQueryValidator : AbstractValidator<SearchDepartmentsQuery>
    {
        public SearchDepartmentsQueryValidator()
        {
            RuleFor(x => x.Name)
                .NotNull().WithMessage("Name cannot be null.")
                .NotEmpty().WithMessage("Name cannot be empty.");

            RuleFor(x => x.MaxDepartments)
                .GreaterThan(0).WithMessage("MaxDepartments must be greater than 0.");
        }
    }
}
