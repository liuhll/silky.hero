namespace Silky.Position.Domain.Positions;

public class PositionDbProperties
{
    public static string DbTablePrefix { get; set; } = "";

    public static string DbSchema { get; set; } = null;

    public const string ConnectionStringName = "default";
}