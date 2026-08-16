using FluentValidation;
using Irminsul.Application.DTos.Validators;
using Microsoft.Extensions.DependencyInjection;
using Irminsul.Application.DTos.Validators;

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