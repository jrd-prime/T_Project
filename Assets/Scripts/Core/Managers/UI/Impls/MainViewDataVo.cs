using System;
using Game.UI.Common;
using UnityEngine.Serialization;

namespace Core.Managers.UI.Impls
{
    [Serializable]
    public struct MainViewDataVo
    {
        public string viewId;
        [FormerlySerializedAs("MainView")] public ViewBase view;
    }
}
