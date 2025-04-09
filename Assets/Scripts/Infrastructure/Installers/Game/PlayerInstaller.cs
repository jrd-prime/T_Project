using Core.Character.Player;
using Game.Character.Player.Impls;
using UnityEngine;
using Zenject;

namespace Infrastructure.Installers.Game
{
    public sealed class PlayerInstaller : MonoInstaller
    {
        [SerializeField] private Player playerPrefab;

        public override void InstallBindings()
        {
        
            Container.BindInterfacesAndSelfTo<Player>().FromComponentInNewPrefab(playerPrefab).AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<PlayerInteractor>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<PlayerService>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<PlayerModel>().AsSingle().NonLazy();
        }
    }
}
