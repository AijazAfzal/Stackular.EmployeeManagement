using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Stackular.EmployeeManagement.Api.Extensions;
using Stackular.EmployeeManagement.Api.Infrastucture;
using Stackular.EmployeeManagement.Application.Services.Common;
using Stackular.EmployeeManagement.Application.Services.Common.Contracts;
using Stackular.EmployeeManagement.Application.Services.Common.Dto;
using Stackular.EmployeeManagement.Application.Services.Department.Commands;
using Stackular.EmployeeManagement.Application.Services.Department.Dto;
using Stackular.EmployeeManagement.Application.Services.Department.Queries;

namespace Stackular.EmployeeManagement.Api.Endpoints
{
    public class Departments : IEndpointGroupBase
    {
        public void Map(WebApplication app)
        {
            app.MapGroup(this)
               .MapPost(SearchDepartments, "/SearchDepartments") 
               .MapGet(GetDepartment, "/{Id}")
               .MapPost(AddDepartment, "/AddDepartment") 
               .MapPut(UpdateDepartment, "/UpdateDepartment")
               .MapPost(GetPagedDepartments, "/GetPagedDepartments")
               .MapDelete(DeleteDepartment, "/DeleteDepartment");
        }

        public static async Task<Results<CreatedAtRoute<DepartmentDto>, BadRequest>> AddDepartment(IRequestDispatcher dispatcher,[FromBody] AddDepartmentCommand command,CancellationToken ct)
        {
            var result = await dispatcher.DispatchAsync<AddDepartmentCommand, DepartmentDto>(command, ct);
            return TypedResults.CreatedAtRoute(routeName: nameof(GetDepartment), routeValues: new { id = result.Id }, value: result);
        }

        public static async Task<NoContent> UpdateDepartment(IRequestDispatcher dispatcher,[FromBody] UpdateDepartmentCommand command, CancellationToken ct)
        {
            await dispatcher.DispatchAsync<UpdateDepartmentCommand,Unit>(command, ct);
            return TypedResults.NoContent();
        }

        public static async Task<Results<Ok<IEnumerable<DepartmentDto>>, BadRequest>> SearchDepartments(IRequestDispatcher dispatcher, [FromBody] SearchDepartmentsQuery query, CancellationToken ct)
        {
            var result = await dispatcher.DispatchAsync<SearchDepartmentsQuery,IEnumerable<DepartmentDto>>(query, ct);
            return TypedResults.Ok(result);
        }

        public static async Task<Results<Ok<DepartmentDto>, NotFound>> GetDepartment(IRequestDispatcher dispatcher,[AsParameters] GetDepartmentByIdQuery query, CancellationToken ct)
        {
            var result = await dispatcher.DispatchAsync<GetDepartmentByIdQuery,DepartmentDto>(query, ct);
            return TypedResults.Ok(result);
        }

        public static async Task<Results<Ok<PagedResponseDto<DepartmentDto>>, BadRequest>> GetPagedDepartments(IRequestDispatcher dispatcher, [FromBody] GetDepartmentPagedQuery query, CancellationToken ct)
        {
            var result = await dispatcher.DispatchAsync<GetDepartmentPagedQuery,PagedResponseDto<DepartmentDto>>(query, ct);
            return TypedResults.Ok(result);
        }

        public static async Task<Results<NoContent, NotFound>> DeleteDepartment(IRequestDispatcher dispatcher,[FromBody] DeleteDepartmentCommand command, CancellationToken ct)
        {
             await dispatcher.DispatchAsync<DeleteDepartmentCommand,Unit>(command, ct);
             return TypedResults.NoContent();
        }
    }
}
