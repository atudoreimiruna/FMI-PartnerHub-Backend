using Licenta.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Licenta.Infrastructure.EntityConfigurations;

public class EventConfiguration : IEntityTypeConfiguration<Event>
{
    public void Configure(EntityTypeBuilder<Event> builder)
    {
        // CONFIG
        builder
            .Property(post => post.Title)
            .HasMaxLength(100);

        builder
           .Property(post => post.Type)
           .HasMaxLength(100);

        builder
           .Property(post => post.Location)
           .HasMaxLength(100);

        builder
            .Property(post => post.Description)
            .HasMaxLength(5000);

        builder
            .HasMany(r => r.Files)
            .WithOne(r => r.Event)
            .HasForeignKey(r => r.EventId);

        builder.HasData
        (
            new Event
            {
                Id = 1,
                Title = "Post 1",
                Type = "Conferinta",
                Location = "FMI",
                Date = new DateTime(2022, 04, 05),
                Time = "17:00",
                Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed ut quam imperdiet, ullamcorper ex non, efficitur nisi. Aliquam erat volutpat. Nullam et luctus dui, a porttitor lacus. Lorem ipsum dolor sit amet, consectetur adipiscing elit. Pellentesque et cursus erat. Nullam cursus consequat leo, a laoreet lectus convallis nec. Maecenas eget felis neque. Morbi lacinia neque id sapien dapibus, ac gravida neque pulvinar. Pellentesque rhoncus eu augue a pretium. ",
                PartnerId = 1
            },
            new Event
            {
                Id = 2,
                Title = "Post 2",
                Type = "Workshop",
                Location = "FMI",
                Date = new DateTime(2023, 08, 09),
                Time = "17:00",
                Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed ut quam imperdiet, ullamcorper ex non, efficitur nisi. Aliquam erat volutpat. Nullam et luctus dui, a porttitor lacus. Lorem ipsum dolor sit amet, consectetur adipiscing elit. Pellentesque et cursus erat. Nullam cursus consequat leo, a laoreet lectus convallis nec. Maecenas eget felis neque. Morbi lacinia neque id sapien dapibus, ac gravida neque pulvinar. Pellentesque rhoncus eu augue a pretium.",
                PartnerId = 1
            },
            new Event
            {
                Id = 3,
                Title = "Post 3",
                Type = "Workshop",
                Location = "FMI",
                Date = new DateTime(2023, 09, 09),
                Time = "17:00",
                Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed ut quam imperdiet, ullamcorper ex non, efficitur nisi. Aliquam erat volutpat. Nullam et luctus dui, a porttitor lacus. Lorem ipsum dolor sit amet, consectetur adipiscing elit. Pellentesque et cursus erat. Nullam cursus consequat leo, a laoreet lectus convallis nec. Maecenas eget felis neque. Morbi lacinia neque id sapien dapibus, ac gravida neque pulvinar. Pellentesque rhoncus eu augue a pretium.",
                PartnerId = 3
            }
        );
    }
}
