using System;
using System.Collections.Generic;
using Code.Core.GameStateMachine;
using Code.Core.Providers;
using Code.Core.SO;
using Code.Extensions;
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
        private ISettingsProvider _settingsProvider;

        private readonly Dictionary<StateType, StateUIView> _states = new();
        private readonly CompositeDisposable _disposables = new();
        private IStateUI _current;
        private IObjectResolver _resolver;

        [Inject]
        private void Construct(IObjectResolver resolver)
        {
            Debug.LogWarning("construct ui manager");
            _resolver = resolver;
        }

        public void Initialize()
        {
            _settingsProvider = _resolver.ResolveAndCheckOnNull<ISettingsProvider>();
            var viewsSettings = _settingsProvider.GetSettings<UIViewsSettings>();
            InitializeViews(viewsSettings);

            _stateMachine = _resolver.ResolveAndCheckOnNull<IGameStateMachine>();

            _stateMachine.GameState.DistinctUntilChanged().Subscribe(HandleGameState).AddTo(_disposables);
        }

        private void InitializeViews(UIViewsSettings viewsSettings)
        {
            foreach (var viewSettings in viewsSettings.States)
            {
                _states.TryAdd(viewSettings.GameState, viewSettings.SateUIView);
            }
        }

        private void HandleGameState(StateType state)
        {
            if (!_states.TryGetValue(state, out var uiState)) throw new KeyNotFoundException($"{state} not found!");

            ChangeState(uiState);
        }

        private void ChangeState(IStateUI newStateUI)
        {
            _current?.Hide();
            _current = newStateUI;
            _current.Show();
        }
    }
}
