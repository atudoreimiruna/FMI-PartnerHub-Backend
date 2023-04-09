using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Licenta.Infrastructure.EntityConfigurations;

public class FileConfiguration : IEntityTypeConfiguration<Core.Entities.File>
{
    public void Configure(EntityTypeBuilder<Core.Entities.File> builder)
    {
        // CONFIG
        builder
            .HasOne(cs => cs.Post)
            .WithMany(cs => cs.Files)
            .HasForeignKey(cs => cs.PostId)
            .IsRequired(false)
            .OnDelete(DeleteBehavior.SetNull);
    }
}