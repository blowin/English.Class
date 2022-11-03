using English.Class.Domain.Homeworks;
using English.Class.Domain.Students;
using Microsoft.EntityFrameworkCore;

namespace English.Class.Infrastructure.Database.Homeworks;

public class HomeworkRepository : IHomeworkRepository
{
    private readonly AppDbContext _db;

    public HomeworkRepository(AppDbContext db)
    {
        _db = db;
    }

    public async Task<Homework> CreateAsync(CreateHomeworkRequest request, CancellationToken token)
    {
        var homework = new Homework
        {
            GroupId = request.GroupId,
            Title = request.Title,
            Description = request.Description,
            HandingDate = request.HandingDate
        };
        _db.Homework.Add(homework);
        await _db.SaveChangesAsync(token);
        return homework;
    }

    public async Task<Homework?> DeleteAsync(Guid id, CancellationToken token)
    {
        var homework = await _db.Homework.AsTracking().FirstOrDefaultAsync(e => e.Id == id, token);
        if (homework == null)
            return null;

        _db.Homework.Remove(homework);
        await _db.SaveChangesAsync(token);
        return homework;
    }
}