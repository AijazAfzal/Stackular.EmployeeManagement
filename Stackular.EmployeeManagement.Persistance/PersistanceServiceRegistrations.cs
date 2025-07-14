using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Stackular.EmployeeManagement.Application.Interfaces.Persistance;
using Stackular.EmployeeManagement.Persistance.Data;

namespace Stackular.EmployeeManagement.Persistance
{
    public static class PersistenceServiceRegistration
    {
        public static IServiceCollection AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<EmployeeManagementDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("EmployeeManagementConnectionString")));
            services.AddScoped<IApplicationDbContext>(provider => provider.GetRequiredService<EmployeeManagementDbContext>());

            services.AddScoped<ApplicationDbContextInitialiser>();

            return services;
        }
    }
}
