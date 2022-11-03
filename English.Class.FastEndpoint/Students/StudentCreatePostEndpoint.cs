using Microsoft.AspNetCore.Http;
using System.Net;
using English.Class.FastEndpoint.Extension;
using English.Class.Domain.Students;

namespace English.Class.FastEndpoint.Students;

public class StudentCreatePostEndpoint : Endpoint<CreateStudentRequest, Guid>
{
    public override void Configure()
    {
        Post("student");
        Description(b => b.Produces<Guid>(HttpStatusCode.Created.AsInt()));
        AllowAnonymous();
    }

    public override async Task HandleAsync(CreateStudentRequest req, CancellationToken token)
    {
        var rep = Resolve<IStudentRepository>();
        var response = await rep.CreateAsync(req, token);
        await SendAsync(response.Id, HttpStatusCode.Created.AsInt(), cancellation: token);
    }
}