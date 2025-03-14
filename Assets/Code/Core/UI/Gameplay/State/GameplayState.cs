using Code.Core.FSM;
using Code.Core.UI._Base.ViewStateTypes;

namespace Code.Core.UI.Gameplay.State
{
    public sealed class GameplayState : UIStateBase<IGameplayModel, GameplayStateType>
    {
        protected override void InitializeSubStates()
        {
            SubStatesCache.TryAdd(GameplayStateType.Main,
                new GameplayStateMain(UIManager, GameStateType.Gameplay, GameplayStateType.Main));
            SubStatesCache.TryAdd(GameplayStateType.ShelterMenu,
                new GameplayStateShelterMenu(UIManager, GameStateType.Gameplay, GameplayStateType.ShelterMenu));
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
