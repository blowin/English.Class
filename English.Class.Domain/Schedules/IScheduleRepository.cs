using English.Class.Domain.Core;

namespace English.Class.Domain.Schedules;

public interface IScheduleRepository : IRepository
{
    Task<Schedule> CreateAsync(CreateScheduleRequest request, CancellationToken token);
    Task<Schedule?> DeleteAsync(Guid id, CancellationToken token);
}

public class CreateScheduleRequest
{
    public DateTime Time { get; set; }
    public Guid GroupId { get; set; }
}