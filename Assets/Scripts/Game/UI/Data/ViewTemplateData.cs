using UnityEngine.UIElements;

namespace Game.UI.Data
{
    public struct ViewTemplateData
    {
        public string ViewId;
        public string StateId;
        public bool InSafeZone;
        public VisualElement Template;
        public UIViewerDebugDataVo UIViewerDebugData;
    }
}
