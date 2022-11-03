using English.Class.Domain.Students;
using Microsoft.EntityFrameworkCore;

namespace English.Class.Infrastructure.Database.Students;

public class StudentRepository : IStudentRepository
{
    private readonly AppDbContext _db;

    public StudentRepository(AppDbContext db)
    {
        _db = db;
    }

    public async Task<Student> CreateAsync(CreateStudentRequest request, CancellationToken token)
    {
        var student = new Student
        {
            FirstName = request.FirstName,
            LastName = request.LastName,
            GroupId = request.GroupId
        };
        _db.Student.Add(student);
        await _db.SaveChangesAsync(token);
        return student;
    }

    public async Task<Student?> DeleteAsync(Guid id, CancellationToken token)
    {
        var student = await _db.Student.AsTracking().FirstOrDefaultAsync(e => e.Id == id, token);
        if (student == null)
            return null;

        _db.Student.Remove(student);
        await _db.SaveChangesAsync(token);
        return student;
    }
}