using Core.Managers.UI.Data;

namespace Game.UI.Signals
{
    public record ShowViewSignalVo(
        ViewRegistryType ViewRegistryType,
        string ViewId,
        ViewerLayer ViewerLayer = ViewerLayer.Default,
        bool IsOverlay = false)
    {
        public ViewRegistryType ViewRegistryType { get; private set; } = ViewRegistryType;
        public string ViewId { get; private set; } = ViewId;
        public ViewerLayer ViewerLayer { get; private set; } = ViewerLayer;
        public bool IsOverlay { get; private set; } = IsOverlay;
    }
}
