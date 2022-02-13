using Microsoft.EntityFrameworkCore;
using Silky.EntityFrameworkCore.Contexts.Attributes;
using Silky.Hero.Common.EntityFrameworkCore.Contexts;
using Silky.Log.Domain.AuditLogging;

namespace Silky.Log.EntityFrameworkCore.DbContexts
{
    [AppDbContext("default",DbProvider.MySql)]
    public class DefaultContext : HeroDbContext<DefaultContext>
    {
        public DefaultContext(DbContextOptions<DefaultContext> options) : base(options)
        {
        }
        
        public DbSet<AuditLog> AuditLogs { get; set; }
        
        public DbSet<AuditLogAction> AuditLogActions { get; set; }
    }
}