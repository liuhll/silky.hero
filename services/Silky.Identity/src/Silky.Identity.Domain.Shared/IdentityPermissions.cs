namespace Silky.Identity.Domain.Shared;

public class IdentityPermissions
{
    private const string GroupName = "Identity";

    public static class Users
    {
        public const string Default = GroupName + ".User";
        public const string Create = Default + ".Create";
        public const string Update = Default + ".Update";
        public const string Delete = Default + ".Delete";
        public const string UpdateClaimTypes = Default + ".UpdateClaimTypes";
        public const string Lock = Default + ".Lock";
        public const string UnLock = Default + ".UnLock";
        public const string ChangePassword = Default + ".ChangePassword";
        public const string SetRoles = Default + ".SetRoles";
    }

    public static class Roles
    {
        public const string Default = GroupName + ".Role";
        public const string Create = Default + ".Create";
        public const string Update = Default + ".Update";
        public const string Delete = Default + ".Delete";
        public const string SetMenus = Default + ".SetMenus";
        public const string SetDataRange = Default + ".SetDataRange";
    }

    public static class ClaimTypes
    {
        public const string Default = GroupName + ".ClaimType";
        public const string Create = Default + ".Create";
        public const string Update = Default + ".Update";
        public const string Delete = Default + ".Delete";
    }
}