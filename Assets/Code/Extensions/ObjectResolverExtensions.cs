using System;
using Code.Core.FSM;
using VContainer;

namespace Code.Extensions
{
    public static class ObjectResolverExtensions
    {
        public static T ResolveAndCheckOnNull<T>(this IObjectResolver resolver) where T : class
        {
            var result = resolver.Resolve(typeof(T));
            if (result == null) throw new NullReferenceException($"Object of type {typeof(T)} is null.");

            return result as T;
        }
    }
}
