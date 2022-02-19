using Silky.Core;
using Silky.Core.Serialization;

namespace Silky.Hero.Common.Extensions;

public static class JsonExtensions
{
    private readonly static ISerializer _serializer;
        
    static JsonExtensions()
    {
        _serializer = EngineContext.Current.Resolve<ISerializer>();
    }

    public static string ToJsonString(this object obj, bool camelCase = false, bool indented = false)
    {
        return _serializer.Serialize(obj,camelCase,indented);
    }

    public static T FromJsonString<T>(this string value)
    {
        return _serializer.Deserialize<T>(value);
    }
}