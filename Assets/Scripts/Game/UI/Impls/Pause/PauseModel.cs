using Game.UI.Common.Base.Model;

namespace Game.UI.Impls.Pause
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
