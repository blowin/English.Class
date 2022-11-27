using System.Net;
using CSharpFunctionalExtensions;
using English.Class.Domain.Students;
using English.Class.Domain.Users;
using English.Class.FastEndpoint.Extension;
using English.Class.FastEndpoint.Requests;
using Microsoft.AspNetCore.Http;

namespace English.Class.FastEndpoint.Endpoints.Users;

public class UserCreatePostEndpoint : Endpoint<CreateUserRequest, RequestId>
{
    public override void Configure()
    {
        Post("user");
        Description(b => b
            .Produces<Guid>(HttpStatusCode.Created.AsInt())
            .Produces<string>(HttpStatusCode.BadRequest.AsInt())
        );
        AllowAnonymous();
    }

    public override async Task HandleAsync(CreateUserRequest req, CancellationToken token)
    {
        var service = Resolve<UserService>();
        var (_, isFailure, user, fail) = await service.CreateAsync(req, token);
        if (isFailure)
            ThrowError(fail);

        await SendAsync(RequestId.From(user.Id), HttpStatusCode.Created.AsInt(), cancellation: token);
    }
}
