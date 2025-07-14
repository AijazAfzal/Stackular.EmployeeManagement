using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.DependencyInjection;
using Stackular.EmployeeManagement.Application.Exceptions;
using Stackular.EmployeeManagement.Application.Interfaces.Infrastructure;
using Stackular.EmployeeManagement.Application.Interfaces.Persistance;
using Stackular.EmployeeManagement.Application.Services.Common;
using Stackular.EmployeeManagement.Application.Services.Common.Contracts;
using Stackular.EmployeeManagement.Application.Services.Employee;
using Stackular.EmployeeManagement.Application.Services.Employee.Commands;
using Stackular.EmployeeManagement.Application.Services.Employee.Validators;
using Stackular.EmployeeManagement.Domain.Entities;

namespace Stackular.EmployeeManagement.UnitTests
{
    public class EmployeeServiceTests
    {
        private readonly IApplicationDbContext _context;
        private readonly ModelValidationService _modelValidationService;
        private readonly EmployeeService _service;
        private readonly IEmailService _emailService;
        private readonly ISortingService _sortingService;

        public EmployeeServiceTests()
        {
            _context = Substitute.For<IApplicationDbContext>();
            _emailService = Substitute.For<IEmailService>();
            _sortingService = new SortingService();
            _modelValidationService = new ModelValidationService(null!);
            _service = new EmployeeService(_context, _modelValidationService, _emailService, _sortingService);
        }

        [Fact]
        public async Task AddEmployee_ShouldAddSuccessfully()
        {
            var services = new ServiceCollection();
            services.AddValidatorsFromAssemblyContaining<AddEmloyeeCommandValidator>();
            var provider = services.BuildServiceProvider();

            var context = Substitute.For<IApplicationDbContext>();
            var emailService = Substitute.For<IEmailService>();
            var sortingService = Substitute.For<ISortingService>();
            var modelValidationService = new ModelValidationService(provider);

            var service = new EmployeeService(context, modelValidationService, emailService, sortingService);

            var command = new AddEmployeeCommand
            {
                Name = "John Doe",
                EmailAddress = "john@example.com",
                Dob = DateTime.Today.AddYears(-25),
                DepartmentId = Guid.NewGuid()
            };

            var employee = new Domain.Entities.Employee
            {
                Id = Guid.NewGuid(),
                Name = command.Name,
                EmailAddress = command.EmailAddress,
                Dob = command.Dob,
                DepartmentId = command.DepartmentId
            };

            context.Employees.AddAsync(Arg.Any<Employee>(), Arg.Any<CancellationToken>())
                   .Returns(callInfo => ValueTask.FromResult<EntityEntry<Employee>?>(null));



            context.SaveChangesAsync(Arg.Any<CancellationToken>()).Returns(1);
            emailService.SendEmail().Returns(true);

            // Act
            var result = await service.AddEmployee(command, CancellationToken.None);

            // Assert
            result.ShouldNotBeNull();
            result.Name.ShouldBe("John Doe");
        }

        [Fact]
        public async Task GetEmployee_ShouldReturnCorrectEmployee()
        {
            var id = Guid.NewGuid();
            var employee = new Domain.Entities.Employee { Id = id, Name = "Jane Doe" };
            _context.Employees.FindAsync(Arg.Any<object[]>(), Arg.Any<CancellationToken>())
                .Returns(ValueTask.FromResult<Domain.Entities.Employee?>(employee));

            var result = await _service.GetEmployee(id, default);

            result.ShouldNotBeNull();
            result.Name.ShouldBe("Jane Doe");
        }

        [Fact]
        public async Task GetEmployee_InvalidId_ShouldThrowNotFound()
        {
            var id = Guid.NewGuid();
            _context.Employees.FindAsync(Arg.Any<object[]>(), Arg.Any<CancellationToken>())
                .Returns(ValueTask.FromResult<Domain.Entities.Employee?>(null));

            await Should.ThrowAsync<NotFoundException>(async () =>
                await _service.GetEmployee(id, default));
        }
    }
}
