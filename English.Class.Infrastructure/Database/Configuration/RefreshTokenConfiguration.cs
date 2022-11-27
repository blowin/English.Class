using English.Class.Domain.Users;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace English.Class.Infrastructure.Database.Configuration;

public class RefreshTokenConfiguration : ConfigurationBase<RefreshToken>
{
    protected override void ConfigureEntity(EntityTypeBuilder<RefreshToken> builder)
    {
        builder.Property(e => e.Token).IsRequired();
        builder.HasOne(e => e.User)
            .WithOne()
            .HasForeignKey<RefreshToken>(e => e.UserId)
            .IsRequired();
    }
}