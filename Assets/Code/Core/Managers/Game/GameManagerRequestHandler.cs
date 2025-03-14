using Code.Core.UI._Base.Model;
using Code.Extensions;
using JetBrains.Annotations;
using VContainer;
using VContainer.Unity;

namespace Code.Core.Managers.Game
{
    /// <summary>
    /// Ловим и мерджим запросы из разных моделей и дальше передаем в GameManager
    /// </summary>
    [UsedImplicitly]
    public sealed class GameManagerRequestHandler : IInitializable
    {
        private IGameManager _gameManager;
        private IObjectResolver _resolver;

        [Inject]
        private void Construct(IObjectResolver resolver)
        {
            _resolver = resolver;
            _gameManager = _resolver.ResolveAndCheckOnNull<IGameManager>();
        }

        public void Initialize()
        {
            var menuModel = _resolver.ResolveAndCheckOnNull<IMenuModel>();
            // menuModel.GameStateChangeRequest += OnGameStateChangeRequest;
        }
    }
}
