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
            .Property(x => x.Description)
            .HasMaxLength(500);

        builder
            .Property(x => x.Address)
            .HasMaxLength(100);

        builder
            .Property(x => x.Contact)
            .HasMaxLength(100);

        builder.HasData
        (
            new Partner
            {
                Id = 1,
                Name = "Partner 1",
                Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed ut quam imperdiet, ullamcorper ex non, efficitur nisi. Aliquam erat volutpat. Nullam et luctus dui, a porttitor lacus. Lorem ipsum dolor sit amet, consectetur adipiscing elit. Pellentesque et cursus erat. Nullam cursus consequat leo, a laoreet lectus convallis nec. Maecenas eget felis neque. Morbi lacinia neque id sapien dapibus, ac gravida neque pulvinar. Pellentesque rhoncus eu augue a pretium. ",
                Address = "Bucharest",
                Contact = "Phone: 0886565767"
            },
            new Partner
            {
                Id = 2,
                Name = "Partner 2",
                Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed ut quam imperdiet, ullamcorper ex non, efficitur nisi. Aliquam erat volutpat. Nullam et luctus dui, a porttitor lacus. Lorem ipsum dolor sit amet, consectetur adipiscing elit. Pellentesque et cursus erat. Nullam cursus consequat leo, a laoreet lectus convallis nec. Maecenas eget felis neque. Morbi lacinia neque id sapien dapibus, ac gravida neque pulvinar. Pellentesque rhoncus eu augue a pretium.",
                Address = "Bucharest",
                Contact = "Email: office@partner.com"
            },
            new Partner
            {
                Id = 3,
                Name = "Partner 3",
                Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed ut quam imperdiet, ullamcorper ex non, efficitur nisi. Aliquam erat volutpat. Nullam et luctus dui, a porttitor lacus. Lorem ipsum dolor sit amet, consectetur adipiscing elit. Pellentesque et cursus erat. Nullam cursus consequat leo, a laoreet lectus convallis nec. Maecenas eget felis neque. Morbi lacinia neque id sapien dapibus, ac gravida neque pulvinar. Pellentesque rhoncus eu augue a pretium.",
                Address = "Bucharest",
                Contact = "Phone: 0775345243"
            }
        );
    }
}
