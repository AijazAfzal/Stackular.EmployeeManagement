using Stackular.EmployeeManagement.Application.Services.Common.Contracts;

namespace Stackular.EmployeeManagement.Application.Services.Common
{
    public class RequestDispatcher : IRequestDispatcher
    {
        private readonly IServiceProvider _serviceProvider;

        public RequestDispatcher(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task<TResult> DispatchAsync<TRequest, TResult>(TRequest request, CancellationToken cancellationToken = default)
        {
            var handlerType = typeof(IRequestHandler<,>).MakeGenericType(typeof(TRequest), typeof(TResult));
            var handler = _serviceProvider.GetService(handlerType);

            if (handler == null)
                throw new InvalidOperationException($"Handler for '{typeof(TRequest).Name}' not found.");

            var method = handlerType.GetMethod("HandleAsync");
            return await (Task<TResult>)method!.Invoke(handler, new object[] { request, cancellationToken })!;
        }
    }

}
