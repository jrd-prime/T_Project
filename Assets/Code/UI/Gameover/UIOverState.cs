using Code.UI._Base;
using Code.UI._Base.Model;
using Code.UI._Base.ViewStateTypes;

namespace Code.UI.Gameover
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
