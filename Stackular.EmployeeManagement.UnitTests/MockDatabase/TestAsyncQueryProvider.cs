using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Stackular.EmployeeManagement.UnitTests.MockDatabase
{
    public class TestAsyncQueryProvider<TEntity> : IAsyncQueryProvider
    {
        private readonly IQueryProvider _inner;

        public TestAsyncQueryProvider(IQueryProvider inner)
        {
            _inner = inner;
        }

        public IQueryable CreateQuery(Expression expression)
        {
            return new TestAsyncEnumerable<TEntity>(expression);
        }

        public IQueryable<TElement> CreateQuery<TElement>(Expression expression)
        {
            return new TestAsyncEnumerable<TElement>(expression);
        }

        public object Execute(Expression expression)
        {
            return _inner.Execute(expression)!;
        }

        public TResult Execute<TResult>(Expression expression)
        {
            return _inner.Execute<TResult>(expression);
        }

        public TResult ExecuteAsync<TResult>(Expression expression, CancellationToken cancellationToken = default)
        {
            var resultType = typeof(TResult).GetGenericArguments()[0];

            // Using null-conditional and null-forgiving operators instead of disabling warnings
            var method = typeof(IQueryProvider)
                .GetMethod(
                    name: nameof(IQueryProvider.Execute),
                    genericParameterCount: 1,
                    types: new[] { typeof(Expression) });

            if (method == null)
                throw new InvalidOperationException("Could not find Execute method on IQueryProvider");

            var genericMethod = method.MakeGenericMethod(resultType);
            var executionResult = genericMethod.Invoke(_inner, new[] { expression });

            var taskFromResultMethod = typeof(Task).GetMethod(nameof(Task.FromResult))
                ?.MakeGenericMethod(resultType);

            if (taskFromResultMethod == null)
                throw new InvalidOperationException("Could not find FromResult method on Task");

            return (TResult)taskFromResultMethod.Invoke(null, new[] { executionResult })!;
        }
    }
}
