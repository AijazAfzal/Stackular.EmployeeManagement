using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Stackular.EmployeeManagement.Api.Extensions;
using Stackular.EmployeeManagement.Api.Infrastucture;
using Stackular.EmployeeManagement.Application.Services.Common;
using Stackular.EmployeeManagement.Application.Services.Common.Contracts;
using Stackular.EmployeeManagement.Application.Services.Common.Dto;
using Stackular.EmployeeManagement.Application.Services.Employee.Commands;
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
               .MapPost(AddEmployee, "/AddEmployee")
               .MapPut(UpdateEmployee, "/UpdateEmployee")
               .MapPost(GetPagedEmployees, "/GetPagedEmployees")
               .MapDelete(DeleteEmployee, "/DeleteEmployee");
        }

        public static async Task<Results<CreatedAtRoute<EmployeeDto>, BadRequest>> AddEmployee(IRequestDispatcher dispatcher,[FromBody] AddEmployeeCommand command, CancellationToken ct)
        {
            var result = await dispatcher.DispatchAsync<AddEmployeeCommand,EmployeeDto>(command, ct);
            return TypedResults.CreatedAtRoute(routeName: nameof(GetEmployee), routeValues: new { id = result.Id }, value: result);
        }

        public static async Task<NoContent> UpdateEmployee(IRequestDispatcher dispatcher,[FromBody] UpdateEmployeeCommand command, CancellationToken ct)
        {
            await dispatcher.DispatchAsync<UpdateEmployeeCommand,Unit>(command, ct);
            return TypedResults.NoContent();
        }

        public static async Task<Results<Ok<IEnumerable<EmployeeDto>>, BadRequest>> SearchEmployees(IRequestDispatcher dispatcher,[FromBody] SearchEmployeeQuery query, CancellationToken ct)
        {
            var results = await dispatcher.DispatchAsync<SearchEmployeeQuery,IEnumerable<EmployeeDto>>(query, ct);
            return TypedResults.Ok(results);
        }

        public static async Task<Results<Ok<IEnumerable<EmployeeDto>>, BadRequest>> SearchEmployeesByDepartmentName(IRequestDispatcher dispatcher, [FromBody] SearchEmployeesByDepartmentNameQuery query, CancellationToken ct)
        {
            var results = await dispatcher.DispatchAsync<SearchEmployeesByDepartmentNameQuery,IEnumerable<EmployeeDto>>(query, ct);
            return TypedResults.Ok(results);
        }

        public static async Task<Results<Ok<EmployeeDto>, NotFound>> GetEmployee(IRequestDispatcher dispatcher,[AsParameters] GetEmployeeByIdQuery query, CancellationToken ct)
        {
            var result = await dispatcher.DispatchAsync<GetEmployeeByIdQuery,EmployeeDto>(query, ct);
            return TypedResults.Ok(result);
        }

        public static async Task<Results<Ok<PagedResponseDto<EmployeeDto>>, BadRequest>> GetPagedEmployees(IRequestDispatcher dispatcher,[FromBody] GetEmployeePagedQuery query, CancellationToken ct)
        {
            var result = await dispatcher.DispatchAsync<GetEmployeePagedQuery,PagedResponseDto<EmployeeDto>>(query, ct);
            return TypedResults.Ok(result);
        }

        public static async Task<Results<NoContent, NotFound>> DeleteEmployee(IRequestDispatcher dispatcher,[FromBody] DeleteEmployeeCommand command, CancellationToken ct)
        {
            await dispatcher.DispatchAsync<DeleteEmployeeCommand,Unit>(command, ct);
            return TypedResults.NoContent();    
        }
    }
}
