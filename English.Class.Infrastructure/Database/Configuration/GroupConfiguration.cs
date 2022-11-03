using English.Class.Domain.Groups;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace English.Class.Infrastructure.Database.Configuration;

public class GroupConfiguration : ConfigurationBase<Group>
{
    protected override void ConfigureEntity(EntityTypeBuilder<Group> builder)
    {
        builder.Property(e => e.Name).IsRequired().HasMaxLength(256);

        builder.HasMany(e => e.Schedules)
            .WithOne(e => e.Group)
            .HasForeignKey(e => e.GroupId);

        builder.HasMany(e => e.Students)
            .WithOne(e => e.Group)
            .HasForeignKey(e => e.GroupId);

        builder.HasMany(e => e.Homeworks)
            .WithOne(e => e.Group)
            .HasForeignKey(e => e.GroupId);
    }
}