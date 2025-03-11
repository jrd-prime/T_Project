using System;
using Code.Core.Game;
using VContainer;
using VContainer.Unity;

namespace Code.Core.GameStateMachine
{
    public interface IGameState
    {
        public void Enter();
        public void Exit();
    }

    public abstract class GameStateBase : IGameState, IInitializable
    {
        public void Initialize()
        {
        }

        public abstract void Enter();
        public abstract void Exit();
    }
}
