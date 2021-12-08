using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using Silky.Core;
using Silky.Hero.Common.EntityFrameworkCore.Modeling;
using Silky.Identity.Domain;
using Silky.Identity.Domain.Shared;

namespace Silky.Identity.EntityFrameworkCore.DbContexts;

public static class DbContextModelBuilderExtensions
{
    public static void ConfigureIdentity([NotNull] this ModelBuilder builder)
    {
        Check.NotNull(builder, nameof(builder));

        builder.Entity<IdentityUser>(b =>
        {
            b.ToTable(HeroIdentityDbProperties.DbTablePrefix + "Users", HeroIdentityDbProperties.DbSchema);

            b.ConfigureByConvention();
            b.ConfigureHeroUser();

            b.Property(u => u.NormalizedUserName).IsRequired()
                .HasMaxLength(IdentityUserConsts.MaxNormalizedUserNameLength)
                .HasColumnName(nameof(IdentityUser.NormalizedUserName));
            b.Property(u => u.NormalizedEmail).IsRequired()
                .HasMaxLength(IdentityUserConsts.MaxNormalizedEmailLength)
                .HasColumnName(nameof(IdentityUser.NormalizedEmail));
            b.Property(u => u.PasswordHash).HasMaxLength(IdentityUserConsts.MaxPasswordHashLength)
                .HasColumnName(nameof(IdentityUser.PasswordHash));
            b.Property(u => u.SecurityStamp).IsRequired().HasMaxLength(IdentityUserConsts.MaxSecurityStampLength)
                .HasColumnName(nameof(IdentityUser.SecurityStamp));

            b.Property(u => u.LockoutEnabled).HasDefaultValue(false)
                .HasColumnName(nameof(IdentityUser.LockoutEnabled));

            b.Property(u => u.AccessFailedCount)
                .HasDefaultValue(0)
                .HasColumnName(nameof(IdentityUser.AccessFailedCount));

            b.Property(u => u.EmailConfirmed).HasDefaultValue(false).HasColumnName(nameof(IdentityUser.EmailConfirmed));
            b.Property(u => u.PhoneNumberConfirmed).HasDefaultValue(false)
                .HasColumnName(nameof(IdentityUser.PhoneNumberConfirmed));

            b.Property(u => u.LockoutEnabled).HasDefaultValue(false)
                .HasColumnName(nameof(IdentityUser.LockoutEnabled));

            b.HasMany(u => u.Claims).WithOne().HasForeignKey(uc => uc.UserId).IsRequired();
            b.HasMany(u => u.Logins).WithOne().HasForeignKey(ul => ul.UserId).IsRequired();
            b.HasMany(u => u.Roles).WithOne().HasForeignKey(ur => ur.UserId).IsRequired();
            b.HasMany(u => u.Tokens).WithOne().HasForeignKey(ur => ur.UserId).IsRequired();
            b.HasMany(u => u.UserSubsidiaries).WithOne().HasForeignKey(ur => ur.UserId).IsRequired();

            b.HasIndex(u => u.NormalizedUserName);
            b.HasIndex(u => u.NormalizedEmail);
            b.HasIndex(u => u.UserName);
            b.HasIndex(u => u.Email);
        });

        builder.Entity<IdentityUserClaim>(b =>
        {
            b.ToTable(HeroIdentityDbProperties.DbTablePrefix + "UserClaims", HeroIdentityDbProperties.DbSchema);

            b.ConfigureByConvention();
            
            b.Property(uc => uc.ClaimType).HasMaxLength(IdentityUserClaimConsts.MaxClaimTypeLength).IsRequired();
            b.Property(uc => uc.ClaimValue).HasMaxLength(IdentityUserClaimConsts.MaxClaimValueLength);

            b.HasIndex(uc => uc.UserId);
        });

        builder.Entity<IdentityUserRole>(b =>
        {
            b.ToTable(HeroIdentityDbProperties.DbTablePrefix + "UserRoles", HeroIdentityDbProperties.DbSchema);

            b.ConfigureByConvention();

            b.HasOne<IdentityRole>().WithMany().HasForeignKey(ur => ur.RoleId).IsRequired();
            b.HasOne<IdentityUser>().WithMany(u => u.Roles).HasForeignKey(ur => ur.UserId).IsRequired();

            b.HasIndex(ur => new { ur.RoleId, ur.UserId });
        });

        builder.Entity<IdentityUserLogin>(b =>
        {
            b.ToTable(HeroIdentityDbProperties.DbTablePrefix + "UserLogins", HeroIdentityDbProperties.DbSchema);

            b.ConfigureByConvention();

            b.Property(ul => ul.LoginProvider).HasMaxLength(IdentityUserLoginConsts.MaxLoginProviderLength)
                .IsRequired();
            b.Property(ul => ul.ProviderKey).HasMaxLength(IdentityUserLoginConsts.MaxProviderKeyLength)
                .IsRequired();
            b.Property(ul => ul.ProviderDisplayName)
                .HasMaxLength(IdentityUserLoginConsts.MaxProviderDisplayNameLength);

            b.HasIndex(l => new { l.LoginProvider, l.ProviderKey });
        });

        builder.Entity<IdentityUserToken>(b =>
        {
            b.ToTable(HeroIdentityDbProperties.DbTablePrefix + "UserTokens", HeroIdentityDbProperties.DbSchema);

            b.ConfigureByConvention();

            b.Property(ul => ul.LoginProvider).HasMaxLength(IdentityUserTokenConsts.MaxLoginProviderLength)
                .IsRequired();
            b.Property(ul => ul.Name).HasMaxLength(IdentityUserTokenConsts.MaxNameLength).IsRequired();
        });

        builder.Entity<IdentityRole>(b =>
        {
            b.ToTable(HeroIdentityDbProperties.DbTablePrefix + "Roles", HeroIdentityDbProperties.DbSchema);

            b.ConfigureByConvention();

            b.Property(r => r.Name).IsRequired().HasMaxLength(IdentityRoleConsts.MaxNameLength);
            b.Property(r => r.RealName).IsRequired().HasMaxLength(IdentityRoleConsts.MaxNameLength);
            b.Property(r => r.NormalizedName).IsRequired().HasMaxLength(IdentityRoleConsts.MaxNormalizedNameLength);
            b.Property(r => r.IsDefault).HasColumnName(nameof(IdentityRole.IsDefault));
            b.Property(r => r.IsStatic).HasColumnName(nameof(IdentityRole.IsStatic));
            b.Property(r => r.IsPublic).HasColumnName(nameof(IdentityRole.IsPublic));

            b.HasMany(r => r.Claims).WithOne().HasForeignKey(rc => rc.RoleId).IsRequired();

            b.HasIndex(r => r.NormalizedName);
        });

        builder.Entity<IdentityRoleClaim>(b =>
        {
            b.ToTable(HeroIdentityDbProperties.DbTablePrefix + "RoleClaims", HeroIdentityDbProperties.DbSchema);

            b.ConfigureByConvention();
            
            b.Property(uc => uc.ClaimType).HasMaxLength(IdentityRoleClaimConsts.MaxClaimTypeLength).IsRequired();
            b.Property(uc => uc.ClaimValue).HasMaxLength(IdentityRoleClaimConsts.MaxClaimValueLength);

            b.HasIndex(uc => uc.RoleId);
        });

        builder.Entity<IdentityClaimType>(b =>
        {
            b.ToTable(HeroIdentityDbProperties.DbTablePrefix + "ClaimTypes", HeroIdentityDbProperties.DbSchema);

            b.ConfigureByConvention();

            b.Property(uc => uc.Name).HasMaxLength(IdentityClaimTypeConsts.MaxNameLength)
                .IsRequired(); // make unique
            b.Property(uc => uc.Regex).HasMaxLength(IdentityClaimTypeConsts.MaxRegexLength);
            b.Property(uc => uc.RegexDescription).HasMaxLength(IdentityClaimTypeConsts.MaxRegexDescriptionLength);
            b.Property(uc => uc.Description).HasMaxLength(IdentityClaimTypeConsts.MaxDescriptionLength);
        });

        builder.Entity<UserSubsidiary>(b =>
        {
            b.ToTable(HeroIdentityDbProperties.DbTablePrefix + "UserSubsidiaries", HeroIdentityDbProperties.DbSchema);

            b.ConfigureByConvention();
        });
    }
}