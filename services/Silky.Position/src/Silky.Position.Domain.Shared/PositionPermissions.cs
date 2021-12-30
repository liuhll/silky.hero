namespace Silky.Position.Domain.Shared;

public class PositionPermissions
{
    private const string GroupName = "Position";
    
    public static class Positions
    {
        public const string Default = GroupName + ".Default";
        public const string Create = GroupName + ".Create";
        public const string Update = GroupName + ".Update";
        public const string Delete = GroupName + ".Delete";
    }
}