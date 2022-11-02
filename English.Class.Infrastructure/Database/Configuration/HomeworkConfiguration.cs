using English.Class.Domain.Homeworks;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace English.Class.Infrastructure.Database.Configuration;

public class HomeworkConfiguration : ConfigurationBase<Homework>
{
    protected override void ConfigureEntity(EntityTypeBuilder<Homework> builder)
    {
        builder.Property(e => e.Title).HasMaxLength(128).IsRequired();
        builder.Property(e => e.Description).HasMaxLength(512);
        builder.Property(e => e.HandingDate).IsRequired();
        builder.HasOne(e => e.Group)
            .WithOne()
            .HasForeignKey<Homework>(e => e.GroupId);
    }
}
