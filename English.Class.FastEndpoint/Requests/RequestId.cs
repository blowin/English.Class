namespace English.Class.FastEndpoint.Requests;

public class RequestId
{
    public Guid Id { get; set; }

    public static RequestId From(Guid id) => new() { Id = id };
}