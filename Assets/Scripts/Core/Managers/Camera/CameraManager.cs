using System;
using Game.Systems;
using ModestTree;
using R3;
using UnityEngine;
using Zenject;

namespace Core.Managers.Camera
{
    namespace _Game._Scripts.Framework.Manager.JCamera
    {
        public interface ICameraManager
        {
            public void SetTarget(IFollowable target);
            public void RemoveTarget();
            public UnityEngine.Camera GetMainCamera();
            public Vector3 GetCamEulerAngles();
            public Quaternion GetCamRotation();
        }

        public sealed class CameraManager : CameraManagerBase, IInitializable
        {
            [SerializeField] private Vector3 cameraOffset = new(0, 10, -5);
            private UnityEngine.Camera mainCamera;


            private IFollowable _target;
            private readonly CompositeDisposable _disposables = new();

            public void Initialize()
            {
            }

            private void Start()
            {
                mainCamera = UnityEngine.Camera.main;
                Log.Warn("Start camera manager " + mainCamera.GetHashCode());

                if (!mainCamera) throw new NullReferenceException($"MainCamera is null. {this}");
                mainCamera.transform.position = cameraOffset;
            }

            private void SetCameraPosition(Vector3 position)
            {
                Log.Warn("<color=green>[CameraManager]</color>set camera position");
                var newPosition = position + cameraOffset;

                Log.Warn("camera " + mainCamera.GetHashCode());
                Log.Warn("new position = " + newPosition);
                if (mainCamera.transform.position == newPosition) return;
                mainCamera.transform.position = newPosition;
            }

            public override void SetTarget(IFollowable target)
            {
                if (target == null) throw new ArgumentNullException($"Target is null. {this}");
                if (_target == target)
                {
                    Log.Warn("target already set");
                    return;
                }

                _disposables.Clear();
                _target = target;

                Log.Warn("target set to " + _target);

                _target.Position.Subscribe(SetCameraPosition).AddTo(_disposables);
            }

            public override void RemoveTarget()
            {
                Log.Warn("remove camera target");
                _target = null;
                _disposables?.Dispose();
            }

            public override UnityEngine.Camera GetMainCamera() => mainCamera;
            public override Vector3 GetCamEulerAngles() => mainCamera.transform.eulerAngles;
            public override Quaternion GetCamRotation() => mainCamera.transform.rotation;
            private void OnDestroy() => _disposables?.Dispose();
        }
    }
}
