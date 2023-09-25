using Licenta.Core.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Licenta.Infrastructure.EntityConfigurations;

public class StudentPartnerConfiguration : IEntityTypeConfiguration<StudentPartner>
{
    public void Configure(EntityTypeBuilder<StudentPartner> builder)
    {
        // CONFIG
        builder.HasKey(prop => new { prop.StudentId, prop.PartnerId });
    }
}