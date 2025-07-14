namespace Stackular.EmployeeManagement.Application.Services.Common.Contracts
{
    public interface IModelValidationService
    {
        void Validate<T>(T model);
    }
}
