using Stackular.EmployeeManagement.Application.Services.Employee.Queries;

namespace Stackular.EmployeeManagement.Application.Services.Employee.Validators
{
    public class SearchEmployeesByDepartmentNameQueryValidator : AbstractValidator<SearchEmployeesByDepartmentNameQuery>
    {
        public SearchEmployeesByDepartmentNameQueryValidator()
        {
            RuleFor(x => x.DepartmentName)
                .NotEmpty().WithMessage("Department name is required.");
        }
}   }
