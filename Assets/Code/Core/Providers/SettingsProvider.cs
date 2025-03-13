using System;
using System.Collections.Generic;
using System.Reflection;
using Code.Core.Bootstrap;
using Code.Core.SO;
using Cysharp.Threading.Tasks;
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

        public async UniTask InitializationOnBoot()
        {
            if (!_mainSettings) throw new Exception("MainSettings is null");
            await AddSettingsToCacheAsync();
        }

        private async UniTask AddSettingsToCacheAsync()
        {
            var fields =
                typeof(MainSettings).GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);

            foreach (var field in fields)
            {
                if (!typeof(SettingsBase).IsAssignableFrom(field.FieldType)) continue;

                var settings = field.GetValue(_mainSettings) as SettingsBase;

                if (settings == null)
                {
                    Debug.LogWarning($"Field {field.Name} in MainSettings is null, skipping.");
                    continue;
                }

                if (!_cache.TryAdd(settings.GetType(), settings))
                    throw new Exception($"Duplicate settings type {settings.GetType()} found in cache.");
            }

            Debug.Log($"Settings added to cache: {_cache.Count}");
            await UniTask.CompletedTask;
        }

        public T GetSettings<T>() where T : SettingsBase
        {
            if (!_cache.TryGetValue(typeof(T), out var settings))
                throw new Exception($"Settings {typeof(T).Name} not found in cache.");

            return (T)settings;
        }
    }
}
