using Game.UI._Base.Model;
using Game.UI.Pause.State;

namespace Game.UI.Pause
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
