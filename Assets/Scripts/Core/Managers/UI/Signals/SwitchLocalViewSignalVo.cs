namespace Core.Managers.UI.Signals
{
    public record SwitchLocalViewSignalVo(string ViewId)
    {
        public string ViewId { get; private set; } = ViewId;
    }
}