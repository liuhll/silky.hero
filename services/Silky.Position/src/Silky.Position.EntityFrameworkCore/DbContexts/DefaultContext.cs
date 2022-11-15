using Microsoft.EntityFrameworkCore;
using Silky.EntityFrameworkCore.Contexts.Attributes;
using Silky.EntityFrameworkCore.Extras.Contexts;
using Silky.Position.Domain;

namespace Silky.Position.EntityFrameworkCore.DbContexts
{
    [AppDbContext(PositionDbProperties.ConnectionStringName, DbProvider.MySql)]
    public class DefaultContext : SilkyDbContext<DefaultContext>
    {
        public DefaultContext(DbContextOptions<DefaultContext> options) : base(options)
        {
        }

        public DbSet<Domain.Position> Positions { get; set; }
    }
}