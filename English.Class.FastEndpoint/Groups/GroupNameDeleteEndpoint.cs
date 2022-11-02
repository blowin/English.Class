using English.Class.Domain.Groups;
using System.Net;
using English.Class.FastEndpoint.Extension;
using FluentValidation.Results;
using Microsoft.AspNetCore.Http;
using Group = English.Class.Domain.Groups.Group;

namespace English.Class.FastEndpoint.Groups;

public class GroupNameDeleteEndpoint : Endpoint<GroupNameDeleteEndpoint.Request, Group?>
{
    public override void Configure()
    {
        Delete("group");
        Description(b => b
            .Produces<Group>(HttpStatusCode.OK.AsInt())
            .ProducesProblem(HttpStatusCode.BadRequest.AsInt())
        );
        AllowAnonymous();
    }

    public override async Task HandleAsync(Request req, CancellationToken token)
    {
        var rep = Resolve<IGroupRepository>();
        var response = await rep.DeleteAsync(req.Id, token);
        if(response == null)
            AddError(e => e.Id, $"Not found group with id='{req.Id}'");

        ThrowIfAnyErrors();

        await SendAsync(response, cancellation: token);
    }

    public class Request
    {
        public Guid Id { get; set; }
    }
}