using System.Net;

namespace English.Class.FastEndpoint.Services;

public class HealthCheck
{
    public ValueTask<(string Message, HttpStatusCode Code)> CheckHealthAsync(CancellationToken token = default)
    {
        return new ValueTask<(string, HttpStatusCode)>(("Alive", HttpStatusCode.OK));
    }
}