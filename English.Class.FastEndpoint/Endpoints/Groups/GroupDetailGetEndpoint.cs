using English.Class.Domain.Groups;
using English.Class.FastEndpoint.Extension;
using Microsoft.AspNetCore.Http;
using System.Net;

namespace English.Class.FastEndpoint.Endpoints.Groups;

public class GroupDetailGetEndpoint : Endpoint<GroupDetail>
{
    public override void Configure()
    {
        Get("group/{id}");
        Description(b => b
            .Produces<GroupDetail>(HttpStatusCode.OK.AsInt())
            .Produces(HttpStatusCode.NoContent.AsInt())
        );
        AllowAnonymous();
    }

    public override async Task HandleAsync(GroupDetail req, CancellationToken token)
    {
        var id = Route<Guid>("id", isRequired: true);
        var rep = Resolve<IGroupRepository>();
        var group = await rep.GetDetailAsync(id, token);
        await SendAsync(group, group != null ? HttpStatusCode.OK.AsInt() : HttpStatusCode.NoContent.AsInt(), cancellation: token);
    }
}