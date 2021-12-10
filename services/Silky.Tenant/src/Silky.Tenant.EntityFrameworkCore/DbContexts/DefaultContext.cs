using Microsoft.EntityFrameworkCore;
using Silky.EntityFrameworkCore.Contexts.Attributes;
using Silky.Hero.Common.EntityFrameworkCore.Contexts;
using Silky.Tenant.Domain;

namespace Silky.Tenant.EntityFrameworkCore.DbContexts
{
    [AppDbContext(TenantDbProperties.ConnectionStringName,DbProvider.MySql)]
    public class DefaultContext : HeroDbContext<DefaultContext>
    {
        public DefaultContext(DbContextOptions<DefaultContext> options) : base(options)
        {
        }

        public DbSet<Domain.Tenant> Tenants { get; set; }
        
    }
}