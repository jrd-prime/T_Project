using Code.Core.WORK.JStateMachine;
using Code.Core.WORK.UIStates._Base.UIStatesTypes;

namespace Code.Core.WORK.UIStates.Gameplay.State
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
