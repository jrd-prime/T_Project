using System;

namespace Game.UI._old.Pause.State
{
    public enum PauseStateType
    {
        Main = 0
    }

    public sealed class PauseState : UIStateBase<IPauseModel, PauseStateType>
    {
        protected override void InitializeSubStates()
        {
            // var stateBaseData = new UISubStateBaseData(UIManager, GameStateType.Pause);
            // RegisterSubState(PauseStateType.Main, new PauseStateMain(stateBaseData, PauseStateType.Main));
        }

        protected override void OnEnter()
        {
            throw new NotImplementedException();
        }

        protected override void OnExit()
        {
            throw new NotImplementedException();
        }

        protected override void Subscribe()
        {
            throw new NotImplementedException();
        }

    
    }
}
