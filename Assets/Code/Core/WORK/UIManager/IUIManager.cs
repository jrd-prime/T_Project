using System;
using Code.Core.WORK.Enums;
using Code.Core.WORK.Enums.States;
using Code.Core.WORK.JStateMachine.Data;
using VContainer.Unity;

namespace Code.Core.WORK.UIManager
{
    public interface IUIManager : IInitializable
    {
        public void ShowView(EGameState eGameState, Enum subState, EShowLogic showLogic = EShowLogic.Default);
        public void HideView(EGameState eGameState, Enum subState, EShowLogic showLogic);
        public void ShowPopUpAsync(string clickTimesToExit, int doubleClickDelay);
        public StateData GetPreviousState();
    }
}
