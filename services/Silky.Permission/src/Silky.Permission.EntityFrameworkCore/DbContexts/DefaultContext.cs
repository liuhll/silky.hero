using Microsoft.EntityFrameworkCore;
using Silky.EntityFrameworkCore.Contexts.Attributes;
using Silky.EntityFrameworkCore.Extras.Contexts;
using Silky.Permission.Domain;
using Silky.Permission.Domain.Menu;

namespace Silky.Permission.EntityFrameworkCore.DbContexts
{
    [AppDbContext(PermissionDbProperties.ConnectionStringName, DbProvider.MySql)]
    public class DefaultContext : SilkyDbContext<DefaultContext>
    {
        public DefaultContext(DbContextOptions<DefaultContext> options) : base(options)
        {
        }

        public DbSet<Menu> Menus { get; set; }
    }
}