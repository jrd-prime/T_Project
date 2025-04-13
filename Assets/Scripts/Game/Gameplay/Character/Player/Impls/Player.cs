using System;
using Core.Character.Common.Interfaces;
using Core.Character.Player.Interfaces;
using Core.Data;
using Game.Extensions;
using ModestTree;
using R3;
using UnityEngine;
using Zenject;

namespace Game.Gameplay.Character.Player.Impls
{
    [RequireComponent(typeof(Rigidbody), typeof(CapsuleCollider), typeof(UnityEngine.Animator))]
    public sealed class Player : MonoBehaviour, IPlayer
    {
        [SerializeField] private PlayerFrontTriggerArea frontTriggerArea;
        [SerializeField] private float moveSpeed = 5f;
        [SerializeField] private float rotationSpeed = 10f;
        [SerializeField] private float acceleration = 0.1f;

        public ReactiveProperty<JVector3> Position { get; } = new();
        public string Id => _interactor.Id;
        public string Name { get; }
        public string Description { get; }
        public object Animator { get; private set; }
        public int Health { get; }
        public int MaxHealth { get; }
        public ICharacterInteractor GetInteractor() => _interactor;

        [Inject] private PlayerInteractor _interactor;

        private Rigidbody _rb;
        private Vector3 _currentVelocity;
        private Camera _mainCamera;
        private Vector3 _previousPosition;

        private void Awake()
        {
            if (!frontTriggerArea)
                throw new NullReferenceException($"{nameof(frontTriggerArea)} is null. {name}");
            frontTriggerArea.Init(this);

            Animator = GetComponent<UnityEngine.Animator>();
            Log.Warn("player animator = " + Animator);
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
            Position.Value = position.ToJVector3();
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
