using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Stackular.EmployeeManagement.Application.Interfaces.Persistance;
using Stackular.EmployeeManagement.Persistance.Data;

namespace Stackular.EmployeeManagement.IntegrationTests.Infrastructure
{
    public class IntegrationTestWebAppFactory : WebApplicationFactory<Program>, IAsyncLifetime
    {
        private readonly string _connectionString =
            "Server=(localdb)\\MSSQLLocalDB;Database=EmployeeManagementTestDb;Trusted_Connection=True;TrustServerCertificate=True";

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                // Remove the default ApplicationDbContext registration
                var descriptor = services.SingleOrDefault(
                    d => d.ServiceType == typeof(DbContextOptions<EmployeeManagementDbContext>));

                if (descriptor != null)
                {
                    services.Remove(descriptor);
                }

                // Register LocalDB context
                services.AddDbContext<EmployeeManagementDbContext>(options =>
                    options.UseSqlServer(_connectionString));

                services.AddScoped<IApplicationDbContext>(provider =>
                    provider.GetRequiredService<EmployeeManagementDbContext>());

                // Ensure DB is created and clean before running each test class
                var sp = services.BuildServiceProvider();
                using var scope = sp.CreateScope();
                var db = scope.ServiceProvider.GetRequiredService<EmployeeManagementDbContext>();
                db.Database.EnsureDeleted();
                db.Database.EnsureCreated();
            });
        }

        public Task InitializeAsync() => Task.CompletedTask;

        public new Task DisposeAsync() => Task.CompletedTask;
    }
}
