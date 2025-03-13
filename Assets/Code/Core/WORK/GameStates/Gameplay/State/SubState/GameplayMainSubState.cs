using System;
using Code.Core.WORK.Enums.States;
using Code.Core.WORK.UIManager;

namespace Code.Core.WORK.GameStates.Gameplay.State.SubState
{
    public class GameplayMainSubState : SubStateBase
    {
        public GameplayMainSubState(IUIManager uiManager, EGameState baseState, Enum defaultSubState) : base(uiManager,
            baseState, defaultSubState)
        {
        }

        public override void Enter()
        {
            ShowView();
        }

        public override void Exit()
        {
            // Hide only when we change BASE state
            // HideView();
        }
    }
}
