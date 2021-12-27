namespace Silky.Permission.Domain;

public class PermissionDbProperties
{
    public static string DbTablePrefix { get; set; } = "";

    public static string DbSchema { get; set; } = null;
    
    public const string ConnectionStringName = "default";
}