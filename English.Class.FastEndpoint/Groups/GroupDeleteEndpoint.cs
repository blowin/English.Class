using English.Class.Domain.Groups;
using System.Net;
using English.Class.FastEndpoint.Extension;
using Microsoft.AspNetCore.Http;
using Group = English.Class.Domain.Groups.Group;
using English.Class.FastEndpoint.Requests;

namespace English.Class.FastEndpoint.Groups;

public class GroupDeleteEndpoint : Endpoint<RequestId, GroupDeleteEndpoint.EResponse>
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

    public override async Task HandleAsync(RequestId req, CancellationToken token)
    {
        var rep = Resolve<IGroupRepository>();
        var response = await rep.DeleteAsync(req.Id, token);
        if(response == null)
            AddError(e => e.Id, $"Not found group with id='{req.Id}'");

        ThrowIfAnyErrors();

        await SendAsync(new EResponse(response!), cancellation: token);
    }

    public class EResponse
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }

        public EResponse(Group group)
        {
            Id = group.Id;
            Name = group.Name;
        }
    }
}