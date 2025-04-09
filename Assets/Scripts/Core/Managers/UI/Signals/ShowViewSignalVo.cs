using Game.UI.Data;
using Game.UI.Impls;

namespace Core.Managers.UI.Signals
{
    public record ShowViewSignalVo(
        ViewRegistryType ViewRegistryType,
        string ViewId,
        UIViewer.Layer Layer = UIViewer.Layer.Default,
        bool IsOverlay = false)
    {
        public ViewRegistryType ViewRegistryType { get; private set; } = ViewRegistryType;
        public string ViewId { get; private set; } = ViewId;
        public UIViewer.Layer Layer { get; private set; } = Layer;
        public bool IsOverlay { get; private set; } = IsOverlay;
    }
}
