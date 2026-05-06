using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace RetailSystem.Application.Common.Validators
{
    public static class ValidationExtensions
    {
        public static async Task ValidateAndThrowAsync<T>(this IServiceProvider serviceProvider, T dto)
        {
            var validator = serviceProvider.GetService<IValidator<T>>();
            if (validator != null)
            {
               var result  = await validator.ValidateAsync(dto);
                if (!result.IsValid)
                {
                    throw new ValidationException(result.Errors);
                }
            }
        }
    }
}
