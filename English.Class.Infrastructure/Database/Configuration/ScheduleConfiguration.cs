using English.Class.Domain.Schedules;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace English.Class.Infrastructure.Database.Configuration;

public class ScheduleConfiguration : ConfigurationBase<Schedule>
{
    protected override void ConfigureEntity(EntityTypeBuilder<Schedule> builder)
    {
        builder.Property(e => e.Time).IsRequired();
    }
}