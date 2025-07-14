using Microsoft.AspNetCore.Http.HttpResults;
using Stackular.EmployeeManagement.Api.Extensions;
using Stackular.EmployeeManagement.Api.Infrastucture;
using Stackular.EmployeeManagement.Application.Services.Common.Dto;
using Stackular.EmployeeManagement.Application.Services.Department.Commands;
using Stackular.EmployeeManagement.Application.Services.Department.Contracts;
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
               .MapGet(GetDepartment, "/{id}")
               .MapPost(AddDepartment, "/") 
               .MapPut(UpdateDepartment, "/{id}")
               .MapPost(GetPagedDepartments, "/GetPagedDepartments");
        }

        public static async Task<Results<CreatedAtRoute<DepartmentDto>, BadRequest>> AddDepartment(IDepartmentService service, AddDepartmentCommand command, CancellationToken ct)
        {
            var result = await service.AddDepartment(command, ct);
            return TypedResults.CreatedAtRoute(routeName: nameof(GetDepartment), routeValues: new { id = result.Id }, value: result);
        }

        public static async Task<NoContent> UpdateDepartment(IDepartmentService service, Guid id, UpdateDepartmentCommand command, CancellationToken ct)
        {
            await service.UpdateDepartment(id, command, ct);
            return TypedResults.NoContent();
        }

        public static async Task<Results<Ok<IEnumerable<DepartmentDto>>, BadRequest>> SearchDepartments(IDepartmentService service, SearchDepartmentsQuery query, CancellationToken ct)
        {
            var results = await service.SearchDepartmentsByName(query, ct);
            return TypedResults.Ok(results);
        }

        public static async Task<Results<Ok<DepartmentDto>, NotFound>> GetDepartment(IDepartmentService service, Guid id, CancellationToken ct)
        {
            var result = await service.GetDepartmentById(id, ct);
            return TypedResults.Ok(result);
        }

        public static async Task<Results<Ok<PagedResponseDto<DepartmentDto>>, BadRequest>> GetPagedDepartments(IDepartmentService service, DepartmentPagedQuery query, CancellationToken ct)
        {
            var result = await service.GetPagedDepartments(query, ct);
            return TypedResults.Ok(result);
        }
    }
}
