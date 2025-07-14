using FluentValidation.Results;
using Microsoft.Extensions.DependencyInjection;
using Stackular.EmployeeManagement.Application.Services.Common.Contracts;
using Stackular.EmployeeManagement.Application.Services.Common.Dto;
using System.Text.Json;

namespace Stackular.EmployeeManagement.Application.Services.Common
{
    public class ModelValidationService(IServiceProvider serviceProvider) : IModelValidationService
    {
        private readonly IServiceProvider _serviceProvider = serviceProvider;

        public void Validate<T>(T model)
        {
            var validator = _serviceProvider.GetRequiredService<IValidator<T>>();
            var validationResult = validator.Validate(model);

            if (!validationResult.IsValid)
            {
                FormatExceptionResult(validationResult);
            }
        }

        private static void FormatExceptionResult(ValidationResult validationResult)
        {
            List<ErrorDetailDto> validationErrors = [];

            foreach (var validationError in validationResult.Errors)
            {
                validationErrors.Add(new ErrorDetailDto { Field = validationError.PropertyName, Message = validationError.ErrorMessage });
            }

            throw new ValidationException(JsonSerializer.Serialize(validationErrors));
        }
    }
}
