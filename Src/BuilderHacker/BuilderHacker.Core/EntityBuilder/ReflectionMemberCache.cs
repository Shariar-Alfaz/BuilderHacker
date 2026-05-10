using System;
using System.Collections.Generic;
using System.Reflection;

namespace BuilderHacker.Core.EntityBuilder
{
    /// <summary>
    /// Thread-safe cache for reflection member lookups to improve performance of EntityBuilder.
    /// Caches PropertyInfo and FieldInfo objects to avoid expensive reflection calls on repeated property/field access.
    /// </summary>
    internal static class ReflectionMemberCache
    {
        private static readonly Dictionary<(Type, string), MemberInfo> _cache = 
            new Dictionary<(Type, string), MemberInfo>();
        private static readonly object _lock = new object();

        /// <summary>
        /// Attempts to retrieve a cached property or field member info for the given type and name.
        /// </summary>
        /// <param name="type">The type to search for members.</param>
        /// <param name="name">The name of the property or field (case-insensitive).</param>
        /// <param name="member">The cached PropertyInfo or FieldInfo, or null if not found.</param>
        /// <returns>True if a member was found in cache or via reflection lookup; false otherwise.</returns>
        public static bool TryGetMember(Type type, string name, out MemberInfo member)
        {
            if (type == null)
                throw new ArgumentNullException(nameof(type));

            if (string.IsNullOrEmpty(name))
            {
                member = null;
                return false;
            }

            var key = (type, name.ToLowerInvariant());

            lock (_lock)
            {
                if (_cache.TryGetValue(key, out member))
                    return true;
            }

            // Perform property lookup
            var prop = type.GetProperty(name,
                BindingFlags.Public |
                BindingFlags.NonPublic |
                BindingFlags.Instance |
                BindingFlags.IgnoreCase);

            if (prop != null)
            {
                lock (_lock)
                {
                    // Double-check after lock acquisition
                    if (!_cache.ContainsKey(key))
                        _cache[key] = prop;
                    else
                        member = _cache[key];
                }
                member = prop;
                return true;
            }

            // Perform field lookup
            var field = type.GetField(name,
                BindingFlags.Public |
                BindingFlags.NonPublic |
                BindingFlags.Instance |
                BindingFlags.IgnoreCase);

            if (field != null)
            {
                lock (_lock)
                {
                    // Double-check after lock acquisition
                    if (!_cache.ContainsKey(key))
                        _cache[key] = field;
                    else
                        member = _cache[key];
                }
                member = field;
                return true;
            }

            member = null;
            return false;
        }

        /// <summary>
        /// Clears the reflection member cache.
        /// Use this if types are being dynamically unloaded or for testing purposes.
        /// </summary>
        public static void Clear()
        {
            lock (_lock)
            {
                _cache.Clear();
            }
        }

        /// <summary>
        /// Gets the current number of cached members.
        /// </summary>
        /// <returns>The count of cached PropertyInfo and FieldInfo objects.</returns>
        public static int GetCacheSize()
        {
            lock (_lock)
            {
                return _cache.Count;
            }
        }
    }
}
