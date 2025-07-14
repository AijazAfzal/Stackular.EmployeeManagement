using Microsoft.Extensions.DependencyInjection;
using Stackular.EmployeeManagement.Application;

namespace Stackular.EmployeeManagement.UnitTests
{
    public class TestFixture
    {
        public IServiceProvider ServiceProvider { get; }

        public TestFixture()
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddApplicationServices();
            ServiceProvider = serviceCollection.BuildServiceProvider();
        }
    }
}
