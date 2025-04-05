using Core.Character.Player.Services;
using Core.Managers.Camera.Interfaces;
using Infrastructure.Input.Signals;
using UnityEngine;
using Zenject;

namespace Core.Character.Player.Interactors
{
    public sealed class PlayerInteractor
    {
        public Vector3 MoveDirection { get; private set; } = Vector3.zero;
        public Camera MainCamera => _cameraManager.GetMainCamera();

        private readonly PlayerService _service;
        private readonly SignalBus _signalBus;
        private readonly ICameraManager _cameraManager;

        public PlayerInteractor(PlayerService service, SignalBus signalBus, ICameraManager cameraManager)
        {
            _service = service;
            _signalBus = signalBus;
            _cameraManager = cameraManager;


            signalBus.Subscribe<MoveDirectionSignal>(OnMoveDirectionSignal);
        }

        private void OnMoveDirectionSignal(MoveDirectionSignal signal) => MoveDirection = signal.Direction;
    }
}
