using System;
using Code.Core.Data;
using Code.Core.FSM;
using Code.Core.Managers.UI;

namespace Code.Core.UI
{
    public interface ISubState
    {
        public void Enter();
        public void Exit();
    }

    public abstract class UISubStateBase : ISubState
    {
        protected IUIManager UIManager;

        protected readonly GameStateType BaseStateType;
        protected Enum CurrentSubState { get; set; }
        protected Enum DefaultSubState { get; }

        protected UISubStateBase(IUIManager uiManager, GameStateType baseStateType, Enum defaultStateType)
        {
            UIManager = uiManager ?? throw new ArgumentNullException(nameof(uiManager));
            DefaultSubState = defaultStateType ?? throw new ArgumentNullException(nameof(defaultStateType));
            CurrentSubState = DefaultSubState;
            BaseStateType = baseStateType;
        }

        protected void ShowView(EShowLogic showLogic = EShowLogic.Default)
        {
            UIManager.ShowView(BaseStateType, DefaultSubState, showLogic);
        }

        protected void HideView(EShowLogic showLogic = EShowLogic.Default)
        {
            UIManager.HideView(BaseStateType, DefaultSubState, showLogic);
        }

        public abstract void Enter();
        public abstract void Exit();
    }
}
