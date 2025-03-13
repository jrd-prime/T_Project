using UnityEditor;

namespace Code.Tools
{
    //TODO remove
    public static class ExitHelp
    {
        public static void ExitGame()
        {
#if UNITY_EDITOR
            EditorApplication.isPlaying = false;
#else
        UnityEngine.Application.Quit();
#endif
        }
    }
}
