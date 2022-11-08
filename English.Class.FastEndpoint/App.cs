using English.Class.DependencyInjection;
using English.Class.FastEndpoint.Services;
using English.Class.FastEndpoint.Services.ExceptionHandling;
using English.Class.FastEndpoint.Services.ExceptionHandling.ExceptionHandler;
using English.Class.Infrastructure.Database;
using FastEndpoints.Swagger;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace English.Class.FastEndpoint;

public class App
{
    private readonly WebApplication _app;

    public App(WebApplication app)
    {
        _app = app;
    }

    public App AddMiddleware()
    {
        _app.UseAppExceptionHandler();
        _app.UseAuthorization();
        _app.UseFastEndpoints(c =>
        {
            c.Endpoints.RoutePrefix = "api";
        });
        _app.UseOpenApi();
        _app.UseSwaggerUi3(s => s.ConfigureDefaults());
        return this;
    }

    public App Run()
    {
        BeforeRun();
        _app.Run();
        return this;
    }

    private void BeforeRun()
    {
        using var serviceScope = _app.Services.CreateScope();
        var appDbContext = serviceScope.ServiceProvider.GetRequiredService<AppDbContext>();
        appDbContext.Database.EnsureCreated();
    }
}