using Licenta.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Licenta.Infrastructure.EntityConfigurations;

public class PostConfiguration : IEntityTypeConfiguration<Post>
{
    public void Configure(EntityTypeBuilder<Post> builder)
    {
        // CONFIG
        builder
            .Property(post => post.Title)
            .HasMaxLength(100);

        builder
            .Property(post => post.Description)
            .HasMaxLength(5000);

        builder
            .HasMany(r => r.Images)
            .WithOne(r => r.Post)
            .HasForeignKey(r => r.PostId);

        builder.HasData
            (
                new Post
                {
                    Id = 1,
                    Title = "Post 1",
                    Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed ut quam imperdiet, ullamcorper ex non, efficitur nisi. Aliquam erat volutpat. Nullam et luctus dui, a porttitor lacus. Lorem ipsum dolor sit amet, consectetur adipiscing elit. Pellentesque et cursus erat. Nullam cursus consequat leo, a laoreet lectus convallis nec. Maecenas eget felis neque. Morbi lacinia neque id sapien dapibus, ac gravida neque pulvinar. Pellentesque rhoncus eu augue a pretium. ",
                    PartnerId = 1
                },
                new Post
                {
                    Id = 2,
                    Title = "Post 2",
                    Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed ut quam imperdiet, ullamcorper ex non, efficitur nisi. Aliquam erat volutpat. Nullam et luctus dui, a porttitor lacus. Lorem ipsum dolor sit amet, consectetur adipiscing elit. Pellentesque et cursus erat. Nullam cursus consequat leo, a laoreet lectus convallis nec. Maecenas eget felis neque. Morbi lacinia neque id sapien dapibus, ac gravida neque pulvinar. Pellentesque rhoncus eu augue a pretium.",
                    PartnerId = 1
                },
                new Post
                {
                    Id = 3,
                    Title = "Post 3",
                    Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed ut quam imperdiet, ullamcorper ex non, efficitur nisi. Aliquam erat volutpat. Nullam et luctus dui, a porttitor lacus. Lorem ipsum dolor sit amet, consectetur adipiscing elit. Pellentesque et cursus erat. Nullam cursus consequat leo, a laoreet lectus convallis nec. Maecenas eget felis neque. Morbi lacinia neque id sapien dapibus, ac gravida neque pulvinar. Pellentesque rhoncus eu augue a pretium.",
                    PartnerId = 3
                }
            );
    }
}
