using Stackular.EmployeeManagement.Application.Services.Common.Enums;

namespace Stackular.EmployeeManagement.Application.Services.Common.Contracts
{
    public interface ISortingService
    {
        IQueryable<T> ApplySorting<T>(IQueryable<T> query,string sortBy,SortDirection sortOrder);
    }
}
