using System;
using Code.Core.WORK.Enums;
using Code.Core.WORK.JStateMachine;
using Code.Core.WORK.UIStates;
using VContainer.Unity;

namespace Code.Core.WORK.UIManager
{
    public interface IUIManager : IInitializable
    {
        public void ShowView(GameStateType gameStateType, Enum subState, EShowLogic showLogic = EShowLogic.Default);
        public void HideView(GameStateType gameStateType, Enum subState, EShowLogic showLogic);
        public void ShowPopUpAsync(string clickTimesToExit, int doubleClickDelay);
        public StateData GetPreviousState();
    }
}
