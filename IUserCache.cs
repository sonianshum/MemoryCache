namespace MemoryCache
{
    using System.Runtime.Caching;

    /// <summary>
    /// Interface to Expose Wrapper methods for Memory cache
    /// </summary>
    interface IUserCache
    {
        /// <summary>
        /// Add User to Cache
        /// </summary>
        /// <param name="key"></param>
        /// <param name="item"></param>
        /// <param name="policy"></param>
        /// <returns></returns>
        bool AddUserToCache(string key, object item, CacheItemPolicy policy);
        /// <summary>
        /// Add or update existinf User
        /// </summary>
        /// <param name="key"></param>
        /// <param name="item"></param>
        /// <param name="policy"></param>
        /// <returns></returns>
        object AddOrUpdateUserInCache(string key, object item, CacheItemPolicy policy);
        /// <summary>
        /// Delete User from Cache
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        bool DeleteUserFromCache(string key);
        /// <summary>
        /// Get USer from Cache
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        object GetUserFromCache(string key);
    }
}
