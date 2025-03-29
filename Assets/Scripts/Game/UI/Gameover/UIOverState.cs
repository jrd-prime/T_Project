using Game.UI._Base;
using Game.UI._Base.Model;
using Game.UI._Base.ViewStateTypes;

namespace Game.UI.Gameover
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
