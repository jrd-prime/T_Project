using R3;
using VContainer.Unity;

namespace Code.Core.WORK.UI.Bootstrap
{
    public interface ILoadingScreenViewModel : IInitializable
    {
        public ReactiveProperty<string> TitleText { get; }
    }
}
