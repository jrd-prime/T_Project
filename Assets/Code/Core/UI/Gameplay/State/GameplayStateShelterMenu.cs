using System;
using Code.Core.Data;
using Code.Core.UI._Base;

namespace Code.Core.UI.Gameplay.State
{
    public class GameplayStateShelterMenu : UISubStateBase
    {
        public GameplayStateShelterMenu(UISubStateBaseData data, Enum defaultSubState) : base(data, defaultSubState)
        {
        }

        public override void Enter()
        {
            ShowView(EShowLogic.OverSubView);
        }

        public override void Exit()
        {
            HideView();
        }
    }
}
