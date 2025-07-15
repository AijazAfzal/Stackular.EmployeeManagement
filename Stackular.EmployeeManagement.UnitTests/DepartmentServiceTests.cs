using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.DependencyInjection;
using NSubstitute;
using Stackular.EmployeeManagement.Application.Exceptions;
using Stackular.EmployeeManagement.Application.Interfaces.Infrastructure;
using Stackular.EmployeeManagement.Application.Interfaces.Persistance;
using Stackular.EmployeeManagement.Application.Services.Common;
using Stackular.EmployeeManagement.Application.Services.Common.Contracts;
using Stackular.EmployeeManagement.Application.Services.Department;
using Stackular.EmployeeManagement.Application.Services.Department.Commands;
using Stackular.EmployeeManagement.Application.Services.Department.Validators;
using Stackular.EmployeeManagement.Domain.Entities;

namespace Stackular.EmployeeManagement.UnitTests
{
    public class DepartmentServiceTests
    {
        private readonly IApplicationDbContext _context;
        private readonly ModelValidationService _modelValidationService;
        private readonly DepartmentService _service;
        private readonly IEmailService _emailService;
        private readonly ISortingService _sortingService;

        public DepartmentServiceTests()
        {
            _context = Substitute.For<IApplicationDbContext>();
            _emailService = Substitute.For<IEmailService>();
            _sortingService = new SortingService();
            _modelValidationService = new ModelValidationService(null!);
            _service = new DepartmentService(_context, _modelValidationService, _emailService, _sortingService);
        }

        [Fact]
        public async Task AddDepartment_ShouldAddSuccessfully()
        {
            // Arrange
            var services = new ServiceCollection();
            services.AddValidatorsFromAssemblyContaining<AddDepartmentCommandValidator>();
            var provider = services.BuildServiceProvider();

            var context = Substitute.For<IApplicationDbContext>();
            var emailService = Substitute.For<IEmailService>();
            var sortingService = Substitute.For<ISortingService>();
            var modelValidationService = new ModelValidationService(provider);

            var service = new DepartmentService(context, modelValidationService, emailService, sortingService);

            var command = new AddDepartmentCommand { Name = "HR" };
            var department = new Department { Id = Guid.NewGuid(), Name = "HR" };

            context.Departments.AddAsync(Arg.Any<Department>(), Arg.Any<CancellationToken>())
                               .Returns(callInfo => ValueTask.FromResult<EntityEntry<Department>?>(null));



            context.SaveChangesAsync(Arg.Any<CancellationToken>()).Returns(1);
            emailService.SendEmail().Returns(true);

            // Act
            var result = await service.AddDepartment(command, CancellationToken.None);

            // Assert
            result.ShouldNotBeNull();
            result.Name.ShouldBe("HR");
        }


        [Fact]
        public async Task GetDepartmentById_ShouldReturnCorrectDepartment()
        {
            var id = Guid.NewGuid();
            var department = new Domain.Entities.Department { Id = id, Name = "Finance" };
            _context.Departments.FindAsync(Arg.Any<object[]>(), Arg.Any<CancellationToken>())
                .Returns(ValueTask.FromResult<Domain.Entities.Department?>(department));


            var result = await _service.GetDepartmentById(id, default);

            result.ShouldNotBeNull();
            result.Name.ShouldBe("Finance");
        }

        [Fact]
        public async Task GetDepartmentById_InvalidId_ShouldThrowNotFound()
        {
            var id = Guid.NewGuid();
            _context.Departments.FindAsync(Arg.Any<object[]>(), Arg.Any<CancellationToken>())
                .Returns(ValueTask.FromResult<Domain.Entities.Department>(null));

            await Should.ThrowAsync<NotFoundException>(async () =>
                await _service.GetDepartmentById(id, default));
        }

        [Fact]
        public async Task DeleteDepartment_ShouldDeleteSuccessfully()
        {
            // Arrange
            var id = Guid.NewGuid();
            var department = new Department { Id = id, Name = "ToDelete" };
            _context.Departments.FindAsync(Arg.Any<object[]>(), Arg.Any<CancellationToken>())
                .Returns(ValueTask.FromResult<Department?>(department));
            _context.Departments.Remove(department);
            _context.SaveChangesAsync(Arg.Any<CancellationToken>()).Returns(1);

            // Act
            await _service.DeleteDepartment(id, default);

            // Assert
            _context.Received().Departments.Remove(department);
            await _context.Received().SaveChangesAsync(Arg.Any<CancellationToken>());
        }

        [Fact]
        public async Task DeleteDepartment_InvalidId_ShouldThrowNotFound()
        {
            // Arrange
            var id = Guid.NewGuid();
            _context.Departments.FindAsync(Arg.Any<object[]>(), Arg.Any<CancellationToken>())
                .Returns(ValueTask.FromResult<Department>(null));

            // Act & Assert
            await Should.ThrowAsync<NotFoundException>(async () =>
                await _service.DeleteDepartment(id, default));
        }
    }
}
