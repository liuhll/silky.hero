using Microsoft.EntityFrameworkCore;
using Silky.EntityFrameworkCore.Contexts.Attributes;
using Silky.Hero.Common.EntityFrameworkCore.Contexts;
using Silky.Organization.Domain.Organizations;

namespace Silky.Organization.EntityFrameworkCore.DbContexts
{
    [AppDbContext(OrganizationDbProperties.ConnectionStringName, DbProvider.MySql)]
    public class DefaultDbContext : HeroDbContext<DefaultDbContext>
    {
        public DefaultDbContext(DbContextOptions<DefaultDbContext> options) : base(options)
        {
        }

        public DbSet<Domain.Organizations.Organization> Organizations { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ConfigureOrganization();
        }
    }
}