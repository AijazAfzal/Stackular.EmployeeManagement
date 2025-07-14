namespace Stackular.EmployeeManagement.Application.Services.Common.Dto
{
    public record ApiErrorDto
    {
        public bool Success { get; set; }
        public string? Message { get; set; }
        public int StatusCode { get; set; }
        public IEnumerable<ErrorDetailDto>? Errors { get; set; }
    }
}
