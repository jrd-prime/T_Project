using Code.Core.GameStateMachine;
using R3;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Code.Core.Managers.UI
{
    public interface IUIManager
    {
    }

    public sealed class UIManager : MonoBehaviour, IUIManager, IInitializable
    {
        private IGameStateMachine _stateMachine;

        private readonly CompositeDisposable _disposables = new();

        [Inject]
        private void Construct(IGameStateMachine stateMachine)
        {
            _stateMachine = stateMachine;
        }

        public void Initialize()
        {
            _stateMachine.GameState.Subscribe(x => Debug.LogWarning("state changed to: " + x.GetType().Name + ")"))
                .AddTo(_disposables);
        }
    }
}
