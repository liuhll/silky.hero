using Microsoft.EntityFrameworkCore;
using Silky.EntityFrameworkCore.Contexts.Attributes;
using Silky.EntityFrameworkCore.Extras.Contexts;
using Silky.Saas.Domain;

namespace Silky.Saas.EntityFrameworkCore.DbContexts
{
    [AppDbContext(TenantDbProperties.ConnectionStringName,DbProvider.MySql)]
    public class DefaultContext : SilkyDbContext<DefaultContext>
    {
        public DefaultContext(DbContextOptions<DefaultContext> options) : base(options)
        {
        }
        
    }
}