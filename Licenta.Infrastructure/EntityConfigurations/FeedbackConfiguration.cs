using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Licenta.Core.Entities;

namespace Licenta.Infrastructure.EntityConfigurations;

public class FeedbackConfiguration : IEntityTypeConfiguration<Feedback>
{
    public void Configure(EntityTypeBuilder<Feedback> builder)
    {
        builder
            .Property(x => x.Name)
            .HasMaxLength(100);

        builder
            .Property(x => x.Message)
            .HasMaxLength(3000);
    }
}
