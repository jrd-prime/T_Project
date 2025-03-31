using Game.UI._old.Base.Model;

namespace Game.UI._old.Pause
{
    public interface IPauseModel : IUIModel
    {
    }

    public sealed class PauseModel : CustomUIModelBase, IPauseModel
    {
        public override void Initialize()
        {
        }
    }
}
