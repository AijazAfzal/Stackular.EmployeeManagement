namespace Stackular.EmployeeManagement.Application.Services.Common.Contracts
{
    public interface IRequestDispatcher
    {
        Task<TResult> DispatchAsync<TRequest, TResult>(TRequest request, CancellationToken cancellationToken = default);
    }
}
