
using Silky.Identity.Domain.Identity;
using System.Collections.Generic;
using System.Linq;

namespace Silky.Identity.Domain.Extensions;

public static class FrontendMenuExtensions
{
    public static ICollection<FrontendMenu> BuildTree(this ICollection<FrontendMenu> frontendMenus) 
    {
        var treeResult = new List<FrontendMenu>();
        var topNodes = frontendMenus.Where(p => p.ParentId == null);
        foreach (var topNode in topNodes)
        {
            topNode.Children = GetTreeChildren(topNode, frontendMenus);
            treeResult.Add(topNode);
        }

        return treeResult;
    }

    private static ICollection<FrontendMenu> GetTreeChildren(FrontendMenu node,
        ICollection<FrontendMenu> treeData)
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