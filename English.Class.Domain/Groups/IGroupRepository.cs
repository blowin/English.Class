using English.Class.Domain.Core;
using X.PagedList;

namespace English.Class.Domain.Groups;

public interface IGroupRepository : IRepository
{
    Task<IPagedList<GroupName>> GetGroupNamePageAsync(int page, int pageSize, CancellationToken token);
    Task<Group> CreateAsync(string name, CancellationToken token);
    Task<Group?> DeleteAsync(Guid id, CancellationToken token);
    Task<Group?> RenameAsync(Guid id, string newName, CancellationToken token);
}

public class GroupName
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
}