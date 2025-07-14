namespace Stackular.EmployeeManagement.Application.Services.Common.Dto
{
    public record ErrorDetailDto
    {
        public string? Field { get; set; }
        public string? Message { get; set; }
    }
}
