using Microsoft.EntityFrameworkCore;
using Silky.EntityFrameworkCore.Contexts;
using Silky.EntityFrameworkCore.Contexts.Attributes;

namespace Silky.Log.EntityFrameworkCore.DbContexts
{
    [AppDbContext("default",DbProvider.MySql)]
    public class DefaultContext : SilkyDbContext<DefaultContext>
    {
        public DefaultContext(DbContextOptions<DefaultContext> options) : base(options)
        {
        }
    }
}