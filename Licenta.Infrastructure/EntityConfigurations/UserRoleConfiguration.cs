using Licenta.Core.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Licenta.Infrastructure.EntityConfigurations;

public class UserRoleConfiguration : IEntityTypeConfiguration<UserRole>
{
    public void Configure(EntityTypeBuilder<UserRole> builder)
    {
        // CONFIG
        builder.HasKey(prop => new { prop.UserId, prop.RoleId });
    }
}
