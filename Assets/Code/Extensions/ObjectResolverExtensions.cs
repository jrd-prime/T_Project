using System;
using VContainer;

namespace Code.Extensions
{
    public static class ObjectResolverExtensions
    {
        public static T ResolveAndCheckOnNull<T>(this IObjectResolver resolver) where T : class
        {
            var result = resolver.Resolve(typeof(T));
            if (result == null) throw new NullReferenceException($"Cant resolve object of type {typeof(T)}.");

            return result as T;
        }
    }
}
