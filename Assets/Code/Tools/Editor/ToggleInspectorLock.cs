#if UNITY_EDITOR
using Code.Core.Data;
using UnityEditor;

namespace Code.Tools.Editor
{
    public class ToggleInspectorLock : UnityEditor.Editor
    {
        [MenuItem(ProjectConstant.AppName + "/Editor Hotkeys/Toggle Inspector Lock #e")]
        public static void ToggleLock() =>
            ActiveEditorTracker.sharedTracker.isLocked = !ActiveEditorTracker.sharedTracker.isLocked;
    }
}
#endif
