using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Silky.EntityFrameworkCore.Contexts.Attributes;
using Silky.EntityFrameworkCore.Entities.Configures;
using Silky.Hero.Common.EntityFrameworkCore.Contexts;
using Silky.Identity.Domain;

namespace Silky.Identity.EntityFrameworkCore.DbContexts
{
    [AppDbContext(HeroIdentityDbProperties.ConnectionStringName, DbProvider.MySql)]
    public class DefaultContext : HeroContext<DefaultContext>, IModelBuilderFilter
    {
        public DefaultContext(DbContextOptions<DefaultContext> options) : base(options)
        {
        }

        public DbSet<IdentityUser> Users { get; set; }

        public DbSet<IdentityRole> Roles { get; set; }

        public DbSet<IdentityClaimType> ClaimTypes { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ConfigureIdentity();
        }

        public void OnCreating(ModelBuilder modelBuilder, EntityTypeBuilder entityBuilder, DbContext dbContext,
            Type dbContextLocator)
        {
            OnEntityCreating(modelBuilder, entityBuilder, dbContext, dbContextLocator);
        }
    }
}