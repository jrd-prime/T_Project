using Code.UI._Base.Model;
using Code.UI.Pause.State;

namespace Code.UI.Pause
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
