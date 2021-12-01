using Microsoft.EntityFrameworkCore;
using Silky.EntityFrameworkCore.Contexts.Attributes;
using Silky.Hero.Common.EntityFrameworkCore.Contexts;
using Silky.Position.Domain.Positions;

namespace Silky.Position.EntityFrameworkCore.DbContexts
{
    [AppDbContext(PositionDbProperties.ConnectionStringName, DbProvider.MySql)]
    public class DefaultContext : HeroDbContext<DefaultContext>
    {
        public DefaultContext(DbContextOptions<DefaultContext> options) : base(options)
        {
        }

        public DbSet<Domain.Positions.Position> Positions { get; set; }
    }
}