using Core.Managers.UI.Impls;
using Game.UI.Data;

namespace Core.Managers.UI.Data
{
    public record UIManagerViewDataVo(
        ViewRegistryType RegistryType,
        string ViewId,
        UIRenderer.Layer Layer = UIRenderer.Layer.Default,
        bool IsOverlay = false)
    {
        public ViewRegistryType RegistryType { get; } = RegistryType;
        public string ViewId { get; } = ViewId;
        public UIRenderer.Layer Layer { get; } = Layer;
        public bool IsOverlay { get; } = IsOverlay;
    }
}
