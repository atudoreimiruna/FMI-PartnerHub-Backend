using Licenta.Core.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Licenta.Infrastructure.EntityConfigurations;

public class ImageConfiguration : IEntityTypeConfiguration<Image>
{
    public void Configure(EntityTypeBuilder<Image> builder)
    {
        // CONFIG

        builder
            .HasOne(cs => cs.Post)
            .WithMany(cs => cs.Images)
            .HasForeignKey(cs => cs.PostId)
            .IsRequired(false)
            .OnDelete(DeleteBehavior.SetNull);
    }
}