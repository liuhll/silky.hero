using Microsoft.EntityFrameworkCore;
using Silky.EntityFrameworkCore.Contexts.Attributes;
using Silky.Hero.Common.EntityFrameworkCore.Contexts;
using Silky.Identity.Domain;

namespace Silky.Identity.EntityFrameworkCore.DbContexts
{
    [AppDbContext(HeroIdentityDbProperties.ConnectionStringName,DbProvider.MySql)]
    public class DefaultContext : HeroContext<DefaultContext>
    {
        public DefaultContext(DbContextOptions<DefaultContext> options) : base(options)
        {
        }
        
        public DbSet<IdentityUser> Users { get; set; }

        public DbSet<IdentityRole> Roles { get; set; }

        public DbSet<IdentityClaimType> ClaimTypes { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ConfigureIdentity();
        }
    }
}