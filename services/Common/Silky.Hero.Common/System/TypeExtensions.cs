using JetBrains.Annotations;
using Silky.Core;

namespace System;

public static class TypeExtensions
{
    public static bool IsAssignableTo<TTarget>([NotNull] this Type type)
    {
        Check.NotNull(type, nameof(type));

        return type.IsAssignableTo(typeof(TTarget));
    }
}