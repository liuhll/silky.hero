using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using Silky.Core;

namespace Silky.Permission.Domain.Menu;

public static class MenuExtensions
{
    public static ICollection<Menu> BuildTree([NotNull]this IEnumerable<Menu> treeData)
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
        IEnumerable<Menu> treeData)
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