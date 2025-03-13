using Code.Core.WORK.Enums.States;
using Code.Core.WORK.JStateMachine._Base;
using Code.Core.WORK.JStateMachine.State.Menu.State.SubState;
using Code.Core.WORK.UI.Base.Model;

namespace Code.Core.WORK.JStateMachine.State.Menu.State
{
    public class MenuState : GameStateBase<IMenuModel, EMenuSubState>
    {
        protected override void InitializeSubStates()
        {
            SubStatesCache.TryAdd(EMenuSubState.Main,
                new MenuMainSubState(UIManager, EGameState.Menu, EMenuSubState.Main));
            SubStatesCache.TryAdd(EMenuSubState.Settings,
                new MenuSettingsSubState(UIManager, EGameState.Menu, EMenuSubState.Settings));
        }

        protected override void InitCustomSubscribes()
        {
        }

        protected override void OnBaseStateEnter()
        {
        }

        protected override void OnBaseStateExit()
        {
        }
    }
}
