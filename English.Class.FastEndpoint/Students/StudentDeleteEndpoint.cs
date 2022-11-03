using Microsoft.AspNetCore.Http;
using System.Net;
using English.Class.FastEndpoint.Extension;
using English.Class.Domain.Students;
using English.Class.FastEndpoint.Requests;

namespace English.Class.FastEndpoint.Students;

public class StudentDeleteEndpoint : Endpoint<RequestId, StudentDeleteEndpoint.EResponse>
{
    public override void Configure()
    {
        Delete("student");
        Description(b => b
            .Produces<Student>(HttpStatusCode.OK.AsInt())
            .ProducesProblem(HttpStatusCode.BadRequest.AsInt())
        );
        AllowAnonymous();
    }

    public override async Task HandleAsync(RequestId req, CancellationToken token)
    {
        var rep = Resolve<IStudentRepository>();
        var response = await rep.DeleteAsync(req.Id, token);
        if (response == null)
            AddError(e => e.Id, $"Not found student with id='{req.Id}'");

        ThrowIfAnyErrors();

        await SendAsync(new EResponse(response!), cancellation: token);
    }

    public class EResponse
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public EResponse(Student student)
        {
            Id = student.Id;
            FirstName = student.FirstName;
            LastName = student.LastName;
        }
    }
}