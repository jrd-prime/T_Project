using Core.FSM.Data;
using Zenject;

namespace Game.UI._old.Base.Model
{
    public interface IUIModel : IInitializable
    {
        public void GameStateChangeRequest(StateDataVo stateDataVo);

        // public void SetPreviousState();
    }

    public interface IMenuModel : IUIModel
    {
    }


    public interface IGameoverModel : IUIModel
    {
    }


    public interface IWinModel : IUIModel
    {
    }
}
