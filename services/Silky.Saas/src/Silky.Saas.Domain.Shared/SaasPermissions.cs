namespace Silky.Saas.Domain.Shared;

public class SaasPermissions
{
    public static class Tenants
    {
        private const string GroupName = "Tenant";
        public const string Default = GroupName + ".Default";
        public const string Create = GroupName + ".Create";
        public const string Update = GroupName + ".Update";
        public const string Delete = GroupName + ".Delete";
        public const string Search = GroupName + ".Search";
        public const string LookDetail = GroupName + ".LookDetail";
    }
    
    public static class Editions
    {
        private const string GroupName = "Edition";
        
        public const string Default = GroupName + ".Default";
        public const string Create = GroupName + ".Create";
        public const string Update = GroupName + ".Update";
        public const string Delete = GroupName + ".Delete";
        public const string LookDetail = GroupName + ".LookDetail";
        public const string SetFeatures = GroupName + ".SetFeatures";
        public const string Search = GroupName + ".Search";
    }
}