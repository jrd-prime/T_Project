using System;
using VContainer;

namespace Code.Tools
{
    public static class ObjectResolverExtensions
    {
        public static T ResolveAndCheck<T>(IObjectResolver resolver) where T : class
        {
            var obj = resolver.Resolve<T>();
            if (obj == null) throw new NullReferenceException($"{typeof(T).Name} is null.");
            return obj;
        }
    }
}
