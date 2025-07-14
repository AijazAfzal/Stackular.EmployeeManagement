using Stackular.EmployeeManagement.Application.Services.Common.Enums;
using System.Text.Json.Serialization;

namespace Stackular.EmployeeManagement.Application.Services.Common.Queries
{
    public abstract class PagedBaseQuery
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public string SortBy { get; set; } = string.Empty;

        private string _sortOrder = "asc";
        public string SortOrder
        {
            get => _sortOrder;
            set => _sortOrder = value != null && value.Equals("desc", StringComparison.OrdinalIgnoreCase) ? "desc" : "asc";
        }

        [JsonIgnore]
        public SortDirection SortDirection => SortOrder.Equals("desc", StringComparison.OrdinalIgnoreCase) ?
            SortDirection.Descending : SortDirection.Ascending;
    }
}
