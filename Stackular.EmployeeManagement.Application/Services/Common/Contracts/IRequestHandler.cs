namespace Stackular.EmployeeManagement.Application.Services.Common.Contracts
{
    public interface IRequestHandler<TRequest, TResult>
    {
        Task<TResult> HandleAsync(TRequest request, CancellationToken cancellationToken = default);
    }

}
