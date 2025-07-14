using Stackular.EmployeeManagement.Api.Infrastucture;
using System.Reflection;

namespace Stackular.EmployeeManagement.Api.Extensions
{
    internal static class WebApplicationExtensions
    {
        //to map grouped routes under group name (as we are grouping in ToDoItems class)
        public static RouteGroupBuilder MapGroup(this WebApplication app, IEndpointGroupBase group)
        {
            var groupName = group.GetType().Name;

            return app
                .MapGroup($"/api/{groupName}")
                .WithOpenApi();
        }

        public static WebApplication MapEndpoints(this WebApplication app)
        {
            var endpointGroupType = typeof(IEndpointGroupBase);

            var assembly = Assembly.GetExecutingAssembly();

            var endpointGroupTypes = assembly.DefinedTypes
                 .Where(type => type is { IsAbstract: false, IsInterface: false } &&
                       type.IsAssignableTo(endpointGroupType));

            foreach (var type in endpointGroupTypes)
            {
                if (Activator.CreateInstance(type) is IEndpointGroupBase instance)
                {
                    instance.Map(app);
                }
            }

            return app;
        }
    }
}
