namespace Silky.Log.Domain.Shared;

public class LogPermissions
{
    private const string GroupName = "Log";
    
    public static class AuditLogging
    {
        public const string Default = GroupName + ".AuditLogging";

        public const string Search = Default + ".Search";
        
        public const string LookDetail = Default + ".LookDetail";
    }
}