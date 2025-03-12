using System;
using System.Collections.Generic;
using Code.Core.JStateMachine;
using Code.Core.Providers;
using Code.Core.SO;
using Code.Core.UI._Base.View;
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

        private readonly Dictionary<StateType, UIViewBase> _states = new();
        private readonly CompositeDisposable _disposables = new();
        private IUIView _current;
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
                var viewInstance = viewSettings.sateUIViewBase;
                _resolver.Inject(viewInstance);
                _states.TryAdd(viewSettings.GameState, viewSettings.sateUIViewBase);
            }
        }

        private void HandleGameState(StateType state)
        {
            if (!_states.TryGetValue(state, out var uiState)) throw new KeyNotFoundException($"{state} not found!");

            ChangeState(uiState);
        }

        private void ChangeState(IUIView newIuiView)
        {
            _current?.Hide();
            _current = newIuiView;
            _current.Show();
        }
    }
}
