namespace MemoryCache.Base
{
    using System.Runtime.Caching;

    /// <summary>
    /// Interface to expose Cache Providers Methods
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface ICacheProviderBase<T>
    {
        /// <summary>
        /// Cache Name
        /// </summary>
        string Name { get; }
        /// <summary>
        /// Default Cache Policy
        /// </summary>
        CacheItemPolicy DefaultCacheItemPolicy { get; set; }

        /// <summary>
        /// Add an item to Cache
        /// </summary>
        /// <param name="key"></param>
        /// <param name="cacheItem"></param>
        /// <returns></returns>
        bool TryAdd(string key, T cacheItem);

        /// <summary>
        /// Add an item to Cache
        /// </summary>
        /// <param name="key"></param>
        /// <param name="cacheItem"></param>
        /// <param name="policy"></param>
        /// <returns></returns>
        bool TryAdd(string key, T cacheItem, CacheItemPolicy policy);
        /// <summary>
        /// Reterive item  from Cache
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        T TryGet(string key);

        /// <summary>
        /// Add or Update existing item in Cache
        /// </summary>
        /// <param name="cacheKey"></param>
        /// <param name="returnData"></param>
        /// <returns></returns>
        T TryAddOrUpdate(string cacheKey, T returnData);
        /// <summary>
        /// Add or Update existing item in Cache
        /// </summary>
        /// <param name="cacheKey"></param>
        /// <param name="returnData"></param>
        /// <param name="policy"></param>
        /// <returns></returns>
        T TryAddOrUpdate(string cacheKey, T returnData, CacheItemPolicy policy);
        /// <summary>
        /// Remoce an item from Cache
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        bool TryRemove(string key);
    }
}
