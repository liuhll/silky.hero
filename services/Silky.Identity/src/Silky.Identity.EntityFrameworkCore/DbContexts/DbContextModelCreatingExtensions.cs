using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Silky.Identity.Domain;
using Silky.Identity.Domain.Shared;

namespace Silky.Identity.EntityFrameworkCore.DbContexts;

public static class DbContextModelCreatingExtensions
{
    public static void ConfigureHeroUser<TUser>(this EntityTypeBuilder<TUser> b)
        where TUser : IdentityUser
    {
        b.Property(u => u.TenantId).HasColumnName(nameof(IdentityUser.TenantId));
        b.Property(u => u.UserName).IsRequired().HasMaxLength(IdentityUserConsts.MaxUserNameLength).HasColumnName(nameof(IdentityUser.UserName));
        b.Property(u => u.Email).IsRequired().HasMaxLength(IdentityUserConsts.MaxEmailLength).HasColumnName(nameof(IdentityUser.Email));
        b.Property(u => u.RealName).HasMaxLength(IdentityUserConsts.MaxRealNameLength).HasColumnName(nameof(IdentityUser.RealName));
        b.Property(u => u.Surname).HasMaxLength(IdentityUserConsts.MaxSurnameLength).HasColumnName(nameof(IdentityUser.Surname));
        b.Property(u => u.MobilePhone).HasMaxLength(IdentityUserConsts.MaxPhoneNumberLength).HasColumnName(nameof(IdentityUser.MobilePhone));
        b.Property(u => u.TelPhone).HasMaxLength(IdentityUserConsts.MaxPhoneNumberLength).HasColumnName(nameof(IdentityUser.TelPhone));
    }
}