namespace Silky.Identity.Domain;

public class HeroIdentityDbProperties
{
    
    public static string DbTablePrefix { get; set; } = "";

    public static string DbSchema { get; set; } = null;

    public const string ConnectionStringName = "default";
}