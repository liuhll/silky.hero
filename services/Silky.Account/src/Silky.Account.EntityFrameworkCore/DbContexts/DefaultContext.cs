using Microsoft.EntityFrameworkCore;
using Silky.Account.Domain.Users;
using Silky.EntityFrameworkCore.Contexts;
using Silky.EntityFrameworkCore.Contexts.Attributes;

namespace Silky.Account.EntityFrameworkCore.DbContexts
{
    [AppDbContext("default", DbProvider.MySql)]
    public class DefaultContext : SilkyDbContext<DefaultContext>
    {
        public DefaultContext(DbContextOptions<DefaultContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }

        public DbSet<UserSubsidiary> UserSubsidiaries { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // modelBuilder.Entity<UserSubsidiary>()
            //     .HasOne(p => p.User)
            //     .WithMany(p => p.UserSubsidiaries)
            //     .HasForeignKey(f => f.UserId);
        }
    }
}