using System.Net;
using English.Class.Domain.Homeworks;
using English.Class.FastEndpoint.Extension;
using English.Class.FastEndpoint.Requests;
using Microsoft.AspNetCore.Http;

namespace English.Class.FastEndpoint.Endpoints.Homeworks;

public class HomeworkCreatePostEndpoint : Endpoint<CreateHomeworkRequest, RequestId>
{
    public override void Configure()
    {
        Post("homework");
        Description(b => b.Produces<Guid>(HttpStatusCode.Created.AsInt()));
        AllowAnonymous();
    }

    public override async Task HandleAsync(CreateHomeworkRequest req, CancellationToken token)
    {
        var rep = Resolve<IHomeworkRepository>();
        var response = await rep.CreateAsync(req, token);
        await SendAsync(RequestId.From(response.Id), HttpStatusCode.Created.AsInt(), cancellation: token);
    }
}