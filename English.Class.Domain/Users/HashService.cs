using English.Class.Domain.Core;

namespace English.Class.Domain.Users;

public class HashService : ISingletonService
{
    public string Hash(string value) => BCrypt.Net.BCrypt.HashPassword(value, 15);
}