using English.Class.Domain.Core;

namespace English.Class.Domain.Users;

public interface IUserRepository : IRepository
{
    Task<bool> HasUserAsync(string login, CancellationToken token = default);
    Task<User> CreateAsync(CreateUserRequest request, CancellationToken token = default);
}

public record CreateUserRequest
{
    public string Login { get; init; } = string.Empty;
    public string Password { get; init; } = string.Empty;
    public HashSet<Role>? Roles { get; init; }
}
