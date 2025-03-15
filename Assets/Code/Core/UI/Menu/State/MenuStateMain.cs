using System;
using Code.Core.UI._Base;

namespace Code.Core.UI.Menu.State
{
    public class MenuStateMain : UISubStateBase
    {
        public MenuStateMain(UISubStateBaseData data, Enum defaultSubState) : base(data, defaultSubState)
        {
        }

        public override void Enter()
        {
            ShowView();
        }

        public override void Exit()
        {
            // HideView();
        }
    }
}
