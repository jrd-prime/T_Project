using System;
using Code.Core.GameStateMachine;
using Code.Core.Managers.UI;

namespace Code.Core.SO
{
    [Serializable]
    public struct UIStateData
    {
        public StateType GameState;
        public StateUIView SateUIView;
    }
}
