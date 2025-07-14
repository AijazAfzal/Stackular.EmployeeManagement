using Microsoft.AspNetCore.Diagnostics;
using Stackular.EmployeeManagement.Application.Exceptions;
using Stackular.EmployeeManagement.Application.Services.Common.Dto;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Text.Json;

namespace Stackular.EmployeeManagement.Api.Extensions
{
    public static class GlobalExceptionHandlerExtensions
    {
        public static void ConfigureGlobalExceptionHandler(this WebApplication app)
        {
            app.UseExceptionHandler(exceptionHandlerApp =>
            {
                exceptionHandlerApp.Run(async context =>
                {
                    var exception = GetExceptionFromContext(context);
                    if (exception == null) return;

                    context.Response.ContentType = "application/json";
                    var response = CreateBaseErrorResponse();
                    SetResponseStatusAndMessage(context, exception, response);
                    await context.Response.WriteAsJsonAsync(response);
                });
            });
        }

        private static Exception? GetExceptionFromContext(HttpContext context)
        {
            var exceptionHandlerFeature = context.Features.Get<IExceptionHandlerFeature>();
            return exceptionHandlerFeature?.Error;
        }

        private static ApiErrorDto CreateBaseErrorResponse()
        {
            return new ApiErrorDto
            {
                Success = false,
                Errors = new List<ErrorDetailDto>()
            };
        }

        private static void SetResponseStatusAndMessage(HttpContext context, Exception exception, ApiErrorDto response)
        {
            switch (exception)
            {
                case ValidationException validationException:
                    context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    response.Message = "Validation failed";
                    response.Errors = JsonSerializer.Deserialize<List<ErrorDetailDto>>(validationException.Message)!;
                    break;

                case BadRequestException badRequestException:
                    context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    response.Message = badRequestException.Message;
                    break;

                case NotFoundException notFoundException:
                    context.Response.StatusCode = (int)HttpStatusCode.NotFound;
                    response.Message = notFoundException.Message;
                    break;

                case UnauthorizedException unauthorizedException:
                    context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                    response.Message = unauthorizedException.Message;
                    break;

                case ForbiddenException forbiddenException:
                    context.Response.StatusCode = (int)HttpStatusCode.Forbidden;
                    response.Message = forbiddenException.Message;
                    break;

                case DatabaseException databaseException:
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    response.Message = databaseException.Message;
                    break;

                case DependencyFailureException dependencyFailureException:
                    context.Response.StatusCode = (int)HttpStatusCode.FailedDependency;
                    response.Message = dependencyFailureException.Message;
                    break;

                default:
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    response.Message = "An unexpected error occurred. Please try again later.";
                    break;
            }

            response.StatusCode = context.Response.StatusCode;
        }
    }
}
