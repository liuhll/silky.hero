using Microsoft.EntityFrameworkCore;
using Silky.EntityFrameworkCore.Contexts.Attributes;
using Silky.Hero.Common.EntityFrameworkCore.Contexts;
using Silky.Permission.Domain;
using Silky.Permission.Domain.Menu;

namespace Silky.Permission.EntityFrameworkCore.DbContexts
{
    [AppDbContext(PermissionDbProperties.ConnectionStringName, DbProvider.MySql)]
    public class DefaultContext : HeroDbContext<DefaultContext>
    {
        public DefaultContext(DbContextOptions<DefaultContext> options) : base(options)
        {
        }

        public DbSet<Menu> Menus { get; set; }
    }
}