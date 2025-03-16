using System;
using Code.UI._Base.Data;
using Code.UI._Base.ViewStateTypes;
using Code.UI.Menu.State;
using VContainer.Unity;

namespace Code.UI._Base.Model
{
    public interface IUIModel<TSubStateEnum> : IInitializable where TSubStateEnum : Enum
    {
        public void GameStateChangeRequest(StateData stateData);

        public void SetPreviousState();
    }

    public interface IMenuModel : IUIModel<MenuStateType>
    {
    }


    public interface IGameoverModel : IUIModel<EGameoverSubState>
    {
    }

    public interface IPauseModel : IUIModel<EPauseSubState>
    {
    }

    public interface IWinModel : IUIModel<EWinSubState>
    {
    }
}
