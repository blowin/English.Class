using System.Net;
using English.Class.Domain.Groups;
using English.Class.FastEndpoint.Extension;
using Microsoft.AspNetCore.Http;

namespace English.Class.FastEndpoint.Endpoints.Groups;

public class GroupRenamePutEndpoint : Endpoint<GroupRenamePutEndpoint.Request, GroupRenamePutEndpoint.Request>
{
    public override void Configure()
    {
        Put("group/rename");
        Description(b => b
            .Produces<GroupRenamePutEndpoint.Request>(HttpStatusCode.OK.AsInt())
            .ProducesProblem(HttpStatusCode.BadRequest.AsInt())
        );
        AllowAnonymous();
    }

    public override async Task HandleAsync(Request req, CancellationToken token)
    {
        var rep = Resolve<IGroupRepository>();
        var response = await rep.RenameAsync(req.Id, req.Name, token);
        if (response == null)
            AddError(e => e.Id, $"Not found group with id='{req.Id}'");

        ThrowIfAnyErrors();
        await SendAsync(req, cancellation: token);
    }

    public class Request
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
    }
}