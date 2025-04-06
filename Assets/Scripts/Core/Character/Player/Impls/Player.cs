using System;
using Core.Character.Player.Interactors;
using Core.Character.Player.Interfaces;
using R3;
using UnityEngine;
using Zenject;

namespace Core.Character.Player.Impls
{
    [RequireComponent(typeof(Rigidbody))]
    public sealed class Player : MonoBehaviour, IPlayer
    {
        [SerializeField] private FrontTriggerArea frontTriggerArea;
        public ReactiveProperty<Vector3> Position { get; } = new();
        public int Id { get; }
        public string Name { get; }
        public string Description { get; }
        public int Health { get; }
        public int MaxHealth { get; }

        [Inject] private PlayerInteractor _interactor;
        private Rigidbody _rb;
        [SerializeField] private float moveSpeed = 5f;
        [SerializeField] private float rotationSpeed = 10f;
        [SerializeField] private float acceleration = 0.1f;
        private Vector3 _currentVelocity;
        private Camera _mainCamera;
        private Vector3 _previousPosition;

        private void Awake()
        {
            if (!frontTriggerArea) throw new NullReferenceException($"{nameof(frontTriggerArea)} is null. {name}");
            frontTriggerArea.Init(this);

            _rb = GetComponent<Rigidbody>();
        }

        private void Start() => _mainCamera = _interactor.MainCamera;

        private void FixedUpdate()
        {
            Move(_interactor.MoveDirection);

            if (_previousPosition != transform.position) UpdatePosition();
        }

        private void UpdatePosition()
        {
            var position = transform.position;
            _previousPosition = position;
            Position.Value = position;
            _interactor.SetPosition(position);
        }

        private void Move(Vector3 moveDirection)
        {
            if (moveDirection != Vector3.zero)
            {
                var cameraForward = _mainCamera.transform.forward;
                var cameraRight = _mainCamera.transform.right;

                cameraForward.y = cameraRight.y = 0f;
                cameraForward = cameraForward.normalized;
                cameraRight = cameraRight.normalized;

                var adjustedDirection =
                    (cameraForward * moveDirection.z + cameraRight * moveDirection.x).normalized;

                var targetVelocity = adjustedDirection * moveSpeed;
                _currentVelocity = Vector3.Lerp(_currentVelocity, targetVelocity, acceleration);
                _rb.linearVelocity = new Vector3(_currentVelocity.x, _rb.linearVelocity.y, _currentVelocity.z);

                var targetRotation = Quaternion.LookRotation(adjustedDirection);
                transform.rotation =
                    Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.fixedDeltaTime);
            }
            else
            {
                _currentVelocity = Vector3.Lerp(_currentVelocity, Vector3.zero, acceleration);
                _rb.linearVelocity = new Vector3(_currentVelocity.x, _rb.linearVelocity.y, _currentVelocity.z);
            }
        }
    }
}
