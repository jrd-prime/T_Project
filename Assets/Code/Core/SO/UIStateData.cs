using System;
using Code.Core.FSM;
using Code.Core.UI._Base.View;
using UnityEngine.Serialization;

namespace Code.Core.SO
{
    [Serializable]
    public struct UIStateData
    {
        public GameStateType GameState;
        [FormerlySerializedAs("sateUIViewBase")] [FormerlySerializedAs("SateUIView")] public ViewBase sateViewBase;
    }
}
