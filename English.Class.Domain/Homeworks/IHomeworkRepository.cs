using English.Class.Domain.Core;

namespace English.Class.Domain.Homeworks;

public interface IHomeworkRepository : IRepository
{
    Task<Homework> CreateAsync(CreateHomeworkRequest request, CancellationToken token);
    Task<Homework?> DeleteAsync(Guid id, CancellationToken token);
}

public class CreateHomeworkRequest
{
    public DateOnly HandingDate { get; set; }
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public Guid GroupId { get; set; }
}