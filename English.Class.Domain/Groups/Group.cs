using English.Class.Domain.Core;
using English.Class.Domain.Schedules;
using English.Class.Domain.Students;

namespace English.Class.Domain.Groups;

public class Group : Entity
{
    public string Name { get; set; } = string.Empty;
    public List<Student> Students { get; set; } = new();
    public List<Schedule> Schedules { get; set; } = new();
}