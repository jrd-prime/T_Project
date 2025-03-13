using System;
using R3;

namespace Code.Core.WORK.UI.Bootstrap
{
    public interface ILoadingScreenModel : IDisposable
    {
        public ReactiveProperty<string> LoadingText { get; }
        public void SetLoadingText(string value);
    }
}
