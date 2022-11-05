using English.Class.FastEndpoint.Responses;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using System.Net;
using System.Text.Json;

namespace English.Class.FastEndpoint.ExceptionHandling.ExceptionHandler;

public class JsonFormatExceptionHandler : IExceptionHandler
{
    public bool CanHandle(IExceptionHandlerFeature feature, HttpContext ctx) 
        => feature.Error is JsonException && feature.Error.InnerException is FormatException;

    public Task HandleAsync(IExceptionHandlerFeature feature, HttpContext ctx)
    {
        var ex = (JsonException)feature.Error;

        ctx.Response.StatusCode = (int)HttpStatusCode.BadRequest;
        return ctx.Response.WriteAsJsonAsync(new ClientError
        {
            Status = "Bad format",
            Reason = $"{feature.Error.InnerException!.Message} ({ex.Path})"
        });
    }
}