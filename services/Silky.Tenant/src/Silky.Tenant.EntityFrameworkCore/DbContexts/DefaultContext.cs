using Microsoft.EntityFrameworkCore;
using Silky.EntityFrameworkCore.Contexts;
using Silky.EntityFrameworkCore.Contexts.Attributes;
using Silky.Tenant.Domain;

namespace Silky.Tenant.EntityFrameworkCore.DbContexts
{
    [AppDbContext(TenantDbProperties.ConnectionStringName,DbProvider.MySql)]
    public class DefaultContext : SilkyDbContext<DefaultContext>
    {
        public DefaultContext(DbContextOptions<DefaultContext> options) : base(options)
        {
        }
    }
}