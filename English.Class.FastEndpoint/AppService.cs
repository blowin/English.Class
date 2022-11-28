using English.Class.DependencyInjection;
using English.Class.FastEndpoint.Services;
using English.Class.FastEndpoint.Services.ExceptionHandling.ExceptionHandler;
using FastEndpoints.Swagger;
using Microsoft.AspNetCore.Http.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace English.Class.FastEndpoint;

public class AppService
{
    private IServiceCollection _services;

    public AppService(IServiceCollection services)
    {
        _services = services;
    }

    public void Configure()
    {
        _services.AddFastEndpoints(e =>
        {
            e.IncludeAbstractValidators = true;
        });
        _services.AddSwaggerDoc(settings =>
        {
            settings.Title = "English";
            settings.Version = "v1";
        });
        _services.AddAppServices(optionsBuilder => optionsBuilder.UseSqlite("Data Source=app.db"));
        _services.Scan(e =>
        {
            e.FromAssemblyOf<Program>()
                .AddClasses(f => f.AssignableTo<IExceptionHandler>()).AsImplementedInterfaces().WithSingletonLifetime();
        });
        _services.AddSingleton<HealthCheck>();
    }
}