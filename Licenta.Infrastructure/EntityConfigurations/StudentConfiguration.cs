﻿using Licenta.Core.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Licenta.Infrastructure.EntityConfigurations;

public class StudentConfiguration : IEntityTypeConfiguration<Student>
{
    public void Configure(EntityTypeBuilder<Student> builder)
    {
        // CONFIG
        builder
            .HasIndex(student => new { student.Name, student.Email })
            .IsUnique();

        builder
            .Property(post => post.Name)
            .HasMaxLength(50);

        builder
           .Property(post => post.PersonalEmail)
           .HasMaxLength(50);

        builder
           .Property(post => post.Email)
           .HasMaxLength(50);

        builder
           .Property(post => post.Phone)
           .HasMaxLength(15);

        builder
           .Property(post => post.Skill)
           .HasMaxLength(1000);

        builder
            .Property(post => post.Description)
            .HasMaxLength(5000);

        builder
            .HasMany(r => r.Files)
            .WithOne(r => r.Student)
            .HasForeignKey(r => r.StudentId);

        builder
            .HasMany(c => c.StudentJobs)
            .WithOne(cs => cs.Student)
            .HasForeignKey(cs => cs.StudentId)
            .IsRequired(false)
            .OnDelete(DeleteBehavior.Cascade);

        builder
            .HasMany(c => c.StudentPartners)
            .WithOne(cs => cs.Student)
            .HasForeignKey(cs => cs.StudentId)
            .IsRequired(false)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
