using Code.Core.FSM;
using Code.Core.UI._Base.ViewStateTypes;

namespace Code.Core.UI.Gameplay.State
{
    public enum GameplayStateType
    {
        Main = 0,
        Inventory
    }

    public sealed class GameplayState : UIStateBase<IGameplayModel, GameplayStateType>
    {
        protected override void InitializeSubStates()
        {
            SubStatesCache.TryAdd(GameplayStateType.Main,
                new GameplayStateMain(UIManager, GameStateType.Gameplay, GameplayStateType.Main));
            SubStatesCache.TryAdd(GameplayStateType.Inventory,
                new GameplayStateShelterMenu(UIManager, GameStateType.Gameplay, GameplayStateType.Inventory));
        }

        protected override void InitCustomSubscribes()
        {
        }

        protected override void OnBaseStateEnter()
        {
            GameManager.StartNewGame();
        }

        protected override void OnBaseStateExit()
        {
            GameManager.Pause();
        }
    }
}
