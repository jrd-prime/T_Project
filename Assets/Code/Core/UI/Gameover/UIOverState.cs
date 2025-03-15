using Code.Core.UI._Base;
using Code.Core.UI._Base.Model;
using Code.Core.UI._Base.ViewStateTypes;

namespace Code.Core.UI.Gameover
{
    public sealed class UIOverState : UIStateBase<IGameoverModel, EGameoverSubState>
    {
        protected override void OnBaseStateEnter()
        {
            // UIManager.ShowView(EGameState.GameOver);
            GameManager.GameOver();
        }

        protected override void OnBaseStateExit()
        {
            // UIManager.HideView(EGameState.GameOver);
        }

        protected override void InitCustomSubscribes()
        {
        }

        protected override void InitializeSubStates()
        {
        }
    }
}
