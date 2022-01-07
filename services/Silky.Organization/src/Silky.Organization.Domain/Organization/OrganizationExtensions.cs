using System.Collections.Generic;
using System.Linq;
using Silky.Core;

namespace Silky.Organization.Domain;

public static class OrganizationExtensions
{
    public static IEnumerable<Organization> GetChildrenOrganizations(this IEnumerable<Organization> organizations,
        long organizationId, bool includeSelf = true, bool isAll = true)
    {
        Check.NotNull(organizations, nameof(organizations));
        var childrenOrganizations = new List<Organization>();
        if (includeSelf)
        {
            var self = organizations.First(p => p.Id == organizationId);
            childrenOrganizations.Add(self);
        }

        var children = organizations.Where(p => p.ParentId == organizationId);
        childrenOrganizations.AddRange(children);
        if (isAll && children?.Any() == true)
        {
            foreach (var child in children)
            {
                var nextChildrenOrganizations = GetChildrenOrganizations(organizations, child.Id, false, isAll);
                childrenOrganizations.AddRange(nextChildrenOrganizations);
            }
        }
        return childrenOrganizations;
    }
}