using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using PhoenixTask.Application.Core.Behaviors;
using System.Reflection;

namespace PhoenixTask.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {

        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

        ValidatorOptions.Global.DefaultRuleLevelCascadeMode = CascadeMode.Continue;
        ValidatorOptions.Global.DefaultClassLevelCascadeMode = CascadeMode.Stop;

        services.AddMediatR(config =>
        {
            config.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly());

            config.AddOpenBehavior(typeof(ValidationBehaviour<,>));
        });
        return services;
    }
}