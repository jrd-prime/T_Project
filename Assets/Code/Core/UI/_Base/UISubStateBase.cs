using System;
using Code.Core.Data;
using Code.Core.FSM;
using Code.Core.Managers.UI;

namespace Code.Core.UI._Base
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

        protected UISubStateBase(UISubStateBaseData data, Enum defaultSubState)
        {
            UIManager = data.UIManager;
            DefaultSubState = defaultSubState;
            BaseStateType = data.BaseStateType;
            CurrentSubState = DefaultSubState;
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
