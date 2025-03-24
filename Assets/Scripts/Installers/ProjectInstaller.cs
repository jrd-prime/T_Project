using UnityEngine;
using Zenject;

namespace Installers
{
    public class ProjectInstaller: MonoInstaller
    {
        public override void InstallBindings()
        {
            Debug.LogWarning("ProjectInstaller.InstallBindings");
        }
    }
}
