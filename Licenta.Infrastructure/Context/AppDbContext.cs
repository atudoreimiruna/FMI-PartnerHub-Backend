using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Licenta.Core.Entities;
using Licenta.Infrastructure.EntityConfigurations;

namespace Licenta.Infrastructure;

public class AppDbContext : IdentityDbContext<
    User,
    Role,
    long,
    IdentityUserClaim<long>,
    UserRole,
    IdentityUserLogin<long>,
    IdentityRoleClaim<long>,
    IdentityUserToken<long>>

{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {

    }
    public DbSet<Partner> Partners { get; set; }
    public DbSet<Event> Events { get; set; }
    public DbSet<Core.Entities.File> Files { get; set; }
    public DbSet<Job> Jobs { get; set; }
    public DbSet<Student> Students { get; set; }
    public DbSet<StudentJob> StudentJobs { get; set; }
    public DbSet<StudentPartner> StudentPartners { get; set; }
    public DbSet<Model> Models { get; set; }
    public DbSet<Practice> Practices { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfiguration(new PartnerConfiguration());
        modelBuilder.ApplyConfiguration(new EventConfiguration());
        modelBuilder.ApplyConfiguration(new FileConfiguration());
        modelBuilder.ApplyConfiguration(new JobConfiguration());
        modelBuilder.ApplyConfiguration(new StudentConfiguration());
        modelBuilder.ApplyConfiguration(new StudentJobConfiguration());
        modelBuilder.ApplyConfiguration(new StudentPartnerConfiguration());
        modelBuilder.ApplyConfiguration(new PracticeConfiguration());

        modelBuilder.Entity<User>(b =>
        {
            b.HasMany(u => u.UserRoles)
             .WithOne(ur => ur.User)
             .HasForeignKey(ur => ur.UserId)
             .IsRequired();
        });

        modelBuilder.Entity<Role>(role =>
        {
            role.HasMany<UserRole>()
                .WithOne(ur => ur.Role)
                .HasForeignKey(ur => ur.RoleId)
                .IsRequired();
        });

        modelBuilder.Entity<UserRole>(userRole =>
        {
            userRole.HasKey(r => new { r.UserId, r.RoleId });
        });
    }

}
