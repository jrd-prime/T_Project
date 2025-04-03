using System;
using Core.Managers.HSM.Common;
using Core.Managers.HSM.Impls.States.Gameplay;
using Core.Managers.HSM.Interfaces;
using Core.Managers.UI.Impls;
using Core.Managers.UI.Interfaces;
using Game.UI.Common;
using Game.UI.Data;
using ModestTree;

namespace Core.Managers.HSM.Impls.States.Menu
{
    public sealed class MenuState : BaseState
    {
        public MenuState(HSM stateMachine, IUIManager uiManager) : base(stateMachine, uiManager)
        {
        }

        public override void Enter(IState previousState)
        {
            // Если приходим иг геймплея, то попадаем в паузу
            if (previousState is GameplayState)
            {
                UIManager.ShowViewNew(ViewRegistryType.Menu, ViewIDConst.Pause, UIViewer.Layer.Default);
                return;
            }

            UIManager.ShowViewNew(ViewRegistryType.Menu, ViewIDConst.Main, UIViewer.Layer.Default);
        }

        public override void Exit(IState previousState)
        {
            // Если пришли из геймплея в меню, то по возврату в геймплей
            if (previousState is GameplayState)
            {
                Log.Info("previous state is gameplay");
            }

            Log.Info("prevous state = " + previousState);
        }

        public override IState HandleTransition()
        {
            return null;
        }
    }
}
