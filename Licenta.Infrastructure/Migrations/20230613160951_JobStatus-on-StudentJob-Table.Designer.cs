﻿// <auto-generated />
using System;
using Licenta.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Licenta.Infrastructure.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20230613160951_JobStatus-on-StudentJob-Table")]
    partial class JobStatusonStudentJobTable
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("Licenta.Core.Entities.Event", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Description")
                        .HasMaxLength(5000)
                        .HasColumnType("varchar(5000)");

                    b.Property<DateTime>("LastUpdated")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Location")
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<long?>("PartnerId")
                        .HasColumnType("bigint");

                    b.Property<string>("Time")
                        .HasColumnType("longtext");

                    b.Property<string>("Title")
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<string>("Type")
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.HasKey("Id");

                    b.HasIndex("PartnerId");

                    b.ToTable("Events");

                    b.HasData(
                        new
                        {
                            Id = 1L,
                            CreatedAt = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Date = new DateTime(2022, 4, 5, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed ut quam imperdiet, ullamcorper ex non, efficitur nisi. Aliquam erat volutpat. Nullam et luctus dui, a porttitor lacus. Lorem ipsum dolor sit amet, consectetur adipiscing elit. Pellentesque et cursus erat. Nullam cursus consequat leo, a laoreet lectus convallis nec. Maecenas eget felis neque. Morbi lacinia neque id sapien dapibus, ac gravida neque pulvinar. Pellentesque rhoncus eu augue a pretium. ",
                            LastUpdated = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Location = "FMI",
                            PartnerId = 1L,
                            Time = "17:00",
                            Title = "Post 1",
                            Type = "Conferinta"
                        },
                        new
                        {
                            Id = 2L,
                            CreatedAt = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Date = new DateTime(2023, 8, 9, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed ut quam imperdiet, ullamcorper ex non, efficitur nisi. Aliquam erat volutpat. Nullam et luctus dui, a porttitor lacus. Lorem ipsum dolor sit amet, consectetur adipiscing elit. Pellentesque et cursus erat. Nullam cursus consequat leo, a laoreet lectus convallis nec. Maecenas eget felis neque. Morbi lacinia neque id sapien dapibus, ac gravida neque pulvinar. Pellentesque rhoncus eu augue a pretium.",
                            LastUpdated = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Location = "FMI",
                            PartnerId = 1L,
                            Time = "17:00",
                            Title = "Post 2",
                            Type = "Workshop"
                        },
                        new
                        {
                            Id = 3L,
                            CreatedAt = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Date = new DateTime(2023, 9, 9, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed ut quam imperdiet, ullamcorper ex non, efficitur nisi. Aliquam erat volutpat. Nullam et luctus dui, a porttitor lacus. Lorem ipsum dolor sit amet, consectetur adipiscing elit. Pellentesque et cursus erat. Nullam cursus consequat leo, a laoreet lectus convallis nec. Maecenas eget felis neque. Morbi lacinia neque id sapien dapibus, ac gravida neque pulvinar. Pellentesque rhoncus eu augue a pretium.",
                            LastUpdated = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Location = "FMI",
                            PartnerId = 3L,
                            Time = "17:00",
                            Title = "Post 3",
                            Type = "Workshop"
                        });
                });

            modelBuilder.Entity("Licenta.Core.Entities.File", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime(6)");

                    b.Property<int>("Entity")
                        .HasColumnType("int");

                    b.Property<long?>("EventId")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("LastUpdated")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Name")
                        .HasColumnType("longtext");

                    b.Property<long?>("PartnerId")
                        .HasColumnType("bigint");

                    b.Property<long?>("StudentId")
                        .HasColumnType("bigint");

                    b.Property<string>("Uri")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.HasIndex("EventId");

                    b.HasIndex("PartnerId");

                    b.HasIndex("StudentId");

                    b.ToTable("Files");
                });

            modelBuilder.Entity("Licenta.Core.Entities.Job", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<bool>("Activated")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("Address")
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Criteria")
                        .HasMaxLength(2000)
                        .HasColumnType("varchar(2000)");

                    b.Property<string>("Description")
                        .HasMaxLength(2000)
                        .HasColumnType("varchar(2000)");

                    b.Property<int>("Experience")
                        .HasColumnType("int");

                    b.Property<DateTime>("LastUpdated")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("datetime(6)");

                    b.Property<int>("MaxExperience")
                        .HasColumnType("int");

                    b.Property<int>("MinExperience")
                        .HasColumnType("int");

                    b.Property<long?>("PartnerId")
                        .HasColumnType("bigint");

                    b.Property<string>("Salary")
                        .HasColumnType("longtext");

                    b.Property<string>("Skills")
                        .HasColumnType("longtext");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("PartnerId");

                    b.ToTable("Jobs");

                    b.HasData(
                        new
                        {
                            Id = 1L,
                            Activated = true,
                            Address = "Bucharest",
                            CreatedAt = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Criteria = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed ut quam imperdiet, ullamcorper ex non, efficitur nisi. Aliquam erat volutpat. Nullam et luctus dui, a porttitor lacus. Lorem ipsum dolor sit amet, consectetur adipiscing elit. Pellentesque et cursus erat. Nullam cursus consequat leo, a laoreet lectus convallis nec. Maecenas eget felis neque. Morbi lacinia neque id sapien dapibus, ac gravida neque pulvinar. Pellentesque rhoncus eu augue a pretium. ",
                            Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed ut quam imperdiet, ullamcorper ex non, efficitur nisi. Aliquam erat volutpat. Nullam et luctus dui, a porttitor lacus. Lorem ipsum dolor sit amet, consectetur adipiscing elit. Pellentesque et cursus erat. Nullam cursus consequat leo, a laoreet lectus convallis nec. Maecenas eget felis neque. Morbi lacinia neque id sapien dapibus, ac gravida neque pulvinar. Pellentesque rhoncus eu augue a pretium. ",
                            Experience = 0,
                            LastUpdated = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            MaxExperience = 1,
                            MinExperience = 0,
                            PartnerId = 1L,
                            Salary = "Platit: 2000",
                            Skills = "Problem Solving, Teamwork",
                            Title = "Job 1",
                            Type = 2
                        },
                        new
                        {
                            Id = 2L,
                            Activated = true,
                            Address = "Bucharest",
                            CreatedAt = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Criteria = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed ut quam imperdiet, ullamcorper ex non, efficitur nisi. Aliquam erat volutpat. Nullam et luctus dui, a porttitor lacus. Lorem ipsum dolor sit amet, consectetur adipiscing elit. Pellentesque et cursus erat. Nullam cursus consequat leo, a laoreet lectus convallis nec. Maecenas eget felis neque. Morbi lacinia neque id sapien dapibus, ac gravida neque pulvinar. Pellentesque rhoncus eu augue a pretium. ",
                            Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed ut quam imperdiet, ullamcorper ex non, efficitur nisi. Aliquam erat volutpat. Nullam et luctus dui, a porttitor lacus. Lorem ipsum dolor sit amet, consectetur adipiscing elit. Pellentesque et cursus erat. Nullam cursus consequat leo, a laoreet lectus convallis nec. Maecenas eget felis neque. Morbi lacinia neque id sapien dapibus, ac gravida neque pulvinar. Pellentesque rhoncus eu augue a pretium. ",
                            Experience = 2,
                            LastUpdated = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            MaxExperience = 4,
                            MinExperience = 2,
                            PartnerId = 2L,
                            Salary = "Neplatit",
                            Skills = "Problem Solving, Teamwork",
                            Title = "Job 2",
                            Type = 0
                        });
                });

            modelBuilder.Entity("Licenta.Core.Entities.Partner", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<string>("Address")
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Description")
                        .HasMaxLength(3000)
                        .HasColumnType("varchar(3000)");

                    b.Property<string>("Email")
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<DateTime>("LastUpdated")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("datetime(6)");

                    b.Property<string>("LogoImageUrl")
                        .HasMaxLength(300)
                        .HasColumnType("varchar(300)");

                    b.Property<string>("MainDescription")
                        .HasMaxLength(1000)
                        .HasColumnType("varchar(1000)");

                    b.Property<string>("MainImageUrl")
                        .HasMaxLength(300)
                        .HasColumnType("varchar(300)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.Property<string>("Phone")
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<string>("ProfileImageUrl")
                        .HasColumnType("longtext");

                    b.Property<string>("Social")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Partners");

                    b.HasData(
                        new
                        {
                            Id = 1L,
                            Address = "Bucharest",
                            CreatedAt = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed ut quam imperdiet, ullamcorper ex non, efficitur nisi. Aliquam erat volutpat. Nullam et luctus dui, a porttitor lacus. Lorem ipsum dolor sit amet, consectetur adipiscing elit. Pellentesque et cursus erat. Nullam cursus consequat leo, a laoreet lectus convallis nec. Maecenas eget felis neque. Morbi lacinia neque id sapien dapibus, ac gravida neque pulvinar. Pellentesque rhoncus eu augue a pretium. ",
                            Email = "Email: partner1@gmail.com",
                            LastUpdated = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            MainDescription = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed ut quam imperdiet, ullamcorper ex non, efficitur nisi.",
                            Name = "Partner 1",
                            Phone = "Phone: 0886565767"
                        },
                        new
                        {
                            Id = 2L,
                            Address = "Bucharest",
                            CreatedAt = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed ut quam imperdiet, ullamcorper ex non, efficitur nisi. Aliquam erat volutpat. Nullam et luctus dui, a porttitor lacus. Lorem ipsum dolor sit amet, consectetur adipiscing elit. Pellentesque et cursus erat. Nullam cursus consequat leo, a laoreet lectus convallis nec. Maecenas eget felis neque. Morbi lacinia neque id sapien dapibus, ac gravida neque pulvinar. Pellentesque rhoncus eu augue a pretium.",
                            Email = "Email: office@partner.com",
                            LastUpdated = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            MainDescription = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed ut quam imperdiet, ullamcorper ex non, efficitur nisi.",
                            Name = "Partner 2",
                            Phone = "Phone: 0886435767"
                        },
                        new
                        {
                            Id = 3L,
                            Address = "Bucharest",
                            CreatedAt = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed ut quam imperdiet, ullamcorper ex non, efficitur nisi. Aliquam erat volutpat. Nullam et luctus dui, a porttitor lacus. Lorem ipsum dolor sit amet, consectetur adipiscing elit. Pellentesque et cursus erat. Nullam cursus consequat leo, a laoreet lectus convallis nec. Maecenas eget felis neque. Morbi lacinia neque id sapien dapibus, ac gravida neque pulvinar. Pellentesque rhoncus eu augue a pretium.",
                            Email = "Email: partner3@gmail.com",
                            LastUpdated = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            MainDescription = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed ut quam imperdiet, ullamcorper ex non, efficitur nisi.",
                            Name = "Partner 3",
                            Phone = "Phone: 0775345243"
                        });
                });

            modelBuilder.Entity("Licenta.Core.Entities.Role", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("longtext");

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime?>("LastUpdated")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex");

                    b.ToTable("AspNetRoles", (string)null);
                });

            modelBuilder.Entity("Licenta.Core.Entities.Student", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Degree")
                        .HasColumnType("longtext");

                    b.Property<string>("Description")
                        .HasMaxLength(5000)
                        .HasColumnType("varchar(5000)");

                    b.Property<string>("Email")
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.Property<DateTime>("LastUpdated")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Name")
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.Property<string>("PersonalEmail")
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.Property<string>("Phone")
                        .HasMaxLength(15)
                        .HasColumnType("varchar(15)");

                    b.Property<string>("Skill")
                        .HasMaxLength(1000)
                        .HasColumnType("varchar(1000)");

                    b.HasKey("Id");

                    b.HasIndex("Name", "Email")
                        .IsUnique();

                    b.ToTable("Students");
                });

            modelBuilder.Entity("Licenta.Core.Entities.StudentJob", b =>
                {
                    b.Property<long>("StudentId")
                        .HasColumnType("bigint");

                    b.Property<long>("JobId")
                        .HasColumnType("bigint");

                    b.Property<int>("JobStatus")
                        .HasColumnType("int");

                    b.HasKey("StudentId", "JobId");

                    b.HasIndex("JobId");

                    b.ToTable("StudentJobs");
                });

            modelBuilder.Entity("Licenta.Core.Entities.StudentPartner", b =>
                {
                    b.Property<long>("StudentId")
                        .HasColumnType("bigint");

                    b.Property<long>("PartnerId")
                        .HasColumnType("bigint");

                    b.HasKey("StudentId", "PartnerId");

                    b.HasIndex("PartnerId");

                    b.ToTable("StudentPartners");
                });

            modelBuilder.Entity("Licenta.Core.Entities.User", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("longtext");

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("tinyint(1)");

                    b.Property<DateTime?>("LastUpdated")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("datetime(6)");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("tinyint(1)");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("longtext");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("longtext");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("RefreshToken")
                        .HasColumnType("longtext");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("longtext");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex");

                    b.ToTable("AspNetUsers", (string)null);
                });

            modelBuilder.Entity("Licenta.Core.Entities.UserRole", b =>
                {
                    b.Property<long>("UserId")
                        .HasColumnType("bigint");

                    b.Property<long>("RoleId")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime?>("LastUpdated")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("datetime(6)");

                    b.Property<long>("PartnerId")
                        .HasColumnType("bigint");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<long>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("ClaimType")
                        .HasColumnType("longtext");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("longtext");

                    b.Property<long>("RoleId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<long>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("ClaimType")
                        .HasColumnType("longtext");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("longtext");

                    b.Property<long>("UserId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<long>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("longtext");

                    b.Property<long>("UserId")
                        .HasColumnType("bigint");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<long>", b =>
                {
                    b.Property<long>("UserId")
                        .HasColumnType("bigint");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Name")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Value")
                        .HasColumnType("longtext");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("Licenta.Core.Entities.Event", b =>
                {
                    b.HasOne("Licenta.Core.Entities.Partner", "Partner")
                        .WithMany("Events")
                        .HasForeignKey("PartnerId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.Navigation("Partner");
                });

            modelBuilder.Entity("Licenta.Core.Entities.File", b =>
                {
                    b.HasOne("Licenta.Core.Entities.Event", "Event")
                        .WithMany("Files")
                        .HasForeignKey("EventId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.HasOne("Licenta.Core.Entities.Partner", "Partner")
                        .WithMany("Files")
                        .HasForeignKey("PartnerId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.HasOne("Licenta.Core.Entities.Student", "Student")
                        .WithMany("Files")
                        .HasForeignKey("StudentId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.Navigation("Event");

                    b.Navigation("Partner");

                    b.Navigation("Student");
                });

            modelBuilder.Entity("Licenta.Core.Entities.Job", b =>
                {
                    b.HasOne("Licenta.Core.Entities.Partner", "Partner")
                        .WithMany("Jobs")
                        .HasForeignKey("PartnerId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.Navigation("Partner");
                });

            modelBuilder.Entity("Licenta.Core.Entities.StudentJob", b =>
                {
                    b.HasOne("Licenta.Core.Entities.Job", "Job")
                        .WithMany("StudentJobs")
                        .HasForeignKey("JobId")
                        .OnDelete(DeleteBehavior.NoAction);

                    b.HasOne("Licenta.Core.Entities.Student", "Student")
                        .WithMany("StudentJobs")
                        .HasForeignKey("StudentId")
                        .OnDelete(DeleteBehavior.NoAction);

                    b.Navigation("Job");

                    b.Navigation("Student");
                });

            modelBuilder.Entity("Licenta.Core.Entities.StudentPartner", b =>
                {
                    b.HasOne("Licenta.Core.Entities.Partner", "Partner")
                        .WithMany("StudentPartners")
                        .HasForeignKey("PartnerId")
                        .OnDelete(DeleteBehavior.NoAction);

                    b.HasOne("Licenta.Core.Entities.Student", "Student")
                        .WithMany("StudentPartners")
                        .HasForeignKey("StudentId")
                        .OnDelete(DeleteBehavior.NoAction);

                    b.Navigation("Partner");

                    b.Navigation("Student");
                });

            modelBuilder.Entity("Licenta.Core.Entities.UserRole", b =>
                {
                    b.HasOne("Licenta.Core.Entities.Role", "Role")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Licenta.Core.Entities.User", "User")
                        .WithMany("UserRoles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<long>", b =>
                {
                    b.HasOne("Licenta.Core.Entities.Role", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<long>", b =>
                {
                    b.HasOne("Licenta.Core.Entities.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<long>", b =>
                {
                    b.HasOne("Licenta.Core.Entities.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<long>", b =>
                {
                    b.HasOne("Licenta.Core.Entities.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Licenta.Core.Entities.Event", b =>
                {
                    b.Navigation("Files");
                });

            modelBuilder.Entity("Licenta.Core.Entities.Job", b =>
                {
                    b.Navigation("StudentJobs");
                });

            modelBuilder.Entity("Licenta.Core.Entities.Partner", b =>
                {
                    b.Navigation("Events");

                    b.Navigation("Files");

                    b.Navigation("Jobs");

                    b.Navigation("StudentPartners");
                });

            modelBuilder.Entity("Licenta.Core.Entities.Student", b =>
                {
                    b.Navigation("Files");

                    b.Navigation("StudentJobs");

                    b.Navigation("StudentPartners");
                });

            modelBuilder.Entity("Licenta.Core.Entities.User", b =>
                {
                    b.Navigation("UserRoles");
                });
#pragma warning restore 612, 618
        }
    }
}
