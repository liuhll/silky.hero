using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using Silky.Core;

namespace Silky.Permission.Domain.Menu;

public static class MenuExtensions
{
    
    public static ICollection<Menu> GetChildrenMenus(this ICollection<Menu> menus,
        long menuId, bool includeSelf = true, bool isAll = true)
    {
        Check.NotNull(menus, nameof(menus));
        var childrenMenus = new List<Menu>();
        if (includeSelf)
        {
            var self = menus.First(p => p.Id == menuId);
            childrenMenus.Add(self);
        }

        var children = menus.Where(p => p.ParentId == menuId);
        childrenMenus.AddRange(children);
        if (isAll && children?.Any() == true)
        {
            foreach (var child in children)
            {
                var nextChildrenOrganizations = GetChildrenMenus(menus, child.Id, false, isAll);
                childrenMenus.AddRange(nextChildrenOrganizations);
            }
        }
        return childrenMenus;
    }
    
    public static ICollection<Menu> BuildTree([NotNull]this ICollection<Menu> treeData)
    {
        Check.NotNull(treeData, nameof(treeData));
        var treeResult = new List<Menu>();
        var topNodes = treeData.Where(p => p.ParentId == null);
        foreach (var topNode in topNodes)
        {
            topNode.Children = GetTreeChildren(topNode, treeData);
            treeResult.Add(topNode);
        }

        return treeResult;
    }

    private static ICollection<Menu> GetTreeChildren(Menu node,
        ICollection<Menu> treeData)
    {
        var children = treeData?.Where(p => p.ParentId == node.Id);
        if (children?.Any() == true)
        {
            foreach (var child in children)
            {
                child.Children = GetTreeChildren(child, treeData);
            }
            return children.ToList();
        }

        return null;
    }
}