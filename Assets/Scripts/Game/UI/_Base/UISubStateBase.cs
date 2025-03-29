using System;
using Core.FSM.Data;
using Core.Managers.UI;
using Db.Data;

namespace Game.UI._Base
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

        private void ShowView(EShowLogic showLogic = EShowLogic.Default) =>
            UIManager.ShowView(BaseStateType, DefaultSubState, showLogic);

        private void HideView(EShowLogic showLogic = EShowLogic.Default) =>
            UIManager.HideView(BaseStateType, DefaultSubState, showLogic);


        public void Enter()
        {
            ShowView();
            OnEnter();
        }

        protected virtual void OnEnter()
        {
        }

        public void Exit()
        {
            OnExit();
            HideView();
        }

        protected virtual void OnExit()
        {
        }
    }
}
