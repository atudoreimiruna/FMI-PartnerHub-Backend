using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Licenta.Core.Entities;
using Licenta.Infrastructure.EntityConfigurations;

namespace Licenta.Infrastructure;

public class AppDbContext : IdentityDbContext<
    User,
    Role,
    int,
    IdentityUserClaim<int>,
    UserRole,
    IdentityUserLogin<int>,
    IdentityRoleClaim<int>,
    IdentityUserToken<int>>

{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {

    }
    public DbSet<Newsletter> Newsletters { get; set; } 
    public DbSet<Feedback> Feedbacks { get; set; }
    public DbSet<Partner> Partners { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfiguration(new NewsLetterConfiguration());
        modelBuilder.ApplyConfiguration(new FeedbackConfiguration());
        modelBuilder.ApplyConfiguration(new PartnerConfiguration());
    }
    
}
