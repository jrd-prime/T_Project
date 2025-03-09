using System;
using Code.Core.Input;
using Code.Core.Providers;
using Code.Core.SO;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Profiling;
using VContainer;
using VContainer.Unity;

namespace Code.Core.Context
{
    [UsedImplicitly]
    public class RootContext : LifetimeScope
    {
        [SerializeField] private MainSettings mainSettings;
        [SerializeField] private EventSystem eventSystem;

        protected override void Configure(IContainerBuilder builder)
        {
            Debug.Log("<color=cyan>Root context</color>");

            if (!mainSettings) throw new NullReferenceException("MainSettings is null.");
            if (!eventSystem) throw new NullReferenceException("EventSystem is null.");

            builder.RegisterComponent(mainSettings).AsSelf();
            builder.RegisterComponent(eventSystem).AsSelf();

            var input = gameObject.AddComponent<DesktopInput>();
            builder.RegisterComponent(input).AsSelf();

            builder.Register<InputHandler>(Lifetime.Singleton).AsImplementedInterfaces();

            builder.Register<ISettingsProvider, SettingsProvider>(Lifetime.Singleton);
            builder.Register<IAssetProvider, AssetProvider>(Lifetime.Singleton);
            builder.Register<FirstSceneProvider>(Lifetime.Singleton).AsSelf();
        }

        private void OnApplicationQuit()
        {
            var rendTex = (RenderTexture[])Resources.FindObjectsOfTypeAll(typeof(RenderTexture));

            var rendTexCount = rendTex.Length;
            var i = 0;
            foreach (var t in rendTex)
                if (t.name.StartsWith("Device Simulator"))
                {
                    Destroy(t);
                    i++;
                }

            Debug.Log("<color=darkblue><b>===</b></color>");

            if (i > 0) Debug.Log($"Render Textures: {rendTexCount} / Destroyed: {i}");

            Debug.Log(
                $"Allocated: {Profiler.GetTotalAllocatedMemoryLong() / (1024 * 1024)} MB / " +
                $"Reserved: {Profiler.GetTotalReservedMemoryLong() / (1024 * 1024)} MB / " +
                $"Unused Reserved: {Profiler.GetTotalUnusedReservedMemoryLong() / (1024 * 1024)} MB");

            Debug.Log("<color=darkblue><b>===</b></color>");
        }
    }
}
