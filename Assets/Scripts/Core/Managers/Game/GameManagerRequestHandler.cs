using Core.Extensions;
using Core.Managers.Game.Interfaces;
using Game.UI.Common.Base.Model;
using JetBrains.Annotations;
using Zenject;

namespace Core.Managers.Game
{
    /// <summary>
    /// Ловим и мерджим запросы из разных моделей и дальше передаем в GameManager
    /// </summary>
    [UsedImplicitly]
    public sealed class GameManagerRequestHandler : IInitializable
    {
        private IGameManager _gameManager;
        private DiContainer _container;

        [Inject]
        private void Construct(DiContainer container)
        {
            _container = container;
            _gameManager = _container.ResolveAndCheckOnNull<IGameManager>();
        }

        public void Initialize()
        {
            var menuModel = _container.ResolveAndCheckOnNull<IMenuModel>();
            // menuModel.GameStateChangeRequest += OnGameStateChangeRequest;
        }
    }
}
