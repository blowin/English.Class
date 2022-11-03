using English.Class.Domain.Core;

namespace English.Class.Domain.Students;

public interface IStudentRepository : IRepository
{
    Task<Student> CreateAsync(CreateStudentRequest request, CancellationToken token);
    Task<Student?> DeleteAsync(Guid id, CancellationToken token);
}

public class CreateStudentRequest
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public Guid GroupId { get; set; }
}