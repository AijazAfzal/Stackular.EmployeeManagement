using Stackular.EmployeeManagement.Application.Services.Department.Commands;
using Stackular.EmployeeManagement.Application.Services.Department.Contracts;
using Stackular.EmployeeManagement.Application.Services.Department.Dto;
using Stackular.EmployeeManagement.Application.Services.Employee.Commands;
using Stackular.EmployeeManagement.Application.Services.Employee.Contracts;
using Stackular.EmployeeManagement.Application.Services.Employee.Queries;
using Stackular.EmployeeManagement.IntegrationTests.Infrastructure;

namespace Stackular.EmployeeManagement.IntegrationTests
    {
        public class EmployeeServiceTests : BaseIntegrationTest
        {
            private readonly IEmployeeService _employeeService;

            public EmployeeServiceTests(IntegrationTestWebAppFactory factory) : base(factory)
            {
                _employeeService = _scope.ServiceProvider.GetRequiredService<IEmployeeService>();
            }

            [Fact]
            public async Task AddEmployee_ShouldCreateNewEmployee()
            {
                // Arrange
                var department = await CreateTestDepartmentAsync(); 

                var command = new AddEmployeeCommand
                {
                    Name = "Test User",
                    EmailAddress = "test.user@example.com",
                    Dob = new DateTime(1990, 1, 1),
                    DepartmentId = department.Id
                };

                // Act
                var result = await _employeeService.AddEmployee(command, CancellationToken.None);

                // Assert
                result.ShouldNotBeNull();
                result.Name.ShouldBe(command.Name);
                result.EmailAddress.ShouldBe(command.EmailAddress);
            }

            [Fact]
            public async Task GetEmployee_ShouldReturnCorrectEmployee()
            {
                // Arrange
                var department = await CreateTestDepartmentAsync();
                var added = await _employeeService.AddEmployee(new AddEmployeeCommand
                {
                    Name = "Get User",
                    EmailAddress = "get.user@example.com",
                    Dob = new DateTime(1985, 5, 5),
                    DepartmentId = department.Id
                }, CancellationToken.None);

                // Act
                var fetched = await _employeeService.GetEmployee(added.Id, CancellationToken.None);

                // Assert
                fetched.ShouldNotBeNull();
                fetched.Name.ShouldBe(added.Name);
            }

            [Fact]
            public async Task SearchEmployeesByName_ShouldReturnMatches()
            {
                // Arrange
                var department = await CreateTestDepartmentAsync();
                await _employeeService.AddEmployee(new AddEmployeeCommand
                {
                    Name = "Match Name",
                    EmailAddress = "match@example.com",
                    Dob = new DateTime(1995, 1, 1),
                    DepartmentId = department.Id
                }, CancellationToken.None);

                // Act
                var results = await _employeeService.SearchEmployeesByName(new SearchEmployeeQuery
                {
                    Name = "Match",
                    MaxEmployees = 5
                }, CancellationToken.None);

                // Assert
                results.ShouldNotBeNull();
                results.Any(e => e.Name.Contains("Match")).ShouldBeTrue();
            }

            [Fact]
            public async Task UpdateEmployee_ShouldUpdateEmployeeDetails()
            {
                // Arrange
                var department = await CreateTestDepartmentAsync();
                var added = await _employeeService.AddEmployee(new AddEmployeeCommand
                {
                    Name = "Before Update",
                    EmailAddress = "before@example.com",
                    Dob = new DateTime(1990, 2, 2),
                    DepartmentId = department.Id
                }, CancellationToken.None);

                var update = new UpdateEmployeeCommand
                {
                    Name = "After Update",
                    EmailAddress = "after@example.com",
                    Dob = new DateTime(1990, 2, 2),
                    DepartmentId = department.Id
                };

                // Act
                await _employeeService.UpdateEmployee(added.Id, update, CancellationToken.None);

                // Assert
                var updated = await _employeeService.GetEmployee(added.Id, CancellationToken.None);
                updated.Name.ShouldBe("After Update");
                updated.EmailAddress.ShouldBe("after@example.com");
            }

            [Fact]
            public async Task DeleteEmployee_ShouldDeleteEmployee()
            {
                // Arrange
                var department = await CreateTestDepartmentAsync();
                var added = await _employeeService.AddEmployee(new AddEmployeeCommand
                {
                    Name = "ToDelete",
                    EmailAddress = "delete@example.com",
                    Dob = new DateTime(1991, 3, 3),
                    DepartmentId = department.Id
                }, CancellationToken.None);

                // Act
                await _employeeService.DeleteEmployee(added.Id, CancellationToken.None);

                // Assert
                await Should.ThrowAsync<Stackular.EmployeeManagement.Application.Exceptions.NotFoundException>(async () =>
                {
                    await _employeeService.GetEmployee(added.Id, CancellationToken.None);
                });
            }

            [Fact]
            public async Task DeleteEmployee_ShouldThrow_WhenNotFound()
            {
                await Should.ThrowAsync<Stackular.EmployeeManagement.Application.Exceptions.NotFoundException>(async () =>
                {
                    await _employeeService.DeleteEmployee(Guid.NewGuid(), CancellationToken.None);
                });
            }

            private async Task<DepartmentDto> CreateTestDepartmentAsync()
            {
                var deptService = _scope.ServiceProvider.GetRequiredService<IDepartmentService>();
                return await deptService.AddDepartment(new AddDepartmentCommand
                {
                    Name = "HR-" + Guid.NewGuid().ToString("N")[..6]
                }, CancellationToken.None);
            }
        }
}
