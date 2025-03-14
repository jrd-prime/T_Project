using Code.Core.WORK.UI.Base.Model;
using Code.Core.WORK.UIStates._Base.UIStatesTypes;

namespace Code.Core.WORK.UIStates.Gameover
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
