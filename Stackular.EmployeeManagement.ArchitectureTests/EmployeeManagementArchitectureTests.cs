using ArchUnitNET.Fluent;
using ArchUnitNET.Loader;
using System.Reflection;

namespace Stackular.EmployeeManagement.ArchitectureTests
{
    public class EmployeeManagementArchitectureTests
    {
        private const string ApiNamespace = "Stackular.EmployeeManagement.Api";
        private const string ApplicationNamespace = "Stackular.EmployeeManagement.Application";
        private const string InfrastructureNamespace = "Stackular.EmployeeManagement.Infrastructure";
        private const string PersistenceNamespace = "Stackular.EmployeeManagement.Persistance";

        // Load your architecture once
        private static readonly ArchUnitNET.Domain.Architecture Architecture = new ArchLoader()
            .LoadAssemblies(
                Assembly.Load(ApiNamespace),
                Assembly.Load(ApplicationNamespace),
                Assembly.Load(InfrastructureNamespace),
                Assembly.Load(PersistenceNamespace))
            .Build();

        #region API Project Rules

        [Fact]
        public void EndpointClassesShouldImplementIEndpointGroupBase()
        {
            // Arrange
            var endpointTypes = ArchRuleDefinition.Classes()
                .That()
                .ResideInNamespace($"{ApiNamespace}.EndPoints", true)
                .As("Endpoint Classes");

            // Act & Assert
            foreach (var type in endpointTypes.GetObjects(Architecture))
            {
                var assembly = Assembly.Load(ApiNamespace);
                var actualType = assembly.GetType(type.FullName);

                Assert.True(actualType != null && actualType.GetInterfaces().Any(i => i.FullName == $"{ApiNamespace}.Infrastructure.IEndpointGroupBase"),
                    $"Type {type.FullName} should implement IEndpointGroupBase");
            }
        }

        [Fact]
        public void ExtensionClassesShouldBeStaticAndEndWithExtensions()
        {
            // Arrange
            var extensionTypes = ArchRuleDefinition.Classes()
                .That()
                .ResideInNamespace($"{ApiNamespace}.Extensions", true)
                .As("Extension Classes");

            // Act & Assert
            foreach (var type in extensionTypes.GetObjects(Architecture))
            {
                var assembly = Assembly.Load(ApiNamespace);
                var actualType = assembly.GetType(type.FullName);

                Assert.True(actualType != null && actualType.Name.EndsWith("Extensions", StringComparison.Ordinal),
                    $"Type {type.FullName} should end with 'Extensions'");
                Assert.True(actualType != null && actualType.IsSealed && actualType.IsAbstract,
                    $"Type {type.FullName} should be static (sealed and abstract)");

                var nonStaticMethods = actualType!.GetMethods()
                    .Where(m => !m.IsStatic && !IsInheritedFromObject(m))
                    .ToList();
                Assert.Empty(nonStaticMethods);
            }
        }

        [Fact]
        public void InfrastructureFolderShouldContainOnlyInterfaces()
        {
            // Arrange
            var infrastructureTypes = ArchRuleDefinition.Types()
                .That()
                .ResideInNamespace($"{ApiNamespace}.Infrastructure");

            // Act & Assert
            foreach (var type in infrastructureTypes.GetObjects(Architecture))
            {
                var assembly = Assembly.Load(ApiNamespace);
                var actualType = assembly.GetType(type.FullName);

                Assert.True(actualType != null && actualType.IsInterface,
                    $"Type {type.FullName} should be an interface");
            }
        }

        #endregion

        #region Application Project Rules - Exceptions

        [Fact]
        public void ExceptionClassesShouldEndWithExceptionAndInheritExceptionClass()
        {
            // Arrange
            var exceptionTypes = ArchRuleDefinition.Classes()
                .That()
                .ResideInNamespace($"{ApplicationNamespace}.Exceptions", true)
                .As("Exception Classes");

            // Act & Assert
            foreach (var type in exceptionTypes.GetObjects(Architecture))
            {
                var assembly = Assembly.Load(ApplicationNamespace);
                var actualType = assembly.GetType(type.FullName);

                Assert.True(actualType != null && actualType.Name.EndsWith("Exception", StringComparison.Ordinal),
                    $"Type {type.FullName} should end with 'Exception'");
                Assert.True(actualType != null && actualType.IsSubclassOf(typeof(Exception)),
                    $"Type {type.FullName} should inherit from Exception class");
            }
        }

        #endregion

        #region Application Project Rules - Interfaces

        [Fact]
        public void AllInterfacesShouldStartWithI()
        {
            // Arrange
            var allInterfaces = ArchRuleDefinition.Interfaces()
                .That()
                .ResideInAssembly(ApiNamespace)
                .Or().ResideInAssembly(ApplicationNamespace)
                .Or().ResideInAssembly(InfrastructureNamespace)
                .As("All Interfaces");

            // Act & Assert
            foreach (var type in allInterfaces.GetObjects(Architecture))
            {
                var assembly = Assembly.Load(type.Assembly.FullName);
                var actualType = assembly.GetType(type.FullName);

                Assert.True(actualType != null && actualType.Name.StartsWith('I'),
                    $"Type {type.FullName} should start with 'I'");
            }
        }

