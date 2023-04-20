using Licenta.Core.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Licenta.Core.Enums;

namespace Licenta.Infrastructure.EntityConfigurations;

public class JobConfiguration : IEntityTypeConfiguration<Job>
{
    public void Configure(EntityTypeBuilder<Job> builder)
    {
        builder
            .Property(x => x.Title)
            .HasMaxLength(100)
            .IsRequired();

        builder
            .Property(x => x.Description)
            .HasMaxLength(2000);

        builder
            .Property(x => x.Address)
            .HasMaxLength(100);

        builder
            .HasOne(cs => cs.Partner)
            .WithMany(cs => cs.Jobs)
            .HasForeignKey(cs => cs.PartnerId)
            .IsRequired(false)
            .OnDelete(DeleteBehavior.SetNull);

        builder.HasData
        (
            new Job
            {
                Id = 1,
                Title = "Job 1",
                MinSalary = 2000,
                MaxSalary = 2500,
                Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed ut quam imperdiet, ullamcorper ex non, efficitur nisi. Aliquam erat volutpat. Nullam et luctus dui, a porttitor lacus. Lorem ipsum dolor sit amet, consectetur adipiscing elit. Pellentesque et cursus erat. Nullam cursus consequat leo, a laoreet lectus convallis nec. Maecenas eget felis neque. Morbi lacinia neque id sapien dapibus, ac gravida neque pulvinar. Pellentesque rhoncus eu augue a pretium. ",
                Address = "Bucharest",
                Experience = JobExperienceEnum.Entry,
                Activated = true,
                PartnerId = 1
            },
            new Job
            {
                Id = 2,
                Title = "Job 2",
                MinSalary = 5000,
                MaxSalary = 5500,
                Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed ut quam imperdiet, ullamcorper ex non, efficitur nisi. Aliquam erat volutpat. Nullam et luctus dui, a porttitor lacus. Lorem ipsum dolor sit amet, consectetur adipiscing elit. Pellentesque et cursus erat. Nullam cursus consequat leo, a laoreet lectus convallis nec. Maecenas eget felis neque. Morbi lacinia neque id sapien dapibus, ac gravida neque pulvinar. Pellentesque rhoncus eu augue a pretium. ",
                Address = "Bucharest",
                Experience = JobExperienceEnum.Senior,
                Activated = true,
                PartnerId = 2
            }
        );
    }
}