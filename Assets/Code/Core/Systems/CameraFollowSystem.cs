using System;
using Code.Core.Managers.Camera._Game._Scripts.Framework.Manager.JCamera;
using R3;
using UnityEngine;
using VContainer;

namespace Code.Core.Systems
{
    public interface IFollowable
    {
        public ReadOnlyReactiveProperty<Vector3> Position { get; }
        public ReadOnlyReactiveProperty<Quaternion> Rotation { get; }
    }

    public class CameraFollowSystem
    {
        private ICameraManager _cameraManager;
        private bool _hasTarget;

        [Inject]
        private void Construct(ICameraManager cameraManager) => _cameraManager = cameraManager;

        public void SetTarget(IFollowable target)
        {
            if (_cameraManager == null)
                throw new NullReferenceException("CameraManager is null " + nameof(CameraFollowSystem));
            if (target == null) throw new NullReferenceException("Target is null " + nameof(CameraFollowSystem));

            // TODO remove this
            if (_hasTarget)
            {
                Debug.LogWarning("CameraFollowSystem already has target. Removing...");
                _cameraManager.RemoveTarget();
                _hasTarget = false;
            }
            else
            {
                _cameraManager.SetTarget(target);
                _hasTarget = true;
            }
        }
    }
}
