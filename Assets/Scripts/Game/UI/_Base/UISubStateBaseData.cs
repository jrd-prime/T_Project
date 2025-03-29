using Core.FSM.Data;
using Core.Managers.UI;

namespace Game.UI._Base
{
    public sealed class UISubStateBaseData
    {
        public readonly IUIManager UIManager;
        public readonly GameStateType BaseStateType;

        public UISubStateBaseData(IUIManager uiManager, GameStateType baseStateType)
        {
            UIManager = uiManager;
            BaseStateType = baseStateType;
        }
    }
}
