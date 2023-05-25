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
    public DbSet<Newsletter> Newsletters { get; set; } 
    public DbSet<Feedback> Feedbacks { get; set; }
    public DbSet<Partner> Partners { get; set; }
    public DbSet<Event> Events { get; set; }
    public DbSet<Core.Entities.File> Files { get; set; }
    public DbSet<Job> Jobs { get; set; }
    public DbSet<Student> Students { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfiguration(new NewsLetterConfiguration());
        modelBuilder.ApplyConfiguration(new FeedbackConfiguration());
        modelBuilder.ApplyConfiguration(new PartnerConfiguration());
        modelBuilder.ApplyConfiguration(new EventConfiguration());
        modelBuilder.ApplyConfiguration(new FileConfiguration());
        modelBuilder.ApplyConfiguration(new JobConfiguration());
        modelBuilder.ApplyConfiguration(new StudentConfiguration());
    }
}
