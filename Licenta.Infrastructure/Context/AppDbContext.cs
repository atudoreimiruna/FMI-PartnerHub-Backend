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
    public DbSet<Post> Posts { get; set; }
    public DbSet<Image> Images { get; set; }
    public DbSet<Job> Jobs { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfiguration(new NewsLetterConfiguration());
        modelBuilder.ApplyConfiguration(new FeedbackConfiguration());
        modelBuilder.ApplyConfiguration(new PartnerConfiguration());
        modelBuilder.ApplyConfiguration(new PostConfiguration());
        modelBuilder.ApplyConfiguration(new ImageConfiguration());
        modelBuilder.ApplyConfiguration(new JobConfiguration());
    }
}
