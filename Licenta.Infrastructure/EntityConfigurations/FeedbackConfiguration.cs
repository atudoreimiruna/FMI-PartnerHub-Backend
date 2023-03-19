using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Licenta.Core.Entities;

namespace Licenta.Infrastructure.EntityConfigurations;

public class FeedbackConfiguration : IEntityTypeConfiguration<Feedback>
{
    public void Configure(EntityTypeBuilder<Feedback> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Name).HasColumnType("nvarchar(100)").HasMaxLength(100);
        builder.Property(x => x.Message).HasColumnType("nvarchar(3000)").HasMaxLength(3000);
    }
}
