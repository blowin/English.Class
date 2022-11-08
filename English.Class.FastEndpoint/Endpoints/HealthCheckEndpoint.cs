using English.Class.FastEndpoint.Extension;
using English.Class.FastEndpoint.Services;

namespace English.Class.FastEndpoint.Endpoints;

public class HealthCheckEndpoint : EndpointWithoutRequest<string>
{
    public override void Configure()
    {
        Get("health");
        AllowAnonymous();
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var service = Resolve<HealthCheck>();
        var result = await service.CheckHealthAsync(ct);
        await SendAsync(result.Message, result.Code.AsInt(), ct);
    }
}