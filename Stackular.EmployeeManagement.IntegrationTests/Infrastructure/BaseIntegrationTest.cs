using Stackular.EmployeeManagement.Application.Interfaces.Persistance;

namespace Stackular.EmployeeManagement.IntegrationTests.Infrastructure
{
    public abstract class BaseIntegrationTest : IClassFixture<IntegrationTestWebAppFactory>, IDisposable
    {
        internal readonly IntegrationTestWebAppFactory _factory;
        internal readonly IServiceScope _scope;
        internal readonly IApplicationDbContext _dbContext;
        internal bool _disposed;

        protected BaseIntegrationTest(IntegrationTestWebAppFactory factory)
        {
            _factory = factory;
            _scope = factory.Services.CreateScope();
            _dbContext = _scope.ServiceProvider.GetRequiredService<IApplicationDbContext>();
        }

        protected IntegrationTestWebAppFactory Factory => _factory;
        protected IServiceScope Scope => _scope;
        protected IApplicationDbContext DbContext => _dbContext;

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _scope.Dispose();
                }
                _disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
