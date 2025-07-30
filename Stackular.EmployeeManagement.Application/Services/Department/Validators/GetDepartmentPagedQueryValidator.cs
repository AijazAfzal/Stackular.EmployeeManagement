using Stackular.EmployeeManagement.Application.Services.Department.Queries;

namespace Stackular.EmployeeManagement.Application.Services.Department.Validators
{
    public class GetDepartmentPagedQueryValidator : AbstractValidator<GetDepartmentPagedQuery>
    {
        private readonly string[] _allowedSortFields = { "Name" };
        public GetDepartmentPagedQueryValidator()
        {
            {
                RuleFor(x => x.PageNumber)
                    .GreaterThan(0)
                    .WithMessage("Page number must be greater than 0");

                RuleFor(x => x.PageSize)
                    .InclusiveBetween(1, 100)
                    .WithMessage("Page size must be between 1 and 100");

                RuleFor(x => x.SortBy)
                    .NotEmpty().WithMessage("SortBy cannot be empty.")
                    .Must(sortField => string.IsNullOrEmpty(sortField) || _allowedSortFields.Contains(sortField, StringComparer.Ordinal))
                    .WithMessage($"Sort field must be one of: {string.Join(", ", _allowedSortFields)}");

                RuleFor(x => x.SearchTerm)
                    .Must(term => term == null || term.Length > 0)
                    .WithMessage("Search term cannot be empty if provided");
            }
        }
    }
}
