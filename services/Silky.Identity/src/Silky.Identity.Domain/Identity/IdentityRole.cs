using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.Claims;
using JetBrains.Annotations;
using Silky.Core;
using Silky.Core.Exceptions;
using Silky.Core.Extensions.Collections.Generic;
using Silky.Hero.Common.EntityFrameworkCore.Entities;
using Silky.Identity.Domain.Shared;

namespace Silky.Identity.Domain;

public class IdentityRole : FullAuditedEntity, IHasConcurrencyStamp
{
    public virtual string Name { get; protected internal set; }

    public string RealName { get; protected internal set; }

    public virtual string NormalizedName { get; protected internal set; }

    public virtual ICollection<IdentityRoleClaim> Claims { get; protected set; }

    public virtual ICollection<IdentityRoleOrganization> CustomOrganizationDataRanges { get; protected set; }

    public virtual ICollection<IdentityRoleMenu> Menus { get; protected set; }

    public virtual bool IsDefault { get; set; }

    public virtual bool IsStatic { get; set; }

    public virtual bool IsPublic { get; set; }

    public virtual DataRange DataRange { get; protected internal set; }

    public string ConcurrencyStamp { get; set; }

    public int Sort { get; set; }

    public IdentityRole()
    {
        DataRange = DataRange.SelfOrganization;
        Claims = new Collection<IdentityRoleClaim>();
        CustomOrganizationDataRanges = new List<IdentityRoleOrganization>();
        Menus = new List<IdentityRoleMenu>();
        DataRange = DataRange.SelfOrganization;
    }

    public IdentityRole([NotNull] string name, [NotNull] string realName, object tenantId = null)
    {
        Check.NotNull(name, nameof(name));
        Name = name;
        RealName = realName;
        NormalizedName = name.ToUpperInvariant();

        if (tenantId != null)
        {
            TenantId = long.Parse(tenantId.ToString());
        }
    }

    public virtual void UpdateDataRange(DataRange dataRange)
    {
        if (DataRange != DataRange.CustomOrganization)
        {
            CustomOrganizationDataRanges.Clear();
        }

        DataRange = dataRange;
    }

    public virtual void AddMenu(long menuId)
    {
        if (Menus.All(p => p.MenuId != menuId))
        {
            Menus.Add(new IdentityRoleMenu(Id, menuId, TenantId));
        }
    }

    public virtual void AddMenus(IEnumerable<long> menuIds)
    {
        foreach (var menuId in menuIds)
        {
            AddMenu(menuId);
        }
    }

    public virtual void RemoveMenu(long menuId)
    {
        var menu = Menus.FirstOrDefault(p => p.MenuId == menuId);
        if (menu != null)
        {
            Menus.Remove(menu);
        }
    }

    public virtual void RemoveMenu(IEnumerable<long> menuIds)
    {
        foreach (var menuId in menuIds)
        {
            RemoveMenu(menuId);
        }
    }

    public virtual void AddOrganizationDataRange(long organizationId)
    {
        if (DataRange != DataRange.CustomOrganization)
        {
            DataRange = DataRange.CustomOrganization;
        }

        if (CustomOrganizationDataRanges.All(p => p.OrganizationId != organizationId))
        {
            CustomOrganizationDataRanges.Add(new IdentityRoleOrganization(Id, organizationId));
        }
    }

    public virtual void AddOrganizationDataRanges(IEnumerable<long> organizationIds)
    {
        if (DataRange != DataRange.CustomOrganization)
        {
            DataRange = DataRange.CustomOrganization;
        }

        foreach (var organizationId in organizationIds)
        {
            if (CustomOrganizationDataRanges.All(p => p.OrganizationId != organizationId))
            {
                CustomOrganizationDataRanges.Add(new IdentityRoleOrganization(Id, organizationId));
            }
        }
    }

    public virtual void AddClaim([NotNull] Claim claim)
    {
        Check.NotNull(claim, nameof(claim));

        Claims.Add(new IdentityRoleClaim(Id, claim, TenantId));
    }

    public virtual void AddClaims([NotNull] IEnumerable<Claim> claims)
    {
        Check.NotNull(claims, nameof(claims));

        foreach (var claim in claims)
        {
            AddClaim(claim);
        }
    }

    public virtual IdentityRoleClaim FindClaim([NotNull] Claim claim)
    {
        Check.NotNull(claim, nameof(claim));

        return Claims.FirstOrDefault(c => c.ClaimType == claim.Type && c.ClaimValue == claim.Value);
    }

    public virtual void RemoveClaim([NotNull] Claim claim)
    {
        Check.NotNull(claim, nameof(claim));

        Claims.RemoveAll(c => c.ClaimType == claim.Type && c.ClaimValue == claim.Value);
    }

    public virtual void ChangeName(string name)
    {
        Check.NotNullOrWhiteSpace(name, nameof(name));

        Name = name;
    }

    public void ChangeRealName(string realName)
    {
        Check.NotNullOrWhiteSpace(realName, nameof(realName));

        RealName = realName;
    }

    public override string ToString()
    {
        return $"{base.ToString()}, Name = {Name}";
    }

    public void AddMenus(IEnumerable<IdentityRoleMenu> roleMenus)
    {
        foreach (var roleMenu in roleMenus)
        {
            AddMenu(roleMenu);
        }
    }

    public void AddMenu(IdentityRoleMenu roleMenu)
    {
        if (roleMenu.RoleId != Id)
        {
            throw new BusinessException("增加角色菜单错误,角色Id不一致");
        }

        if (Menus.All(p => p.MenuId != roleMenu.MenuId && p.TenantId != roleMenu.TenantId))
        {
            Menus.Add(roleMenu);
        }

        Menus.Add(roleMenu);
    }
}