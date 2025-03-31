using System;
using Core.FSM.Data;
using Game.UI.Common;
using UnityEngine.Serialization;

namespace Game.SO
{
    [Serializable]
    public struct UIStateData
    {
        public GameStateType GameState;
        [FormerlySerializedAs("sateUIViewBase")] [FormerlySerializedAs("SateUIView")] public ViewBase sateViewBase;
    }
}
