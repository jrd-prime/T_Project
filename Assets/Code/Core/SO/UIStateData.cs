using System;
using Code.Core.WORK.JStateMachine;
using Code.Core.WORK.UIStates._Base.View;
using UnityEngine.Serialization;

namespace Code.Core.SO
{
    [Serializable]
    public struct UIStateData
    {
        public GameStateType GameState;
        [FormerlySerializedAs("SateUIView")] public UIViewBase sateUIViewBase;
    }
}
