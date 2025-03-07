using System;
using R3;
using VContainer;
using VContainer.Unity;

namespace Code.Core.Bootstrap
{
    public interface IBootstrapUIViewModel : IInitializable
    {
        public ReadOnlyReactiveProperty<string> TitleText { get; }
        public ReadOnlyReactiveProperty<float> Opacity { get; }
    }

    public class BootstrapUIViewModel : IBootstrapUIViewModel
    {
        public ReadOnlyReactiveProperty<string> TitleText => _model.LoadingText;
        public ReadOnlyReactiveProperty<float> Opacity => _model.Opacity;

        private IBootstrapUIModel _model;

        [Inject]
        private void Construct(IBootstrapUIModel bootstrapUIModel) => _model = bootstrapUIModel;

        public void Initialize()
        {
            if (_model == null) throw new NullReferenceException($"{typeof(IBootstrapUIModel)} is null.");
        }
    }
}
