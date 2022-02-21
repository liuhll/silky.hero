using Silky.Caching;
using Silky.Caching.Configuration;
using Silky.Core;

namespace Silky.Hero.Common.Extensions;

public static class DistributedCacheKeyNormalizerExtensions
{
    public static string NormalizeTenantKey(this IDistributedCacheKeyNormalizer normalizer,
        DistributedCacheKeyNormalizeArgs args, long tenantId)
    {
        
        var silkyDistributedCacheOptions = EngineContext.Current.GetOptions<SilkyDistributedCacheOptions>();
        var normalizedKey = $"c:{args.CacheName},k:{silkyDistributedCacheOptions.KeyPrefix}{args.Key}";
        if (!args.IgnoreMultiTenancy)
        {
            normalizedKey = $"t:{tenantId},{normalizedKey}";
        }

        return normalizedKey;
    }
    
    public static string NormalizeTenantKey(this IDistributedCacheKeyNormalizer normalizer, string key, string cacheName, long tenantId)
    {
        return normalizer.NormalizeTenantKey(
            new DistributedCacheKeyNormalizeArgs(
                key,
                cacheName,
                false
            ),
            tenantId
        );
    }
}