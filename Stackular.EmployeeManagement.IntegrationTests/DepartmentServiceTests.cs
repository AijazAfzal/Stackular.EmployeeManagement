using Stackular.EmployeeManagement.Application.Exceptions;
using Stackular.EmployeeManagement.Application.Services.Department.Commands;
using Stackular.EmployeeManagement.Application.Services.Department.Contracts;
using Stackular.EmployeeManagement.Application.Services.Department.Queries;
using Stackular.EmployeeManagement.IntegrationTests.Infrastructure;

namespace Stackular.EmployeeManagement.IntegrationTests
{
    public class DepartmentServiceTests : BaseIntegrationTest
    {
        private readonly IDepartmentService _departmentService;

        public DepartmentServiceTests(IntegrationTestWebAppFactory factory) : base(factory)
        {
            _departmentService = _scope.ServiceProvider.GetRequiredService<IDepartmentService>();
        }

        [Fact]
        public async Task AddDepartment_ShouldAddDepartmentToDatabase()
        {
            // Arrange
            var command = new AddDepartmentCommand { Name = "Research" };

            // Act
            var result = await _departmentService.AddDepartment(command, CancellationToken.None);

            // Assert
            result.ShouldNotBeNull();
            result.Name.ShouldBe(command.Name);

            var dbItem = await _dbContext.Departments.FindAsync(result.Id);
            dbItem.ShouldNotBeNull();
            dbItem.Name.ShouldBe(command.Name);
        }

        [Fact]
        public async Task GetDepartmentById_ShouldReturnDepartment()
        {
            // Arrange
            var created = await _departmentService.AddDepartment(new AddDepartmentCommand { Name = "Finance" }, CancellationToken.None);

            // Act
            var result = await _departmentService.GetDepartmentById(created.Id, CancellationToken.None);

            // Assert
            result.ShouldNotBeNull();
            result.Id.ShouldBe(created.Id);
            result.Name.ShouldBe("Finance");
        }

        [Fact]
        public async Task GetDepartmentById_ShouldThrow_WhenDepartmentNotFound()
        {
            await Should.ThrowAsync<NotFoundException>(async () =>
            {
                await _departmentService.GetDepartmentById(Guid.NewGuid(), CancellationToken.None);
            });
        }

        [Fact]
        public async Task UpdateDepartment_ShouldUpdateDepartment()
        {
            // Arrange
            var created = await _departmentService.AddDepartment(new AddDepartmentCommand { Name = "Operations" }, CancellationToken.None);
            var updateCommand = new UpdateDepartmentCommand { Name = "Operations Updated" };

            // Act
            await _departmentService.UpdateDepartment(created.Id, updateCommand, CancellationToken.None);

            // Assert
            var updated = await _dbContext.Departments.FindAsync(created.Id);
            updated.ShouldNotBeNull();
            updated.Name.ShouldBe(updateCommand.Name);
        }

        [Fact]
        public async Task UpdateDepartment_ShouldThrow_WhenNotFound()
        {
            var updateCommand = new UpdateDepartmentCommand { Name = "Invalid Update" };

            await Should.ThrowAsync<NotFoundException>(async () =>
            {
                await _departmentService.UpdateDepartment(Guid.NewGuid(), updateCommand, CancellationToken.None);
            });
        }

        [Fact]
        public async Task SearchDepartmentsByName_ShouldReturnMatches()
        {
            await _departmentService.AddDepartment(new AddDepartmentCommand { Name = "HR test" }, CancellationToken.None);
            await _departmentService.AddDepartment(new AddDepartmentCommand { Name = "HR Department" }, CancellationToken.None);
            await _departmentService.AddDepartment(new AddDepartmentCommand { Name = "IT" }, CancellationToken.None);

            var result = await _departmentService.SearchDepartmentsByName(new SearchDepartmentsQuery
            {
                Name = "HR",
                MaxDepartments = 10
            }, CancellationToken.None);

            result.ShouldNotBeNull();
            result.Count().ShouldBe(2);
        }

        [Fact]
        public async Task GetPagedDepartments_ShouldReturnPagedResults()
        {
            await _departmentService.AddDepartment(new AddDepartmentCommand { Name = "D1" }, CancellationToken.None);
            await _departmentService.AddDepartment(new AddDepartmentCommand { Name = "D2" }, CancellationToken.None);
            await _departmentService.AddDepartment(new AddDepartmentCommand { Name = "D3" }, CancellationToken.None);

            var pagedResult = await _departmentService.GetPagedDepartments(new DepartmentPagedQuery
            {
                PageNumber = 1,
                PageSize = 2,
                SortBy = "Name",
                SearchTerm = "D"
            }, CancellationToken.None);

            pagedResult.ShouldNotBeNull();
            pagedResult.Items.Count().ShouldBe(2);
            pagedResult.TotalCount.ShouldBeGreaterThanOrEqualTo(3);
        }

        [Fact]
        public async Task AddDepartment_ShouldFailValidation_WhenNameEmpty()
        {
            var command = new AddDepartmentCommand { Name = "" };

            await Should.ThrowAsync<ValidationException>(async () =>
            {
                await _departmentService.AddDepartment(command, CancellationToken.None);
            });
        }

        [Fact]
        public async Task DepartmentPagedQuery_ShouldFailValidation_WhenPageSizeInvalid()
        {
            var query = new DepartmentPagedQuery
            {
                PageNumber = 1,
                PageSize = 0
            };

            await Should.ThrowAsync<ValidationException>(async () =>
            {
                await _departmentService.GetPagedDepartments(query, CancellationToken.None);
            });
        }

        [Fact]
        public async Task SearchDepartments_ShouldFailValidation_WhenMaxDepartmentsInvalid()
        {
            var query = new SearchDepartmentsQuery
            {
                Name = "Test",
                MaxDepartments = 0
            };

            await Should.ThrowAsync<ValidationException>(async () =>
            {
                await _departmentService.SearchDepartmentsByName(query, CancellationToken.None);
            });
        }
    }
}
