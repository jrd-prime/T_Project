using System;
using Code.Core.JStateMachineOLD;
using Code.Core.WORK.UI.Base.View;
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