        #endregion

        #region Application Project Rules - Services

        [Fact]
        public void DtoClassesShouldEndWithDto()
        {
            // Arrange
            var dtoTypes = ArchRuleDefinition.Classes()
                .That()
                .ResideInNamespace($"{ApplicationNamespace}.Services.Dto", true)
                .As("DTO Classes");

            // Act & Assert
            foreach (var type in dtoTypes.GetObjects(Architecture))
            {
                var assembly = Assembly.Load(ApplicationNamespace);
                var actualType = assembly.GetType(type.FullName);

                Assert.True(actualType != null && actualType.Name.EndsWith("Dto", StringComparison.Ordinal),
                    $"Type {type.FullName} should end with 'Dto'");
            }
        }

        [Fact]
        public void CommandClassesShouldEndWithCommand()
        {
            // Arrange
            var commandTypes = ArchRuleDefinition.Classes()
                .That()
                .ResideInNamespace($"{ApplicationNamespace}.Services.Commands", true)
                .As("Command Classes");

            // Act & Assert
            foreach (var type in commandTypes.GetObjects(Architecture))
            {
                var assembly = Assembly.Load(ApplicationNamespace);
                var actualType = assembly.GetType(type.FullName);

                Assert.True(actualType != null && actualType.Name.EndsWith("Command", StringComparison.Ordinal),
                    $"Type {type.FullName} should end with 'Command'");
            }
        }

        [Fact]
        public void ContractClassesShouldBeInterfacesAndStartWithI()
        {
            // Arrange
            var contractTypes = ArchRuleDefinition.Types()
                .That()
                .ResideInNamespace($"{ApplicationNamespace}.Services.Contracts", true)
                .As("Contract Types");

            // Act & Assert
            foreach (var type in contractTypes.GetObjects(Architecture))
            {
                var assembly = Assembly.Load(ApplicationNamespace);
                var actualType = assembly.GetType(type.FullName);

                Assert.True(actualType != null && actualType.IsInterface,
                    $"Type {type.FullName} should be an interface");
                Assert.True(actualType != null && actualType.Name.StartsWith('I'),
                    $"Type {type.FullName} should start with 'I'");
            }
        }

        [Fact]
        public void QueryClassesShouldEndWithQuery()
        {
            // Arrange
            var queryTypes = ArchRuleDefinition.Classes()
                .That()
                .ResideInNamespace($"{ApplicationNamespace}.Services.Queries", true)
                .As("Query Classes");

            // Act & Assert
            foreach (var type in queryTypes.GetObjects(Architecture))
            {
                var assembly = Assembly.Load(ApplicationNamespace);
                var actualType = assembly.GetType(type.FullName);

                Assert.True(actualType != null && actualType.Name.EndsWith("Query", StringComparison.Ordinal),
                    $"Type {type.FullName} should end with 'Query'");
            }
        }

        [Fact]
        public void ValidatorClassesShouldEndWithValidatorAndInheritAbstractValidator()
        {
            // Arrange
            var validatorTypes = ArchRuleDefinition.Classes()
                .That()
                .ResideInNamespace($"{ApplicationNamespace}.Services.*.Validators", true)
                .As("Validator Classes");

            // Act & Assert
            foreach (var type in validatorTypes.GetObjects(Architecture))
            {
                var assembly = Assembly.Load(ApplicationNamespace);
                var actualType = assembly.GetType(type.FullName);

                Assert.True(actualType != null && actualType.Name.EndsWith("Validator", StringComparison.Ordinal),
                    $"Type {type.FullName} should end with 'Validator'");
                Assert.True(actualType != null && actualType.BaseType != null && actualType.BaseType.Name.Contains("AbstractValidator", StringComparison.Ordinal),
                    $"Type {type.FullName} should inherit from AbstractValidator");
            }
        }

        #endregion

        #region Infrastructure Project Rules

        [Fact]
        public void ConfigurationClassesShouldEndWithConfigurationAndImplementIEntityTypeConfiguration()
        {
            // Arrange
            var configurationTypes = ArchRuleDefinition.Classes()
                .That()
                .ResideInNamespace($"{PersistenceNamespace}.Configuration", true)
                .As("Configuration Classes");

            // Act & Assert
            foreach (var type in configurationTypes.GetObjects(Architecture))
            {
                var assembly = Assembly.Load(PersistenceNamespace);
                var actualType = assembly.GetType(type.FullName);

                Assert.True(actualType != null && actualType.Name.EndsWith("Configuration", StringComparison.Ordinal),
                    $"Type {type.FullName} should end with 'Configuration'");
                Assert.True(actualType != null && actualType.GetInterfaces().Any(i => i.Name.Contains("IEntityTypeConfiguration", StringComparison.Ordinal)),
                    $"Type {type.FullName} should implement IEntityTypeConfiguration");
            }
        }

        #endregion

        private static bool IsInheritedFromObject(MethodInfo method)
        {
            return method.DeclaringType == typeof(object);
        }
    }
}
