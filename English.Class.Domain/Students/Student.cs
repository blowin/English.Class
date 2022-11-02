using English.Class.Domain.Core;
using English.Class.Domain.Groups;

namespace English.Class.Domain.Students
{
    public class Student : Entity
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public Guid GroupId { get; set; }
        public Group? Group { get; set; }
    }
}