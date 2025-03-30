namespace Core.HSM.Interfaces
{
    public interface IState
    {
        void Enter();
        void Exit();
        void Update();
        IState HandleTransition();
    }
}
