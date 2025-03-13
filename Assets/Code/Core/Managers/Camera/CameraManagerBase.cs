using Code.Core.Managers.Camera._Game._Scripts.Framework.Manager.JCamera;
using Code.Core.Systems;
using UnityEngine;

namespace Code.Core.Managers.Camera
{
    public abstract class CameraManagerBase : MonoBehaviour, ICameraManager
    {
        public abstract void SetTarget(IFollowable target);

        public abstract void RemoveTarget();

        public abstract UnityEngine.Camera GetMainCamera();

        public abstract Vector3 GetCamEulerAngles();

        public abstract Quaternion GetCamRotation();
    }
}
