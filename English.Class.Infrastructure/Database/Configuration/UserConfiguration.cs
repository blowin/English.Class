using System.Text.Json;
using English.Class.Domain.Users;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace English.Class.Infrastructure.Database.Configuration;

public class UserConfiguration : ConfigurationBase<User>
{
    protected override void ConfigureEntity(EntityTypeBuilder<User> builder)
    {
        builder.Property(e => e.Login).IsRequired().HasMaxLength(128);
        builder.Property(e => e.Password).IsRequired().HasMaxLength(128);
        builder.Property(e => e.Roles)
            .HasConversion(set => set == null ? null : JsonSerializer.Serialize(set, JsonSerializerOptions.Default),
                json => string.IsNullOrEmpty(json) ? null : JsonSerializer.Deserialize<HashSet<Role>?>(json, JsonSerializerOptions.Default));
    }
}