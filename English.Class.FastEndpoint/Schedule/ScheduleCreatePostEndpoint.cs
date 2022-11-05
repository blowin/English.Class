using System.Net;
using English.Class.Domain.Schedules;
using English.Class.FastEndpoint.Extension;
using English.Class.FastEndpoint.Requests;
using Microsoft.AspNetCore.Http;

namespace English.Class.FastEndpoint.Schedule;

public class ScheduleCreatePostEndpoint : Endpoint<CreateScheduleRequest, RequestId>
{
    public override void Configure()
    {
        Post("schedule");
        Description(b => b.Produces<Guid>(HttpStatusCode.Created.AsInt()));
        AllowAnonymous();
    }

    public override async Task HandleAsync(CreateScheduleRequest req, CancellationToken token)
    {
        var rep = Resolve<IScheduleRepository>();
        var response = await rep.CreateAsync(req, token);
        await SendAsync(RequestId.From(response.Id), HttpStatusCode.Created.AsInt(), cancellation: token);
    }
}