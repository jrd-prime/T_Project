using Core.FSM.Data;
using Game.UI._Base;

namespace Game.UI.Gameplay.State
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
            var main = new UISubStateBaseData(UIManager, GameStateType.Gameplay);
            var inventory = new UISubStateBaseData(UIManager, GameStateType.Gameplay);

            SubStates.TryAdd(GameplayStateType.Main, new GameplayStateMain(main, GameplayStateType.Main));
            SubStates.TryAdd(GameplayStateType.Inventory,
                new GameplayStateShelterMenu(inventory, GameplayStateType.Inventory));
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
