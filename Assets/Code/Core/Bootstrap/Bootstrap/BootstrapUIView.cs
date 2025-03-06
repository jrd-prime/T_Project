using System;
using Code.Extensions;
using Code.Tools;
using R3;
using UnityEngine;
using UnityEngine.UIElements;
using VContainer;

namespace Code.Core.Bootstrap.Bootstrap
{
    [RequireComponent(typeof(UIDocument))]
    public sealed class BootstrapUIView : MonoBehaviour
    {
        private const string TitleLabelId = "header-label";

        private IBootstrapUIViewModel _viewModel;
        private Label _title;

        private readonly CompositeDisposable _disposables = new();

        [Inject]
        private void Construct(IBootstrapUIViewModel viewModel) => _viewModel = viewModel;

        private void Awake()
        {
            Helper.CheckOnNull(_viewModel, "ViewModel", name);

            var root = gameObject.GetComponent<UIDocument>().rootVisualElement ??
                       throw new NullReferenceException("RootVisualElement is null.");

            _title = root.GetVisualElement<Label>(TitleLabelId, name);

            _viewModel.TitleText.Subscribe(SetTitle).AddTo(_disposables);
        }

        private void SetTitle(string value) => _title.text = !string.IsNullOrEmpty(value) ? value : "Not set";

        private void OnDestroy() => _disposables?.Dispose();
    }
}
