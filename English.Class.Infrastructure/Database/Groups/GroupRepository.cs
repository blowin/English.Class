using English.Class.Domain.Groups;
using Microsoft.EntityFrameworkCore;
using X.PagedList;

namespace English.Class.Infrastructure.Database.Groups;

public sealed class GroupRepository : IGroupRepository
{
    private readonly AppDbContext _db;

    public GroupRepository(AppDbContext db)
    {
        _db = db;
    }

    public Task<IPagedList<GroupName>> GetGroupNamePageAsync(int page, int pageSize, CancellationToken token)
    {
        return _db.Group.Select(e => new GroupName
        {
            Id = e.Id,
            Name = e.Name
        })
        .OrderBy(e => e.Id)
        .ToPagedListAsync(page, pageSize, token);
    }

    public async Task<Group> CreateAsync(string name, CancellationToken token)
    {
        var res = new Group { Name = name };
        _db.Group.Add(res);
        await _db.SaveChangesAsync(token);
        return res;
    }

    public async Task<Group?> DeleteAsync(Guid id, CancellationToken token)
    {
        var removeItem = await _db.Group.AsTracking().FirstOrDefaultAsync(e => e.Id == id, cancellationToken: token);
        if (removeItem == null)
            return null;

        _db.Group.Remove(removeItem);
        await _db.SaveChangesAsync(token);
        return removeItem;
    }

    public async Task<Group?> RenameAsync(Guid id, string newName, CancellationToken token)
    {
        var updateItem = await _db.Group.AsTracking().FirstOrDefaultAsync(e => e.Id == id, cancellationToken: token);
        if (updateItem == null)
            return null;

        updateItem.Name = newName;
        _db.Group.Update(updateItem);
        await _db.SaveChangesAsync(token);
        return updateItem;
    }

    public Task<GroupDetail> GetDetailAsync(Guid id, CancellationToken token)
    {
        return _db.Group
            .Include(e => e.Students)
            .Include(e => e.Schedules)
            .Include(e => e.Homeworks)
            .AsSplitQuery()
            .Select(e => new GroupDetail
            {
                Id = e.Id,
                Name = e.Name,
                Homeworks = e.Homeworks == null ? null : e.Homeworks.Select(h => new GroupDetail.Homework
                {
                    Id = h.Id,
                    Title = h.Title,
                    Description = h.Description,
                    HandingDate = h.HandingDate
                }).ToList(),
                Schedules = e.Schedules == null ? null : e.Schedules.Select(h => new GroupDetail.Schedule
                {
                    Id = h.Id,
                    Time = h.Time
                }).ToList(),
                Students = e.Students == null ? null : e.Students.Select(h => new GroupDetail.Student
                {
                    Id = h.Id,
                    FirstName = h.FirstName,
                    LastName = h.LastName
                }).ToList()
            })
            .FirstAsync(e => e.Id == id);
    }
}