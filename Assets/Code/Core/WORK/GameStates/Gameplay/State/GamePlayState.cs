using Code.Core.WORK.Enums.States;
using Code.Core.WORK.GameStates.Gameplay.State.SubState;
using Code.Core.WORK.GameStates.Gameplay.UI;

namespace Code.Core.WORK.GameStates.Gameplay.State
{
    public sealed class GamePlayState : GameStateBase<IGameplayModel, EGameplaySubState>
    {
        protected override void InitializeSubStates()
        {
            SubStatesCache.TryAdd(EGameplaySubState.Main,
                new GameplayMainSubState(UIManager, EGameState.Gameplay, EGameplaySubState.Main));
            SubStatesCache.TryAdd(EGameplaySubState.ShelterMenu,
                new GameplayShelterMenuSubState(UIManager, EGameState.Gameplay, EGameplaySubState.ShelterMenu));
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
