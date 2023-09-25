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
            .Property(x => x.Criteria)
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

        builder
            .HasMany(c => c.StudentJobs)
            .WithOne(cs => cs.Job)
            .HasForeignKey(cs => cs.JobId)
            .IsRequired(false)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasData
        (
            new Job
            {
                Id = 1,
                Title = "Job 1",
                Salary = "Platit: 2000",
                Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed ut quam imperdiet, ullamcorper ex non, efficitur nisi. Aliquam erat volutpat. Nullam et luctus dui, a porttitor lacus. Lorem ipsum dolor sit amet, consectetur adipiscing elit. Pellentesque et cursus erat. Nullam cursus consequat leo, a laoreet lectus convallis nec. Maecenas eget felis neque. Morbi lacinia neque id sapien dapibus, ac gravida neque pulvinar. Pellentesque rhoncus eu augue a pretium. ",
                Criteria = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed ut quam imperdiet, ullamcorper ex non, efficitur nisi. Aliquam erat volutpat. Nullam et luctus dui, a porttitor lacus. Lorem ipsum dolor sit amet, consectetur adipiscing elit. Pellentesque et cursus erat. Nullam cursus consequat leo, a laoreet lectus convallis nec. Maecenas eget felis neque. Morbi lacinia neque id sapien dapibus, ac gravida neque pulvinar. Pellentesque rhoncus eu augue a pretium. ",
                Address = "Bucharest",
                Skills = "Problem Solving, Teamwork",
                Experience = JobExperienceEnum.Entry,
                MinExperience = 0,
                MaxExperience = 1,
                Activated = true,
                PartnerId = 1,
                Type = TypeJobEnum.Internship
            },
            new Job
            {
                Id = 2,
                Title = "Job 2",
                Salary = "Neplatit",
                Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed ut quam imperdiet, ullamcorper ex non, efficitur nisi. Aliquam erat volutpat. Nullam et luctus dui, a porttitor lacus. Lorem ipsum dolor sit amet, consectetur adipiscing elit. Pellentesque et cursus erat. Nullam cursus consequat leo, a laoreet lectus convallis nec. Maecenas eget felis neque. Morbi lacinia neque id sapien dapibus, ac gravida neque pulvinar. Pellentesque rhoncus eu augue a pretium. ",
                Criteria = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed ut quam imperdiet, ullamcorper ex non, efficitur nisi. Aliquam erat volutpat. Nullam et luctus dui, a porttitor lacus. Lorem ipsum dolor sit amet, consectetur adipiscing elit. Pellentesque et cursus erat. Nullam cursus consequat leo, a laoreet lectus convallis nec. Maecenas eget felis neque. Morbi lacinia neque id sapien dapibus, ac gravida neque pulvinar. Pellentesque rhoncus eu augue a pretium. ",
                Address = "Bucharest",
                Skills = "Problem Solving, Teamwork",
                Experience = JobExperienceEnum.Senior,
                MinExperience = 2,
                MaxExperience = 4,
                Activated = true,
                PartnerId = 2,
                Type = TypeJobEnum.FullTime
            }
        );
    }
}