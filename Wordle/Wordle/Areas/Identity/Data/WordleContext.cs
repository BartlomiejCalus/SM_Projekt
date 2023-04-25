using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NuGet.Protocol;
using Wordle.Areas.Identity.Data;
using Wordle.Models;

namespace Wordle.Data;

public class WordleContext : IdentityDbContext<WordleUser>
{
    public WordleContext(DbContextOptions<WordleContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<WordleUser>()
        .HasMany(u => u.GameStats).WithOne(g => g.user).IsRequired(true).HasForeignKey(g => g.userId);
        builder.Entity<GameStat>();
       // .HasKey(g => g.statsId);
        // Customize the ASP.NET Identity model and override the defaults if needed.
        // For example, you can rename the ASP.NET Identity table names and more.
        // Add your customizations after calling base.OnModelCreating(builder);
        builder.ApplyConfiguration(new WordleUserEntityConfiguration());  
    }
}
public class WordleUserEntityConfiguration : IEntityTypeConfiguration<WordleUser>
{
    void IEntityTypeConfiguration<WordleUser>.Configure(EntityTypeBuilder<WordleUser> builder)
    {
        builder.Property(u => u.Nickname).HasMaxLength(32);
    }
}

