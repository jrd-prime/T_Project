using System;
using Core.Providers;
using Core.Providers.Localization;
using Db.SO;
using Infrastructure.Input;
using Infrastructure.Input.Interfaces;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Profiling;
using Zenject;

namespace Infrastructure.Installers
{
    public class ProjectInstaller : MonoInstaller
    {
        [SerializeField] private MainSettings mainSettings;
        [SerializeField] private EventSystem eventSystem;

        public override void InstallBindings()
        {
            Debug.Log("<color=cyan>ProjectInstaller</color>");
            SignalBusInstaller.Install(Container);

            if (mainSettings == null) throw new NullReferenceException("MainSettings is null.");
            Container.Bind<MainSettings>().FromInstance(mainSettings).AsSingle().NonLazy();

            if (eventSystem == null) throw new NullReferenceException("EventSystem is null.");
            Container.Bind<EventSystem>().FromInstance(eventSystem).AsSingle();

            BindInput();


            Container.Bind<ISettingsProvider>().To<SettingsProvider>().AsSingle();
            Container.Bind<IAssetProvider>().To<AssetProvider>().AsSingle();
            Container.Bind<ILocalizationProvider>().To<LocalizationProvider>().AsSingle();
            Container.Bind<FirstSceneProvider>().AsSingle();
        }

        private void BindInput()
        {
            Container.Bind<IJInput>().To<DesktopInput>().FromNewComponentOnNewGameObject().AsSingle().NonLazy();
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
