using System;
using Code.Core.Managers.UI;
using Code.Core.UI.Views.Menu;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Code.Core.Context
{
    public class UIContext : LifetimeScope
    {
        [SerializeField] private UIManager uiManager;

        protected override void Configure(IContainerBuilder builder)
        {
            Debug.Log("<color=cyan>UI context</color>");


            if (!uiManager) throw new NullReferenceException("UIManager is null. " + name);
            builder.RegisterComponent(uiManager).As<IUIManager, IInitializable>();

            // Menu
            builder.Register<MenuUIModel>(Lifetime.Singleton).AsSelf().AsImplementedInterfaces();
            builder.Register<MenuUIViewModel>(Lifetime.Singleton).AsSelf().AsImplementedInterfaces();
        }
    }
}
