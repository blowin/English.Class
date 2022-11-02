using English.Class.Domain.Students;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace English.Class.Infrastructure.Database.Configuration;

public class StudentConfiguration : ConfigurationBase<Student>
{
    protected override void ConfigureEntity(EntityTypeBuilder<Student> builder)
    {
        builder.Property(e => e.FirstName).HasMaxLength(48).IsRequired();
        builder.Property(e => e.LastName).HasMaxLength(48).IsRequired();
    }
}