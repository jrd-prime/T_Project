using System;
using Code.Core.JStateMachine;
using Code.Core.Managers.UI;
using Code.Core.UI._Base.View;
using UnityEngine.Serialization;

namespace Code.Core.SO
{
    [Serializable]
    public struct UIStateData
    {
        public StateType GameState;
        [FormerlySerializedAs("SateUIView")] public UIViewBase sateUIViewBase;
    }
}
