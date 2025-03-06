using System;
using System.Collections.Generic;
using System.Reflection;
using Code.Core.Bootstrap;
using Code.Core.SO;
using UnityEngine;
using VContainer;

namespace Code.Core.Providers
{
    public interface ISettingsProvider : IBootable
    {
        public T GetSettings<T>() where T : SettingsBase;
    }

    public sealed class SettingsProvider : ISettingsProvider
    {
        public string Description => "Settings Provider";

        private MainSettings _mainSettings;
        private readonly Dictionary<Type, object> _cache = new();


        [Inject]
        private void Construct(MainSettings mainSettings) => _mainSettings = mainSettings;

        public void InitializationOnBoot()
        {
            if (!_mainSettings) throw new Exception("MainSettings is null");
            AddSettingsToCache();
        }

        private void AddSettingsToCache()
        {
            var fields = typeof(MainSettings)
                .GetFields(BindingFlags.Public | BindingFlags.Instance | BindingFlags.FlattenHierarchy);

            foreach (var field in fields)
            {
                if (!typeof(SettingsBase).IsAssignableFrom(field.FieldType)) continue;
                var settings = (SettingsBase)field.GetValue(_mainSettings);

                if (!_cache.TryAdd(settings.GetType(), settings))
                    throw new Exception($"Error. When adding to cache {settings.GetType()}");

                Debug.Log("Settings added to cache: " + settings.GetType().Name);
            }

            Debug.Log("Settings count: " + _cache.Count);
        }

        public T GetSettings<T>() where T : SettingsBase
        {
            if (!_cache.ContainsKey(typeof(T))) throw new Exception($"Settings {typeof(T).Name} not found");

            return _cache[typeof(T)] as T;
        }
    }
}
