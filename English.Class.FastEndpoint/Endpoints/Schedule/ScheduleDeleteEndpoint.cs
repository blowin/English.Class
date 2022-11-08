using System.Net;
using English.Class.Domain.Schedules;
using English.Class.Domain.Students;
using English.Class.FastEndpoint.Extension;
using English.Class.FastEndpoint.Requests;
using Microsoft.AspNetCore.Http;

namespace English.Class.FastEndpoint.Endpoints.Schedule;

public class ScheduleDeleteEndpoint : Endpoint<RequestId, ScheduleDeleteEndpoint.EResponse>
{
    public override void Configure()
    {
        Delete("schedule");
        Description(b => b
            .Produces<Student>(HttpStatusCode.OK.AsInt())
            .ProducesProblem(HttpStatusCode.BadRequest.AsInt())
        );
        AllowAnonymous();
    }

    public override async Task HandleAsync(RequestId req, CancellationToken token)
    {
        var rep = Resolve<IScheduleRepository>();
        var response = await rep.DeleteAsync(req.Id, token);
        if (response == null)
            AddError(e => e.Id, $"Not found student with id='{req.Id}'");

        ThrowIfAnyErrors();

        await SendAsync(new EResponse(response!), cancellation: token);
    }

    public class EResponse
    {
        public Guid Id { get; set; }
        public DateTime Time { get; set; }

        public EResponse(Domain.Schedules.Schedule student)
        {
            Id = student.Id;
            Time = student.Time;
        }
    }
}