using English.Class.Domain.Groups;
using System.Net;
using English.Class.FastEndpoint.Extension;
using Microsoft.AspNetCore.Http;
using YamlDotNet.Core.Tokens;

namespace English.Class.FastEndpoint.Groups;

public class GroupNamePostEndpoint : Endpoint<GroupNamePostEndpoint.Request, Guid>
{
    public override void Configure()
    {
        Post("group");
        Description(b => b.Produces<Guid>(HttpStatusCode.Created.AsInt()));
        AllowAnonymous();
    }

    public override async Task HandleAsync(Request req, CancellationToken token)
    {
        var rep = Resolve<IGroupRepository>();
        var response = await rep.CreateAsync(req.Name, token);
        await SendAsync(response.Id, HttpStatusCode.Created.AsInt(), cancellation: token);
    }

    public class Request
    {
        public string Name { get; set; } = string.Empty;
    }
}