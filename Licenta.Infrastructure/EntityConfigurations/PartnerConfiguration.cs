using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Licenta.Core.Entities;

namespace Licenta.Infrastructure.EntityConfigurations;

public class PartnerConfiguration : IEntityTypeConfiguration<Partner>
{
    public void Configure(EntityTypeBuilder<Partner> builder)
    {
        builder
            .Property(x => x.Name)
            .HasMaxLength(50)
            .IsRequired();

        builder
            .Property(x => x.MainDescription)
            .HasMaxLength(1000);

        builder
            .Property(x => x.Description)
            .HasMaxLength(3000);

        builder
            .Property(x => x.Address)
            .HasMaxLength(100);

        builder
            .Property(x => x.MainImageUrl)
            .HasMaxLength(300);

        builder
           .Property(x => x.LogoImageUrl)
           .HasMaxLength(300);

        builder
            .Property(x => x.Phone)
            .HasMaxLength(100);

        builder
           .Property(x => x.Email)
           .HasMaxLength(100);

        builder
            .HasMany(c => c.Posts)
            .WithOne(n => n.Partner)
            .HasForeignKey(n => n.PartnerId)
            .OnDelete(DeleteBehavior.Cascade);

        builder
            .HasMany(c => c.Jobs)
            .WithOne(n => n.Partner)
            .HasForeignKey(n => n.PartnerId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasData
        (
            new Partner
            {
                Id = 1,
                Name = "Partner 1",
                Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed ut quam imperdiet, ullamcorper ex non, efficitur nisi. Aliquam erat volutpat. Nullam et luctus dui, a porttitor lacus. Lorem ipsum dolor sit amet, consectetur adipiscing elit. Pellentesque et cursus erat. Nullam cursus consequat leo, a laoreet lectus convallis nec. Maecenas eget felis neque. Morbi lacinia neque id sapien dapibus, ac gravida neque pulvinar. Pellentesque rhoncus eu augue a pretium. ",
                MainDescription = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed ut quam imperdiet, ullamcorper ex non, efficitur nisi.",
                Address = "Bucharest",
                Phone = "Phone: 0886565767",
                Email = "Email: partner1@gmail.com"
            },
            new Partner
            {
                Id = 2,
                Name = "Partner 2",
                MainDescription = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed ut quam imperdiet, ullamcorper ex non, efficitur nisi.",
                Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed ut quam imperdiet, ullamcorper ex non, efficitur nisi. Aliquam erat volutpat. Nullam et luctus dui, a porttitor lacus. Lorem ipsum dolor sit amet, consectetur adipiscing elit. Pellentesque et cursus erat. Nullam cursus consequat leo, a laoreet lectus convallis nec. Maecenas eget felis neque. Morbi lacinia neque id sapien dapibus, ac gravida neque pulvinar. Pellentesque rhoncus eu augue a pretium.",
                Address = "Bucharest",
                Email = "Email: office@partner.com",
                Phone = "Phone: 0886435767"
            },
            new Partner
            {
                Id = 3,
                Name = "Partner 3",
                MainDescription = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed ut quam imperdiet, ullamcorper ex non, efficitur nisi.",
                Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed ut quam imperdiet, ullamcorper ex non, efficitur nisi. Aliquam erat volutpat. Nullam et luctus dui, a porttitor lacus. Lorem ipsum dolor sit amet, consectetur adipiscing elit. Pellentesque et cursus erat. Nullam cursus consequat leo, a laoreet lectus convallis nec. Maecenas eget felis neque. Morbi lacinia neque id sapien dapibus, ac gravida neque pulvinar. Pellentesque rhoncus eu augue a pretium.",
                Address = "Bucharest",
                Phone = "Phone: 0775345243",
                Email = "Email: partner3@gmail.com"
            }
        );
    }
}
