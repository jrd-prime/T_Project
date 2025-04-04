using Core.Managers.UI.Impls;
using UnityEngine.UIElements;

namespace Game.UI.Common.Base.Data
{
    public struct ViewTemplateData
    {
        public string ViewId;
        public string StateId;
        public bool InSafeZone;
        public VisualElement Template;
        public DebugDataVo DebugData;
    }
}
