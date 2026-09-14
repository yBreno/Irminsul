using FluentValidation;
using Irminsul.Application.DTos.Validators;
using Microsoft.Extensions.DependencyInjection;

namespace Irminsul.Api.Extensions;

public static class ValidationExtensions
{
    public static IServiceCollection AddApplicationValidation(
        this IServiceCollection services)
    {
        services.AddValidatorsFromAssemblyContaining<CreateCharacterDtoValidator>();


        return services;
    }
}