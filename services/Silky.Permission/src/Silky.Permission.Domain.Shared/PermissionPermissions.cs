namespace Silky.Permission.Domain.Shared;

public class PermissionPermissions
{
    private const string GroupName = "Permission";

    public static class Menus
    {
        public const string Default = GroupName + ".Menu";
        public const string Create = Default + ".Create";
        public const string Update = Default + ".Update";
        public const string Delete = Default + ".Delete";
        public const string LookDetail = Default + ".LookDetail";
    }
}