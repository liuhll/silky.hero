using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.Claims;
using JetBrains.Annotations;
using Silky.Core;
using Silky.Core.Exceptions;
using Silky.Core.Extensions.Collections.Generic;
using Silky.EntityFrameworkCore.Extras.Entities;
using Silky.Hero.Common.EntityFrameworkCore.Entities;
using Silky.Hero.Common.Enums;
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
    
    public string Remark { get; set; }

    public virtual DataRange DataRange { get; protected internal set; }

    public string ConcurrencyStamp { get; set; }
    
    public Status Status { get; set; }

    public int Sort { get; set; }

    public IdentityRole()
    {
        Status = Status.Valid;
        
        DataRange = DataRange.SelfOrganization;
        Claims = new Collection<IdentityRoleClaim>();
        CustomOrganizationDataRanges = new List<IdentityRoleOrganization>();
        Menus = new List<IdentityRoleMenu>();
    }

    public IdentityRole([NotNull] string name, [NotNull] string realName, object tenantId = null) : this()
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
        var menu = Menus.FirstOrDefault(p => p.MenuId == menuId && p.TenantId == TenantId);
        if (menu != null)
        {
            Menus.Remove(menu);
        }
    }

    public virtual void RemoveMenus(IEnumerable<long> menuIds)
    {
        foreach (var menuId in menuIds)
        {
            RemoveMenu(menuId);
        }
    }

    public virtual void AddCustomOrganizationDataRanges(IEnumerable<IdentityRoleOrganization> identityRoleOrganizations)
    {
        foreach (var identityRoleOrganization in identityRoleOrganizations)
        {
            AddCustomOrganizationDataRange(identityRoleOrganization);
        }
    }

    public virtual void AddCustomOrganizationDataRange(IdentityRoleOrganization identityRoleOrganization)
    {
        if (DataRange != DataRange.CustomOrganization)
        {
            throw new BusinessException("只有自定义数据权限范围,才允许添加IdentityRoleOrganization数据");
        }

        if (identityRoleOrganization.RoleId != Id)
        {
            throw new BusinessException("增加数据权限机构失败,角色Id不一致");
        }

        if (!CustomOrganizationDataRanges.Any(p =>
                p.OrganizationId != identityRoleOrganization.OrganizationId &&
                p.TenantId != identityRoleOrganization.TenantId))
        {
            CustomOrganizationDataRanges.Add(identityRoleOrganization);
        }
    }

    public void RemoveCustomOrganizationDataRanges(IEnumerable<long> organizationIds)
    {
        foreach (var organizationId in organizationIds)
        {
            RemoveCustomOrganizationDataRange(organizationId);
        }
    }

    public void RemoveCustomOrganizationDataRange(long organizationId)
    {
        if (DataRange != DataRange.CustomOrganization)
        {
            throw new BusinessException("只有自定义数据权限范围,才允许移除IdentityRoleOrganization数据");
        }

        var customRoleOrganization =
            CustomOrganizationDataRanges.FirstOrDefault(p =>
                p.OrganizationId == organizationId && p.TenantId == TenantId);
        if (customRoleOrganization != null)
        {
            CustomOrganizationDataRanges.Remove(customRoleOrganization);
        }
    }

    public virtual void SetDataRange(DataRange dataRange)
    {
        if (dataRange != DataRange.CustomOrganization)
        {
            CustomOrganizationDataRanges.Clear();
        }

        DataRange = dataRange;
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

        if (!Menus.Any(p => p.MenuId != roleMenu.MenuId && p.TenantId != roleMenu.TenantId))
        {
            Menus.Add(roleMenu);
        }
    }
}