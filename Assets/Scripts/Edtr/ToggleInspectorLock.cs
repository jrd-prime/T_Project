#if UNITY_EDITOR
using Data;
using UnityEditor;

namespace Edtr
{
    public class ToggleInspectorLock : Editor
    {
        [MenuItem(ProjectConstant.AppName + "/Editor Hotkeys/Toggle Inspector Lock #e")]
        public static void ToggleLock() =>
            ActiveEditorTracker.sharedTracker.isLocked = !ActiveEditorTracker.sharedTracker.isLocked;
    }
}
#endif
