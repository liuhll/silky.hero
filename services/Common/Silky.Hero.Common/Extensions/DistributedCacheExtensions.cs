using JetBrains.Annotations;
using Microsoft.Extensions.Caching.Distributed;
using Silky.Caching;
using Silky.Core;

namespace Silky.Hero.Common.Extensions;

public static class DistributedCacheExtensions
{
    public static Task RemoveAsync([NotNull] this IDistributedCache distributedCache, [NotNull] string cacheName,
        [NotNull] string key)
    {
        return distributedCache.RemoveAsync(NormalizeKey(key, cacheName));
    }

    public static Task RemoveAsync([NotNull] this IDistributedCache distributedCache, [NotNull] Type cacheType,
        [NotNull] string key)
    {
        return distributedCache.RemoveAsync(NormalizeKey(key, cacheType.FullName));
    }

    private static string NormalizeKey(string key, string cacheName, bool ignoreMultiTenancy = false)
    {
        var keyNormalizer = EngineContext.Current.Resolve<IDistributedCacheKeyNormalizer>();
        return keyNormalizer.NormalizeKey(
            new DistributedCacheKeyNormalizeArgs(
                key,
                cacheName,
                ignoreMultiTenancy
            )
        );
    }
}