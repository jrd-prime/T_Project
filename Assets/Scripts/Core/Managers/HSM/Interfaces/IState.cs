namespace Core.Managers.HSM.Interfaces
{
    public interface IState
    {
        void Enter();
        void Exit();
        void Update();
        IState HandleTransition();
    }
}
