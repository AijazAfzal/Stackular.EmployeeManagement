using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Stackular.EmployeeManagement.Application.Services.Common;
using Stackular.EmployeeManagement.Application.Services.Common.Contracts;
using Stackular.EmployeeManagement.Application.Services.Department;
using Stackular.EmployeeManagement.Application.Services.Department.Contracts;
using Stackular.EmployeeManagement.Application.Services.Employee;
using Stackular.EmployeeManagement.Application.Services.Employee.Contracts;

namespace Stackular.EmployeeManagement.Application
{
    public static class ApplicationServiceRegistration
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.TryAddTransient<IDepartmentService, DepartmentService>();
            services.TryAddTransient<IEmployeeService, EmployeeService>();
            services.TryAddTransient<ISortingService,SortingService>();
            services.TryAddTransient<IModelValidationService, ModelValidationService>();
            services.AddScoped<IRequestDispatcher, RequestDispatcher>();
            services.Scan(scan => scan
                     .FromAssembliesOf(typeof(AddDepartmentCommandHandler))
                     .AddClasses(classes => classes.AssignableTo(typeof(IRequestHandler<,>)))
                     .AsImplementedInterfaces()
                     .WithScopedLifetime());
            services.AddValidatorsFromAssembly(typeof(ApplicationServiceRegistration).Assembly);
            return services;
        }
    }
}
