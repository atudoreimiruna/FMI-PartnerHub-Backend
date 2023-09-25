using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Licenta.Core.Entities;

namespace Licenta.Infrastructure.EntityConfigurations;

public class PracticeConfiguration : IEntityTypeConfiguration<Practice>
{
    public void Configure(EntityTypeBuilder<Practice> builder)
    {
        builder.HasData
        (
            new Practice
            {
                Id = 1,
                Description = "Despre Practica"
            }
         );
    }
}