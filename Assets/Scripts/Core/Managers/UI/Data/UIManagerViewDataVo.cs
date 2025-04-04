using Game.UI.Data;

namespace Core.Managers.UI.Impls
{
    public record UIManagerViewDataVo(
        ViewRegistryType RegistryType,
        string ViewId,
        UIViewer.Layer Layer = UIViewer.Layer.Default,
        bool IsOverlay = false)
    {
        public ViewRegistryType RegistryType { get; } = RegistryType;
        public string ViewId { get; } = ViewId;
        public UIViewer.Layer Layer { get; } = Layer;
        public bool IsOverlay { get; } = IsOverlay;
    }
}
