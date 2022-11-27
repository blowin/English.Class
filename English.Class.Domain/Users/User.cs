using English.Class.Domain.Core;

namespace English.Class.Domain.Users;

public class User : Entity
{
    public string Login { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public HashSet<Role>? Roles { get; set; }
}