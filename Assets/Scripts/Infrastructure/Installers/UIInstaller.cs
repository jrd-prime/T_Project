using UnityEngine;
using Zenject;

namespace Infrastructure.Installers
{
    public class UIInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            
            Debug.Log("<color=cyan>UIInstaller</color>");
        }
    }
}
