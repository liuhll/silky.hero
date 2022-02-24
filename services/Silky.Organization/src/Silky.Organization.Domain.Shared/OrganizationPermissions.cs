namespace Silky.Organization.Domain.Shared;

public class OrganizationPermissions
{
    private const string GroupName = "Organization";
    
    public static class Organizations
    {
        public const string Default = GroupName + ".Default";
        public const string Create = GroupName + ".Create";
        public const string Update = GroupName + ".Update";
        public const string Delete = GroupName + ".Delete";
        public const string AddUsers = GroupName + ".AddUsers";
        public const string RemoveUser = GroupName + ".RemoveUser";
        public const string LookDetail = GroupName + ".LookDetail";
        public const string AllocationRole = GroupName + ".AllocationRole";
    }
}