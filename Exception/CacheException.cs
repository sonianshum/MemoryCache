namespace MemoryCache.Exception
{
    #region using statements

    using System;
    using System.Runtime.Serialization;

    #endregion

    /// <summary>
    /// An exception which occurs on Radius protocol errors like
    /// invalid packets or malformed attributes.
    /// </summary>
    [Serializable]
    public class CacheException : Exception
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        public CacheException()
        {
            // Add any type-specific logic, and supply the default message.
        }

        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="message"></param>
        /// <param name="innerException"></param>
        public CacheException(string message, Exception innerException)
            : base(message, innerException)
        {
            // Add any type-specific logic for inner exceptions.
        }

        /// <summary>
        /// Constructs a RadiusException with a message.
        /// </summary>
        /// <param name="message">message error message</param>
        public CacheException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// type-specific serialization constructor logic
        /// </summary>
        /// <param name="info"></param>
        /// <param name="context"></param>
        protected CacheException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}