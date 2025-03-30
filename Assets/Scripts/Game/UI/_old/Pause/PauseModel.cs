using Game.UI._old.Base.Model;
using Game.UI._old.Pause.State;

namespace Game.UI._old.Pause
{
    public interface IPauseModel : IUIModel<PauseStateType>
    {
    }

    public sealed class PauseModel : CustomUIModelBase<PauseStateType>, IPauseModel
    {
        public override void Initialize()
        {
        }
    }
}
