using Game.UI.Data;

namespace Core.Managers.UI.Signals
{
    public record SwitchLocalViewSignalVo(ViewRegistryType ViewRegistryType, string ViewId)
    {
        public ViewRegistryType ViewRegistryType { get; private set; } = ViewRegistryType;
        public string ViewId { get; private set; } = ViewId;
    }
}
