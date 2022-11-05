using English.Class.Domain.Schedules;
using Microsoft.EntityFrameworkCore;

namespace English.Class.Infrastructure.Database.Schedules;

public class ScheduleRepository : IScheduleRepository
{
    private AppDbContext _db;

    public ScheduleRepository(AppDbContext db)
    {
        _db = db;
    }

    public async Task<Schedule> CreateAsync(CreateScheduleRequest request, CancellationToken token)
    {
        var res = new Schedule { Time = request.Time, GroupId = request.GroupId };
        _db.Schedule.Add(res);
        await _db.SaveChangesAsync(token);
        return res;
    }

    public async Task<Schedule?> DeleteAsync(Guid id, CancellationToken token)
    {
        var removeItem = await _db.Schedule.AsTracking().FirstOrDefaultAsync(e => e.Id == id, cancellationToken: token);
        if (removeItem == null)
            return null;

        _db.Schedule.Remove(removeItem);
        await _db.SaveChangesAsync(token);
        return removeItem;
    }
}