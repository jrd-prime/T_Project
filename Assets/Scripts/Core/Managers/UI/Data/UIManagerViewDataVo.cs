namespace Core.Managers.UI.Data
{
    public record UIManagerViewDataVo(
        ViewRegistryType RegistryType,
        string ViewId,
        ViewerLayer ViewerLayer = ViewerLayer.Default,
        bool IsOverlay = false)
    {
        public ViewRegistryType RegistryType { get; } = RegistryType;
        public string ViewId { get; } = ViewId;
        public ViewerLayer ViewerLayer { get; } = ViewerLayer;
        public bool IsOverlay { get; } = IsOverlay;
    }
}
