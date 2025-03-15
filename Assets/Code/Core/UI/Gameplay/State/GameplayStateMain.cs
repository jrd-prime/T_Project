using System;
using Code.Core.UI._Base;

namespace Code.Core.UI.Gameplay.State
{
    public class GameplayStateMain : UISubStateBase
    {
        public GameplayStateMain(UISubStateBaseData data, Enum defaultSubState) : base(data, defaultSubState)
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
