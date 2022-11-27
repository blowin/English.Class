using English.Class.Domain.Users;
using Microsoft.EntityFrameworkCore;

namespace English.Class.Infrastructure.Database.Users;

public class UserRepository : IUserRepository
{
    private readonly AppDbContext _db;

    public UserRepository(AppDbContext db)
    {
        _db = db;
    }

    public Task<bool> HasUserAsync(string login, CancellationToken token = default)
    {
        return _db.User.AnyAsync(e => e.Login == login, cancellationToken: token);
    }

    public async Task<User> CreateAsync(CreateUserRequest request, CancellationToken token = default)
    {
        var result = new User
        {
            Login = request.Login,
            Password = request.Password,
            Roles = request.Roles
        };
        _db.User.Add(result);
        await _db.SaveChangesAsync(token);
        return result;
    }
}