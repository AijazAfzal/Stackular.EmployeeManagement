using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Stackular.EmployeeManagement.Application.Models.Mail;
using Stackular.EmployeeManagement.Infrastructure.Mail;

namespace Stackular.EmployeeManagement.Infrastructure
{
    public static class InfrastructureServiceRegistration
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            ArgumentNullException.ThrowIfNull(configuration);
            services.Configure<EmailSettings>(configuration.GetSection("EmailSettings"));

            services.AddTransient<IEmailService, EmailService>();

            return services;
        }
    }
}
