using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;

namespace English.Class.FastEndpoint.ExceptionHandling.ExceptionHandler;

public interface IExceptionHandler
{
    bool CanHandle(IExceptionHandlerFeature feature, HttpContext ctx);
    Task HandleAsync(IExceptionHandlerFeature feature, HttpContext ctx);
}