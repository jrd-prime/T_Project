using System;
using Core.FSM.Data;
using Game.UI._old.Base.ViewStateTypes;
using Game.UI._old.Menu.State;
using Zenject;

namespace Game.UI._old.Base.Model
{
    public interface IUIModel<TSubStateEnum> : IInitializable where TSubStateEnum : Enum
    {
        public void GameStateChangeRequest(StateDataVo stateDataVo);

        // public void SetPreviousState();
    }

    public interface IMenuModel : IUIModel<MenuStateType>
    {
    }


    public interface IGameoverModel : IUIModel<EGameoverSubState>
    {
    }


    public interface IWinModel : IUIModel<EWinSubState>
    {
    }
}
