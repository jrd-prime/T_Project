namespace Core.Managers.UI.Impls
{
    public record UIViewerDebugDataVo(string Name, int ViewStackCount, bool IsOverlay)
    {
        public string Name { get; } = Name;
        public int ViewStackCount { get; } = ViewStackCount;
        public bool IsOverlay { get; } = IsOverlay;
    }
}
