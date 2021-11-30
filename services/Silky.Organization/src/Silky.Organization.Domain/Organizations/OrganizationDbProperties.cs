namespace Silky.Organization.Domain.Organizations;

public class OrganizationDbProperties
{
    public static string DbTablePrefix { get; set; } = "";

    public static string DbSchema { get; set; } = null;

    public const string ConnectionStringName = "default";
}