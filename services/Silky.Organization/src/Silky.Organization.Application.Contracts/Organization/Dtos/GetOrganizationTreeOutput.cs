using System.Collections.Generic;
using System.Linq;
using Silky.Hero.Common.Enums;

namespace Silky.Organization.Application.Contracts.Organization.Dtos;

public class GetOrganizationTreeOutput
{
    public long Id { get; set; }

    public long? ParentId { get; set; }

    public string Name { get; set; }
    
    public string Remark { get; set; }

    public Status Status { get; set; }

    public virtual ICollection<GetOrganizationTreeOutput> Children { get; set; }
}

public static class GetOrganizationTreeOutputExtension
{
    public static ICollection<GetOrganizationTreeOutput> BuildTree(this IEnumerable<GetOrganizationTreeOutput> treeData)
    {
        var treeResult = new List<GetOrganizationTreeOutput>();
        var topNodes = treeData.Where(p => p.ParentId == null);
        foreach (var topNode in topNodes)
        {
            topNode.Children = GetTreeChildren(topNode, treeData);
            treeResult.Add(topNode);
        }

        return treeResult;
    }

    private static ICollection<GetOrganizationTreeOutput> GetTreeChildren(GetOrganizationTreeOutput node,
        IEnumerable<GetOrganizationTreeOutput> treeData)
    {
        var children = treeData.Where(p => p.ParentId == node.Id);
        if (children.Any())
        {
            foreach (var child in children)
            {
                child.Children = GetTreeChildren(child, treeData);
            }
        }

        return children.ToList();
    }
}