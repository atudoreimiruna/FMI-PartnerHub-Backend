using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Licenta.Infrastructure.EntityConfigurations;

public class FileConfiguration : IEntityTypeConfiguration<Core.Entities.File>
{
    public void Configure(EntityTypeBuilder<Core.Entities.File> builder)
    {
        // CONFIG
        builder
            .HasOne(cs => cs.Event)
            .WithMany(cs => cs.Files)
            .HasForeignKey(cs => cs.EventId)
            .IsRequired(false)
            .OnDelete(DeleteBehavior.SetNull);

        builder
            .HasOne(cs => cs.Partner)
            .WithMany(cs => cs.Files)
            .HasForeignKey(cs => cs.PartnerId)
            .IsRequired(false)
            .OnDelete(DeleteBehavior.SetNull);

        builder
           .HasOne(cs => cs.Student)
           .WithMany(cs => cs.Files)
           .HasForeignKey(cs => cs.StudentId)
           .IsRequired(false)
           .OnDelete(DeleteBehavior.SetNull);
    }
}