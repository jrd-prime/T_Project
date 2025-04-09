using Core.Character.Common.Interfaces;
using UnityEngine;

namespace Core.Managers.Camera.Interfaces
{
    public interface ICameraManager
    {
        void SetTarget(IFollowable target);
        void RemoveTarget();
        UnityEngine.Camera GetMainCamera();
        Vector3 GetCamEulerAngles();
        Quaternion GetCamRotation();
    }
}
