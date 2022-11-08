using English.Class.Domain.Groups;

namespace English.Class.FastEndpoint.Endpoints.Groups;

public class GroupDetailGetEndpoint : Endpoint<GroupDetail>
{
    public override void Configure()
    {
        Get("group/{id}");
        AllowAnonymous();
    }

    public override async Task HandleAsync(GroupDetail req, CancellationToken token)
    {
        var id = Route<Guid>("id", isRequired: true);
        var rep = Resolve<IGroupRepository>();
        var group = await rep.GetDetailAsync(id, token);
        await SendAsync(group, cancellation: token);
    }
}