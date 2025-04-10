using Core.Character.Player;
using Game.Currency;
using Game.Extensions;
using Game.Managers;
using Infrastructure.Input.Signals;
using UnityEngine;
using Zenject;

namespace Game.Gameplay.Character.Player.Impls
{
    public sealed class PlayerInteractor
    {
        public Vector3 MoveDirection { get; private set; } = Vector3.zero;
        public Camera MainCamera => _cameraManager.GetMainCamera();
        public string Id => _service.Id;


        private readonly PlayerService _service;
        private readonly SignalBus _signalBus;
        private readonly ICameraManager _cameraManager;
        private readonly IWallet _wallet;

        public PlayerInteractor(PlayerService service, SignalBus signalBus, ICameraManager cameraManager,
            ICurrencyService currencyService)
        {
            _service = service;
            _signalBus = signalBus;
            _cameraManager = cameraManager;

            _wallet = currencyService.CreateWallet("player_test_id");

            signalBus.Subscribe<MoveDirectionSignal>(OnMoveDirectionSignal);
        }

        private void OnMoveDirectionSignal(MoveDirectionSignal signal) => MoveDirection = signal.Direction;

        /// <summary>
        /// Такое себе решение. // TODO: Подумать как лучше сделать с учетом плеера
        /// </summary>
        public void SetPosition(Vector3 position) => _service.SetPosition(position.ToJVector3());
    }
}
