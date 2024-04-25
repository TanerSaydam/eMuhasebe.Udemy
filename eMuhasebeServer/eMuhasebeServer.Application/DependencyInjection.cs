using eMuhasebeServer.Application.Behaviors;
using eMuhasebeServer.Domain.Entities;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace eMuhasebeServer.Application;
public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddFluentEmail("info@emuhasebe.com").AddSmtpSender("localhost",2525);

        services.AddAutoMapper(typeof(DependencyInjection).Assembly);

        services.AddMediatR(conf =>
        {
            conf.RegisterServicesFromAssemblies(
                typeof(DependencyInjection).Assembly, 
                typeof(AppUser).Assembly);
            conf.AddOpenBehavior(typeof(ValidationBehavior<,>));
        });

        services.AddValidatorsFromAssembly(typeof(DependencyInjection).Assembly);

        return services;
    }
}
