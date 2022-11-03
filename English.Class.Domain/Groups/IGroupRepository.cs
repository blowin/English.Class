using English.Class.Domain.Core;
using X.PagedList;

namespace English.Class.Domain.Groups;

public interface IGroupRepository : IRepository
{
    Task<IPagedList<GroupName>> GetGroupNamePageAsync(int page, int pageSize, CancellationToken token);
    Task<Group> CreateAsync(string name, CancellationToken token);
    Task<Group?> DeleteAsync(Guid id, CancellationToken token);
    Task<Group?> RenameAsync(Guid id, string newName, CancellationToken token);
    Task<GroupDetail> GetDetailAsync(Guid id, CancellationToken token);
}

public class GroupName
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
}

public class GroupDetail
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public List<Student>? Students { get; set; }
    public List<Schedule>? Schedules { get; set; }
    public List<Homework>? Homeworks { get; set; }

    public class Student
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
    }

    public class Homework
    {
        public Guid Id { get; set; }
        public DateOnly HandingDate { get; set; }
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
    }

    public class Schedule
    {
        public Guid Id { get; set; }
        public DateTime Time { get; set; }
    }
}