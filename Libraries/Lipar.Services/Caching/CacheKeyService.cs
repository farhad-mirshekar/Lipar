using Lipar.Core.Caching;
using Lipar.Core.Security;
using Lipar.Entities;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace Lipar.Services.Caching
{
    public class CacheKeyService : ICacheKeyService
    {
        #region Fields
        private const string HASH_ALGORITHM = "SHA1";

        #endregion


        #region Utilities

        /// <summary>
        /// Creates the hash sum of identifiers list
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        protected virtual string CreateIdsHash<T>(IEnumerable<T> ids)
        {
            var identifiers = ids.ToList();

            if (!identifiers.Any())
                return string.Empty;

            return HashHelper.CreateHash(Encoding.UTF8.GetBytes(string.Join(", ", identifiers.OrderBy(id => id))), HASH_ALGORITHM);
        }

        /// <summary>
        /// Converts an object to cache parameter
        /// </summary>
        /// <param name="parameter">Object to convert</param>
        /// <returns>Cache parameter</returns>
        protected virtual object CreateCacheKeyParameters<T>(object parameter)
        {
            return parameter switch
            {
                null => "null",
                IEnumerable<Guid> ids => CreateIdsHash(ids),
                IEnumerable<BaseEntity<T>> entities => CreateIdsHash(entities.Select(e => e.Id)),
                BaseEntity<T> entity => entity.Id,
                decimal param => param.ToString(CultureInfo.InvariantCulture),
                _ => parameter
            };
        }

        /// <summary>
        /// Creates a copy of cache key and fills it by set parameters
        /// </summary>
        /// <param name="cacheKey">Initial cache key</param>
        /// <param name="keyObjects">Parameters to create cache key</param>
        /// <returns>Cache key</returns>
        protected virtual CacheKey FillCacheKey<T>(CacheKey cacheKey, params object[] keyObjects)
        {
            return new CacheKey(cacheKey, CreateCacheKeyParameters<T>, keyObjects);
        }

        #endregion

        #region Methods

        /// <summary>
        /// Creates a copy of cache key and fills it by set parameters
        /// </summary>
        /// <param name="cacheKey">Initial cache key</param>
        /// <param name="keyObjects">Parameters to create cache key</param>
        /// <returns>Cache key</returns>
        public virtual CacheKey PrepareKey<T>(CacheKey cacheKey, params object[] keyObjects)
        {
            return FillCacheKey<T>(cacheKey, keyObjects);
        }

        /// <summary>
        /// Creates a copy of cache key using the default cache time and fills it by set parameters
        /// </summary>
        /// <param name="cacheKey">Initial cache key</param>
        /// <param name="keyObjects">Parameters to create cache key</param>
        /// <returns>Cache key</returns>
        public virtual CacheKey PrepareKeyForDefaultCache<T>(CacheKey cacheKey, params object[] keyObjects)
        {
            var key = FillCacheKey<T>(cacheKey, keyObjects);

            key.CacheTime = 30;

            return key;
        }

        /// <summary>
        /// Creates a copy of cache key using the short cache time and fills it by set parameters
        /// </summary>
        /// <param name="cacheKey">Initial cache key</param>
        /// <param name="keyObjects">Parameters to create cache key</param>
        /// <returns>Cache key</returns>
        public virtual CacheKey PrepareKeyForShortTermCache<T>(CacheKey cacheKey, params object[] keyObjects)
        {
            var key = FillCacheKey<T>(cacheKey, keyObjects);

            key.CacheTime = 30;

            return key;
        }

        /// <summary>
        /// Creates the cache key prefix
        /// </summary>
        /// <param name="keyFormatter">Key prefix formatter string</param>
        /// <param name="keyObjects">Parameters to create cache key prefix</param>
        /// <returns>Cache key prefix</returns>
        public virtual string PrepareKeyPrefix<T>(string keyFormatter, params object[] keyObjects)
        {
            return keyObjects?.Any() ?? false
                ? string.Format(keyFormatter, keyObjects.Select(CreateCacheKeyParameters<T>).ToArray())
                : keyFormatter;
        }

        #endregion
    }
}
