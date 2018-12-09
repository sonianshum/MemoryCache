namespace MemoryCache
{
    using System;
    using System.Runtime.Caching;
    using System.Collections.Generic;
    using MemoryCache;
    using MemoryCache.Base;
    using MemoryCache.Exception;

    /// <summary>
    /// Wrapper class for CacheProviderBase
    /// </summary>
    public class UserCache : IUserCache
    {
        private static readonly CacheItemPolicy CacheItemPolicy = new CacheItemPolicy { AbsoluteExpiration = DateTimeOffset.Now.AddMinutes(5) };
        private static readonly ICacheProviderBase<object> CacheProvider;

        /// <summary>
        /// Cache initializer
        /// </summary>

        static UserCache() => CacheProvider = new CacheProviderBase<object>("UserCache");

        /// <summary>
        /// Add or Update an existing User in Cache
        /// </summary>
        /// <param name="key"></param>
        /// <param name="item"></param>
        /// <returns></returns>
        public object AddOrUpdateUserInCache(string key, object item)
        {
            try
            {
                return CacheProvider.TryAddOrUpdate(key, item);
            }
            catch (ArgumentNullException exception)
            {
                throw new CacheException(exception.Message);
            }
            catch (ArgumentOutOfRangeException exception)
            {
                throw new CacheException(exception.Message);
            }
            catch (ArgumentException exception)
            {
                throw new CacheException(exception.Message);
            }
        }
        /// <summary>
        /// Add a user to cache
        /// </summary>
        /// <param name="key"></param>
        /// <param name="item"></param>
        /// <returns></returns>
        public bool AddUserToCache(string key, object item)
        {
            try
            {
                if (item != null)
                {
                    return CacheProvider.TryAdd(key, cacheItem: item);
                }
            }
            catch (ArgumentNullException exception)
            {
                throw new CacheException(exception.Message);
            }
            catch (ArgumentOutOfRangeException exception)
            {
                throw new CacheException(exception.Message);
            }
            catch (ArgumentException exception)
            {
                throw new CacheException(exception.Message);
            }

            return false;
        }

        /// <summary>
        /// Delete a User from cache
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool DeleteUserFromCache(string key)
        {
            try
            {
                return CacheProvider.TryRemove(key);

            }
            catch (NotSupportedException exception)
            {
                throw new CacheException(exception.Message);
            }
            catch (ArgumentNullException exception)
            {
                throw new CacheException(exception.Message);
            }
        }

        /// <summary>
        /// Get a user from Cache by CacheKey
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public object GetUserFromCache(string key)
        {
            try
            {
                return CacheProvider.TryGet(key);
            }
            catch (NotSupportedException exception)
            {
                throw new CacheException(exception.Message);
            }
            catch (ArgumentNullException exception)
            {
                throw new CacheException(exception.Message);
            }
        }

        public object AddOrUpdateUserInCache(string key, object item, CacheItemPolicy policy)
        {
            throw new NotImplementedException();
        }

        public bool AddUserToCache(string key, object item, CacheItemPolicy policy)
        {
            throw new NotImplementedException();
        }
    }
}

