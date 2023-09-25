using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Licenta.Core.Entities;

namespace Licenta.Infrastructure.EntityConfigurations;

public class StudentJobConfiguration : IEntityTypeConfiguration<StudentJob>
{
    public void Configure(EntityTypeBuilder<StudentJob> builder)
    {
        // CONFIG
        builder.HasKey(prop => new { prop.StudentId, prop.JobId });
    }
}
