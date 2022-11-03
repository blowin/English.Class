using English.Class.Domain.Core;
using English.Class.Domain.Groups;

namespace English.Class.Domain.Schedules;

public class Schedule : Entity
{
    public DateTime Time { get; set; }
    public Guid GroupId { get; set; }
    public Group? Group { get; set; }
}
