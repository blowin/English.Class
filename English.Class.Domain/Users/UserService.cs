using CSharpFunctionalExtensions;
using English.Class.Domain.Core;

namespace English.Class.Domain.Users;

public sealed class UserService : IScopedService
{
    private readonly IUserRepository _userRepository;
    private readonly HashService _hashService;

    public UserService(IUserRepository userRepository, HashService hashService)
    {
        _userRepository = userRepository;
        _hashService = hashService;
    }

    public async Task<Result<User>> CreateAsync(CreateUserRequest request, CancellationToken cancellationToken)
    {
        if (request.Roles == null || request.Roles.Count == 0)
            return Result.Failure<User>("You need to specify the user roles");
        
        var hasUser = await _userRepository.HasUserAsync(request.Login, cancellationToken);
        if (hasUser)
            return Result.Failure<User>("There is already such a user");

        var updateRequest = request with { Password = _hashService.Hash(request.Password) };
        var user = await _userRepository.CreateAsync(updateRequest, cancellationToken);
        return user;
    }
}
