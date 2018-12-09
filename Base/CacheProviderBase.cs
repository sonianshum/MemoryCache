namespace MemoryCache.Base
{   
    using System;
    using System.Runtime.Caching;

    /// <summary>
    ///  Base class to implement memory cache handling.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public sealed class CacheProviderBase<T> : IDisposable, ICacheProviderBase<T> where T : class
    {

        private CacheItemPolicy _defaultCacheItemPolicy;
        private readonly object _lockObject = new object();
        private readonly MemoryCache _memoryCache;

        /// <summary>
        /// instantiate the cache provider with CacheName and Time Window (These should be overridden by individual Cache Policies)
        /// </summary>
        /// <param name="name"></param>
        public CacheProviderBase(string name) : this(name, null)
        {
        }

        /// <summary>
        /// instantiate the cache provider with CacheName and Time Window (These should be overridden by individual Cache Policies)
        /// </summary>
        /// <param name="name"></param>
        /// <param name="policy"></param>
        public CacheProviderBase(string name, CacheItemPolicy policy)
        {
            //initialize the memory Cache with Name
            _memoryCache = new MemoryCache(name);

            if (policy != null)
                _defaultCacheItemPolicy = policy;

        }
        /// <summary>
        ///  Default Time Window to keep the cache item
        /// </summary>
        public CacheItemPolicy DefaultCacheItemPolicy
        {
            get
            {
                _defaultCacheItemPolicy = new CacheItemPolicy { AbsoluteExpiration = DateTimeOffset.Now.AddMinutes(10) };
                _defaultCacheItemPolicy.RemovedCallback += OnCacheRemoved;
                return _defaultCacheItemPolicy;

            }
            set { _defaultCacheItemPolicy = value; }
        }


        private void OnCacheRemoved(CacheEntryRemovedArguments arguments)
        {
            //Logger.Debug(FormattableString.Invariant($"Session expired. Removing the user {arguments.CacheItem.Key} with reason {arguments.RemovedReason.ToString()}"));            
        }

        public string Name => _memoryCache.Name;

        public void Dispose()
        {
            this._memoryCache.Dispose();
        }

        /// <summary>
        /// Function to set an item in cache
        /// </summary>
        /// <param name="key"></param>
        /// <param name="cacheItem"></param>
        public bool TryAdd(string key, T cacheItem)
        {
            return this.TryAdd(key, cacheItem, null);
        }
        /// <summary>
        /// Function to set an item in cache
        /// </summary>
        /// <param name="key"></param>
        /// <param name="cacheItem"></param>
        /// <param name="policy"></param>
        public bool TryAdd(string key, T cacheItem, CacheItemPolicy policy)
        {
            var cachePolicy = policy ?? DefaultCacheItemPolicy;
            if (cacheItem != null)
            {
                lock (_lockObject)
                {
                    return _memoryCache.Add(key, cacheItem, cachePolicy);
                }
            }
            return false;
        }

        /// <summary>
        /// Try getting existing entry otherwise set new
        /// </summary>
        /// <param name="cacheKey"></param>
        /// <param name="returnData"></param>
        /// <returns></returns>
        public T TryAddOrUpdate(string cacheKey, T returnData)
        {
            return TryAddOrUpdate(cacheKey, returnData, null);
        }

        /// <summary>
        /// Try getting existing entry otherwise set new
        /// </summary>
        /// <param name="cacheKey"></param>
        /// <param name="returnData"></param>
        /// <param name="policy"></param>
        /// <returns></returns>
        public T TryAddOrUpdate(string cacheKey, T returnData, CacheItemPolicy policy)
        {
            if (TryGet(cacheKey) == null)
            {
                lock (_lockObject)
                {
                    _memoryCache.Add(cacheKey, returnData, policy);
                }
            }
            else
            {
                lock (_lockObject)
                {
                    _memoryCache.Set(cacheKey, returnData, policy);
                }
            }
            return returnData;
        }

        /// <summary>
        /// Function to get the CacheItem.
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public T TryGet(string key)
        {
            object cache = null;
            lock (_lockObject)
            {
                if (_memoryCache.Contains(key))
                {
                    cache = _memoryCache.Get(key);
                }
            }

            return (T)cache;
        }

        /// <summary>
        /// Try to remove an item 
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool TryRemove(string key)
        {
            var returnItem = TryGet(key);
            if (returnItem != null)
            {
                lock (_lockObject)
                {
                    _memoryCache.Remove(key);
                }
            }
            return returnItem != null;
        }
    }
}
