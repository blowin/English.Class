using English.Class.Domain.Core;
using English.Class.Domain.Groups;

namespace English.Class.Domain.Homeworks;

public class Homework : Entity
{
    public DateOnly HandingDate { get; set; }
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public Guid GroupId { get; set; }
    public Group? Group { get; set; }
}