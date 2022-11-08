using System.Net;
using English.Class.FastEndpoint.Services.ExceptionHandling.ExceptionHandler;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace English.Class.FastEndpoint.Services.ExceptionHandling;

// Disable BCC3001
public static class ExceptionHandlerExtensions
{
    public static IApplicationBuilder UseAppExceptionHandler(this IApplicationBuilder app, ILogger? logger = null, bool logStructuredException = false)
    {
        app.UseExceptionHandler(errApp =>
        {
            errApp.Run(async ctx => await HandleError(logger, logStructuredException, ctx));
        });

        return app;
    }

    private static Task HandleError(ILogger? logger, bool logStructuredException, HttpContext ctx)
    {
        var exHandlerFeature = ctx.Features.Get<IExceptionHandlerFeature>();
        if (exHandlerFeature is null)
            return Task.CompletedTask;

        ctx.Response.ContentType = "application/problem+json";
        foreach (var exceptionHandler in ctx.RequestServices.GetRequiredService<IEnumerable<IExceptionHandler>>())
        {
            if (exceptionHandler.CanHandle(exHandlerFeature, ctx))
                return exceptionHandler.HandleAsync(exHandlerFeature, ctx);
        }

        logger ??= ctx.RequestServices.GetRequiredService<ILogger<IExceptionHandlerFeature>>();
        LogUnhandledException(logger, logStructuredException, exHandlerFeature);

        ctx.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
        return ctx.Response.WriteAsJsonAsync(new InternalErrorResponse
        {
            Status = "Internal Server Error!",
            Code = ctx.Response.StatusCode,
            Reason = exHandlerFeature.Error.Message,
            Note = "See application log for stack trace."
        });
    }

    private static void LogUnhandledException(ILogger logger, bool logStructuredException, IExceptionHandlerFeature exHandlerFeature)
    {
        var http = exHandlerFeature.Endpoint?.DisplayName?.Split(" => ")[0];
        var type = exHandlerFeature.Error.GetType().Name;
        var error = exHandlerFeature.Error.Message;

        if (logStructuredException)
        {
            logger.LogError("{@http}{@type}{@reason}{@exception}", http, type, error, exHandlerFeature.Error);
            return;
        }

        var msg = $@"================================= 
{http} 
TYPE: {type} 
REASON: {error} 
--------------------------------- 
{exHandlerFeature.Error.StackTrace}";
        logger.LogError(msg);
    }
}