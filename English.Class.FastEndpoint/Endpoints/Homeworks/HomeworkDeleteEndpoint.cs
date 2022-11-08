using System.Net;
using English.Class.Domain.Homeworks;
using English.Class.Domain.Students;
using English.Class.FastEndpoint.Extension;
using English.Class.FastEndpoint.Requests;
using Microsoft.AspNetCore.Http;

namespace English.Class.FastEndpoint.Endpoints.Homeworks;

public class HomeworkDeleteEndpoint : Endpoint<RequestId, HomeworkDeleteEndpoint.EResponse>
{
    public override void Configure()
    {
        Delete("homework");
        Description(b => b
            .Produces<Student>(HttpStatusCode.OK.AsInt())
            .ProducesProblem(HttpStatusCode.BadRequest.AsInt())
        );
        AllowAnonymous();
    }

    public override async Task HandleAsync(RequestId req, CancellationToken token)
    {
        var rep = Resolve<IHomeworkRepository>();
        var response = await rep.DeleteAsync(req.Id, token);
        if (response == null)
            AddError(e => e.Id, $"Not found homework with id='{req.Id}'");

        ThrowIfAnyErrors();

        await SendAsync(new EResponse(response!), cancellation: token);
    }

    public class EResponse
    {
        public Guid Id { get; set; }
        public DateOnly HandingDate { get; set; }
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }

        public EResponse(Homework homework)
        {
            Id = homework.Id;
            HandingDate = homework.HandingDate;
            Title = homework.Title;
            Description = homework.Description;
        }
    }
}