using Core.Managers.UI.Impls;
using Game.UI.Data;

namespace Core.Managers.UI.Signals
{
    public record ShowViewSignalVo(
        ViewRegistryType ViewRegistryType,
        string ViewId,
        UIRenderer.Layer Layer = UIRenderer.Layer.Default,
        bool IsOverlay = false)
    {
        public ViewRegistryType ViewRegistryType { get; private set; } = ViewRegistryType;
        public string ViewId { get; private set; } = ViewId;
        public UIRenderer.Layer Layer { get; private set; } = Layer;
        public bool IsOverlay { get; private set; } = IsOverlay;
    }
}
