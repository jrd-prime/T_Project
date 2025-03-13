using System;
using Code.Core.WORK.Enums;
using Code.Core.WORK.Enums.States;
using Code.Core.WORK.UIManager;

namespace Code.Core.WORK.GameStates
{
    public interface ISubState
    {
        public void Enter();
        public void Exit();
    }

    public abstract class SubStateBase : ISubState
    {
        protected IUIManager UIManager;

        protected readonly EGameState BaseState;
        protected Enum CurrentSubState { get; set; }
        protected Enum DefaultSubState { get; }

        protected SubStateBase(IUIManager uiManager, EGameState baseState, Enum defaultSubState)
        {
            UIManager = uiManager ?? throw new ArgumentNullException(nameof(uiManager));
            DefaultSubState = defaultSubState ?? throw new ArgumentNullException(nameof(defaultSubState));
            CurrentSubState = DefaultSubState;
            BaseState = baseState;
        }

        protected void ShowView(EShowLogic showLogic = EShowLogic.Default)
        {
            UIManager.ShowView(BaseState, DefaultSubState, showLogic);
        }

        protected void HideView(EShowLogic showLogic = EShowLogic.Default)
        {
            UIManager.HideView(BaseState, DefaultSubState, showLogic);
        }

        public abstract void Enter();
        public abstract void Exit();
    }
}
