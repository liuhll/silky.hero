namespace Silky.Tenant.Domain.Shared;

public class TenantPermissions
{
    private const string GroupName = "Tenant";

    public static class Tenants
    {
        public const string Default = GroupName + ".Default";
        public const string Create = GroupName + ".Create";
        public const string Update = GroupName + ".Update";
        public const string Delete = GroupName + ".Delete";
        public const string LookDetail = GroupName + ".LookDetail";
    }
}