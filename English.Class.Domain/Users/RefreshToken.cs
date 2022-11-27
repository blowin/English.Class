using English.Class.Domain.Core;

namespace English.Class.Domain.Users;

public class RefreshToken : Entity
{
    public Guid UserId { get; set; }
    public Guid Token { get; set; }
    public User? User { get; set; }
}