using Microsoft.AspNetCore.Http.HttpResults;
using Stackular.EmployeeManagement.Api.Extensions;
using Stackular.EmployeeManagement.Api.Infrastucture;
using Stackular.EmployeeManagement.Application.Services.Common.Dto;
using Stackular.EmployeeManagement.Application.Services.Employee.Commands;
using Stackular.EmployeeManagement.Application.Services.Employee.Contracts;
using Stackular.EmployeeManagement.Application.Services.Employee.Dto;
using Stackular.EmployeeManagement.Application.Services.Employee.Queries;

namespace Stackular.EmployeeManagement.Api.Endpoints
{
    public class Employees : IEndpointGroupBase
    {
        public void Map(WebApplication app)
        {
            app.MapGroup(this)
               .MapPost(SearchEmployees, "/SearchEmployees")
               .MapPost(SearchEmployeesByDepartmentName, "/SearchEmployeesByDepartmentName")
               .MapGet(GetEmployee, "/{id}")
               .MapPost(AddEmployee, "/")
               .MapPut(UpdateEmployee, "/{id}")
               .MapPost(GetPagedEmployees, "/GetPagedEmployees");
        }

        public static async Task<Results<CreatedAtRoute<EmployeeDto>, BadRequest>> AddEmployee(IEmployeeService service, AddEmployeeCommand command, CancellationToken ct)
        {
            var result = await service.AddEmployee(command, ct);
            return TypedResults.CreatedAtRoute(routeName: nameof(GetEmployee), routeValues: new { id = result.Id }, value: result);
        }

        public static async Task<NoContent> UpdateEmployee(IEmployeeService service, Guid id, UpdateEmployeeCommand command, CancellationToken ct)
        {
            await service.UpdateEmployee(id, command, ct);
            return TypedResults.NoContent();
        }

        public static async Task<Results<Ok<IEnumerable<EmployeeDto>>, BadRequest>> SearchEmployees(IEmployeeService service, SearchEmployeeQuery query, CancellationToken ct)
        {
            var results = await service.SearchEmployeesByName(query, ct);
            return TypedResults.Ok(results);
        }

        public static async Task<Results<Ok<IEnumerable<EmployeeDto>>, BadRequest>> SearchEmployeesByDepartmentName(IEmployeeService service, SearchEmployeesByDepartmentNameQuery query, CancellationToken ct)
        {
            var results = await service.GetEmployeesByDepartmentName(query, ct);
            return TypedResults.Ok(results);
        }

        public static async Task<Results<Ok<EmployeeDto>, NotFound>> GetEmployee(IEmployeeService service, Guid id, CancellationToken ct)
        {
            var result = await service.GetEmployee(id, ct);
            return TypedResults.Ok(result);
        }

        public static async Task<Results<Ok<PagedResponseDto<EmployeeDto>>, BadRequest>> GetPagedEmployees(IEmployeeService service, EmployeePagedQuery query, CancellationToken ct)
        {
            var result = await service.GetPagedEmployees(query, ct);
            return TypedResults.Ok(result);
        }
    }
}
