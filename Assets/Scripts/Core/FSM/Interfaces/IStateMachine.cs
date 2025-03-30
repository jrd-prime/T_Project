using System;
using Core.FSM.Data;
using R3;
using Zenject;

namespace Core.FSM.Interfaces
{
    public interface IStateMachine : IInitializable, IDisposable
    {
        public ReadOnlyReactiveProperty<GameStateType> CurrentState { get; }
        public ReadOnlyReactiveProperty<Enum> CurrentSubState { get; }
    }
}
