﻿using Lipar.Core.Caching;

namespace Lipar.Services.Caching
{
    public partial interface ICacheKeyService
    {
        /// <summary>
        /// Creates a copy of cache key and fills it by set parameters
        /// </summary>
        /// <param name="cacheKey">Initial cache key</param>
        /// <param name="keyObjects">Parameters to create cache key</param>
        /// <returns>Cache key</returns>
        CacheKey PrepareKey<T>(CacheKey cacheKey, params object[] keyObjects);

        /// <summary>
        /// Creates a copy of cache key using the default cache time and fills it by set parameters
        /// </summary>
        /// <param name="cacheKey">Initial cache key</param>
        /// <param name="keyObjects">Parameters to create cache key</param>
        /// <returns>Cache key</returns>
        CacheKey PrepareKeyForDefaultCache<T>(CacheKey cacheKey, params object[] keyObjects);

        /// <summary>
        /// Creates a copy of cache key using the short cache time and fills it by set parameters
        /// </summary>
        /// <param name="cacheKey">Initial cache key</param>
        /// <param name="keyObjects">Parameters to create cache key</param>
        /// <returns>Cache key</returns>
        CacheKey PrepareKeyForShortTermCache<T>(CacheKey cacheKey, params object[] keyObjects);

        /// <summary>
        /// Creates the cache key prefix
        /// </summary>
        /// <param name="keyFormatter">Key prefix formatter string</param>
        /// <param name="keyObjects">Parameters to create cache key prefix</param>
        /// <returns>Cache key prefix</returns>
        string PrepareKeyPrefix<T>(string keyFormatter, params object[] keyObjects);
    }
}
