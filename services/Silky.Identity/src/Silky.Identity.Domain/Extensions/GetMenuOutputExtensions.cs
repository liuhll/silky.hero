using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Silky.Core.Extensions;
using Silky.Hero.Common.Enums;
using Silky.Identity.Domain.Identity;
using Silky.Identity.Domain.Shared;
using Silky.Permission.Application.Contracts.Menu.Dtos;
using Silky.Permission.Domain.Shared.Menu;

namespace Silky.Identity.Domain.Extensions;

internal static class GetMenuOutputExtensions
{
    public static ICollection<FrontendMenu> MapFrontendMenus(this IEnumerable<GetMenuOutput> menus, bool includeButton = false)
    {
        string GetRedirect(IEnumerable<GetMenuOutput> menus, GetMenuOutput menu)
        {
            if (menu.Type != MenuType.Catalog)
            {
                return null;
            }

            return menus.Where(p => p.ParentId == menu.Id).OrderByDescending(p => p.Sort).FirstOrDefault()?.RoutePath;
        }

        IDictionary<string, object> SetMeta(GetMenuOutput menu)
        {
            var meta = new Dictionary<string, object>();
            meta["Title"] = menu.Name;
            meta["Icon"] = menu.Icon;
            meta["OrderNo"] = menu.Sort;
            meta["Icon"] = menu.Icon;
            if (menu.HideBreadcrumb == true)
            {
                meta["HideBreadcrumb"] = true;
            }

            if (menu.HideChildrenInMenu == true)
            {
                meta["HideChildrenInMenu"] = true;
            }

            if (!menu.CurrentActiveMenu.IsNullOrEmpty())
            {
                meta["CurrentActiveMenu"] = menu.CurrentActiveMenu;
            }

            if (menu.Display == false)
            {
                meta["ShowMenu"] = false;
                meta["HideMenu"] = true;
            }

            if (menu.KeepAlive == false)
            {
                meta["IgnoreKeepAlive"] = true;
            }

            if (menu.ExternalLink == true && menu.ExternalLinkType == ExternalLinkType.Inline)
            {
                meta["frameSrc"] = menu.RoutePath;
            }

            return meta;
        }

        string SetName(GetMenuOutput menu)
        {
            if (menu.Type == MenuType.Button) 
            {
                return menu.PermissionCode;
            }
            if (Regex.IsMatch(menu.Name, RegularExpressionConsts.Http))
            {
                var routePathName = Regex.Replace(menu.RoutePath, RegularExpressionConsts.Http, "");
                return routePathName.Replace("/", ".").TrimStart('.');
            }

            return menu.RoutePath.Replace("/", ".").TrimStart('.');
        }

        string SetPath(GetMenuOutput menu)
        {
            if (menu.ExternalLink == true && menu.ExternalLinkType == ExternalLinkType.Inline)
            {
                var routePathName = Regex.Replace(menu.RoutePath, RegularExpressionConsts.Http, "");
                return routePathName.Replace("/", ".").TrimStart('.');
            }

            return menu.RoutePath;
        }

        var frontendMenus = menus.Where(p => p.Status == Status.Valid)
            .Where(!includeButton, p=> p.Type != MenuType.Button)
            .Select(m => new FrontendMenu()
            {
                Id = m.Id,
                ParentId = m.ParentId,
                Name = SetName(m),
                Component = m.Component,
                Path = m.Type != MenuType.Button ? SetPath(m) : null,
                Redirect = m.Type != MenuType.Button ? GetRedirect(menus, m) : null,
                Meta = SetMeta(m),
            });
        return frontendMenus.ToList();
    }
}