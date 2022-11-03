using English.Class.Domain.Groups;
using English.Class.FastEndpoint.Requests;
using X.PagedList;

namespace English.Class.FastEndpoint.Groups;

public class GroupNameGetEndpoint : Endpoint<PageRequest, IPagedList<GroupName>>
{
    public override void Configure()
    {
        Get("group");
        AllowAnonymous();
    }

    public override async Task HandleAsync(PageRequest req, CancellationToken token)
    {
        var rep = Resolve<IGroupRepository>();
        var groupNames = await rep.GetGroupNamePageAsync(req.PageNumber, req.PageSize, token);
        await SendAsync(groupNames, cancellation: token);
    }
}