using System;
using System.Collections.Generic;
using R3;

namespace Code.Core
{
    public sealed class RareSignalBus
    {
        private readonly Dictionary<Type, object> _subjects = new Dictionary<Type, object>();

        private ISubject<T> GetSubject<T>()
        {
            Type signalType = typeof(T);
            if (!_subjects.ContainsKey(signalType)) _subjects[signalType] = new Subject<T>();

            return (ISubject<T>)_subjects[signalType];
        }

        public void Fire<T>(T signal)
        {
            GetSubject<T>().OnNext(signal);
        }

        public IDisposable Subscribe<T>(Observer<T> handler)
        {
            return GetSubject<T>().Subscribe(handler);
        }

        public void Clear()
        {
            foreach (var subject in _subjects.Values)
            {
                if (subject is IDisposable disposable)
                {
                    disposable.Dispose();
                }
            }

            _subjects.Clear();
        }
    }
}
