using System.Net;
using English.Class.Domain.Groups;
using English.Class.FastEndpoint.Extension;
using English.Class.FastEndpoint.Requests;
using Microsoft.AspNetCore.Http;
using Group = English.Class.Domain.Groups.Group;

namespace English.Class.FastEndpoint.Endpoints.Groups;

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