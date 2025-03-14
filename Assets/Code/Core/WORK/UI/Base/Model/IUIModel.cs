using System;
using Code.Core.WORK.UIStates;
using Code.Core.WORK.UIStates._Base.UIStatesTypes;
using VContainer.Unity;

namespace Code.Core.WORK.UI.Base.Model
{
    public interface IUIModel<TSubStateEnum> : IInitializable where TSubStateEnum : Enum
    {
        public void SetGameState(StateData stateData);

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
