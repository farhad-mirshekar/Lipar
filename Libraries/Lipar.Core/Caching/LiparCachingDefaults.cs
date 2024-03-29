﻿namespace Lipar.Core.Caching
{
    /// <summary>
    /// Represents default values related to caching
    /// </summary>
    public static partial class LiparCachingDefaults
    {
        /// <summary>
        /// Gets the default cache time in minutes
        /// </summary>
        public static int CacheTime => 60;

        /// <summary>
        /// Gets a key for caching
        /// </summary>
        /// <remarks>
        /// {0} : Entity type name
        /// {1} : Entity id
        /// </remarks>
        public static string LiparEntityCacheKey => "Lipar.{0}.id-{1}";

        public static string LiparUser => "LiparUser";
        public static string LiparPosition => "LiparPosition";
        public static string LiparCommand => "LiparCommand";
        public static string LiparShoppingCartItem => "LiparShoppingCartItem";
    }
}