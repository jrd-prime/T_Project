using Code.Core.FSM;
using Code.Core.Managers.UI;

namespace Code.UI._Base
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
