using System;
using Zenject;

namespace Code.Extensions
{
    public static class ObjectResolverExtensions
    {
        public static T ResolveAndCheckOnNull<T>(this DiContainer container) where T : class
        {
            var result = container.Resolve(typeof(T));
            if (result == null) throw new NullReferenceException($"Cant resolve object of type {typeof(T)}.");

            return result as T;
        }
    }
}
