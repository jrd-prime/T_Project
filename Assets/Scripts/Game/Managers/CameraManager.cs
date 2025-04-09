using System;
using Core.Character.Common.Interfaces;
using Core.Data;
using Game.Extensions;
using ModestTree;
using R3;
using UnityEngine;

namespace Game.Managers
{
    namespace _Game._Scripts.Framework.Manager.JCamera
    {
        public sealed class CameraManager : MonoBehaviour, ICameraManager
        {
            [SerializeField] private Vector3 cameraOffset = new(0, 10, -5);
            private Camera _mainCamera;
            private readonly CompositeDisposable _disposables = new();
            private IFollowable _target;

            private void Start()
            {
                _mainCamera = Camera.main;
                if (!_mainCamera) throw new NullReferenceException($"MainCamera is null. {this}");
                _mainCamera.transform.position = cameraOffset;
            }

            private void SetCameraPosition(JVector3 position)
            {
                Vector3 newPosition = position.ToVector3() + cameraOffset;
                if (_mainCamera.transform.position == newPosition) return;
                _mainCamera.transform.position = newPosition;
            }

            public void SetTarget(IFollowable target)
            {
                if (target == null) throw new ArgumentNullException($"Target is null. {this}");
                if (_target == target)
                {
                    Log.Warn("target already set");
                    return;
                }

                _disposables.Clear();
                _target = target;
                _target.Position.Subscribe(SetCameraPosition).AddTo(_disposables);
            }

            public void RemoveTarget()
            {
                _target = null;
                _disposables?.Dispose();
            }

            public Camera GetMainCamera() => _mainCamera;
            public Vector3 GetCamEulerAngles() => _mainCamera.transform.eulerAngles;
            public Quaternion GetCamRotation() => _mainCamera.transform.rotation;
            private void OnDestroy() => _disposables?.Dispose();
        }
    }
}
