using System.Net;
using English.Class.Domain.Homeworks;
using English.Class.Domain.Students;
using English.Class.FastEndpoint.Extension;
using Microsoft.AspNetCore.Http;

namespace English.Class.FastEndpoint.Homeworks;

public class HomeworkCreatePostEndpoint : Endpoint<CreateHomeworkRequest, Guid>
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
        await SendAsync(response.Id, HttpStatusCode.Created.AsInt(), cancellation: token);
    }
}