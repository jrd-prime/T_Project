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
        private UIViewBase _current;
        private IObjectResolver _resolver;

        private readonly Dictionary<StateType, UIViewBase> _states = new();
        private readonly CompositeDisposable _disposables = new();

        [Inject]
        private void Construct(IObjectResolver resolver) => _resolver = resolver;

        public void Initialize()
        {
            Debug.LogWarning("ui manager init");
            _settingsProvider = _resolver.ResolveAndCheckOnNull<ISettingsProvider>();
            var viewsSettings = _settingsProvider.GetSettings<UIViewsSettings>();
            InitializeViews(viewsSettings);

            _stateMachine = _resolver.ResolveAndCheckOnNull<IGameStateMachine>();

            _stateMachine.GameState.DistinctUntilChanged().Subscribe(HandleGameState).AddTo(_disposables);
        }


        private void InitializeViews(UIViewsSettings viewsSettings)
        {
            var uiViews = _resolver.Instantiate(new GameObject("UIViews"));

            foreach (var viewSettings in viewsSettings.States)
            {
                var viewPrefab = viewSettings.sateUIViewBase;
                var viewInstance = _resolver.Instantiate(viewPrefab);
                viewInstance.transform.SetParent(uiViews.transform, false);
                viewInstance.Hide();
                _states.TryAdd(viewSettings.GameState, viewInstance);
            }
        }

        private void HandleGameState(StateType state)
        {
            Debug.LogWarning(" handle game state " + state);
            if (!_states.TryGetValue(state, out var uiState)) throw new KeyNotFoundException($"{state} not found!");

            ChangeState(uiState);
        }

        private void ChangeState(UIViewBase newIuiView)
        {
            _current?.Hide();
            _current = newIuiView;
            _current.Show();
        }
    }
}
