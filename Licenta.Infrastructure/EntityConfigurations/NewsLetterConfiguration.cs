using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Licenta.Core.Entities;

namespace Licenta.Infrastructure.EntityConfigurations;

public class NewsLetterConfiguration : IEntityTypeConfiguration<Newsletter>
{
    public void Configure(EntityTypeBuilder<Newsletter> builder)
    {
        builder
            .Property(x => x.Email)
            .HasMaxLength(200);
    }
}
