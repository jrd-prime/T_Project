using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Edtr
{
    [InitializeOnLoad]
    public static class EditSceneHelper
    {
        private static string _secondScenePath = null;

        static EditSceneHelper() => EditorApplication.playModeStateChanged += OnPlayModeStateChanged;

        private static void OnPlayModeStateChanged(PlayModeStateChange state)
        {
            if (state == PlayModeStateChange.ExitingEditMode)
            {
                var sceneCount = SceneManager.sceneCount;

                if (sceneCount > 1)
                {
                    var secondScene = SceneManager.GetSceneAt(1);

                    if (!secondScene.isLoaded) return;

                    _secondScenePath = secondScene.path;
                    EditorSceneManager.CloseScene(secondScene, true);
                    Debug.Log(
                        $"Вторая сцена '{secondScene.name}' удалена из иерархии. Путь сохранен: {_secondScenePath}");
                }
                else Debug.Log("В иерархии только одна сцена, ничего не удаляем.");
            }
            else if (state == PlayModeStateChange.EnteredEditMode)
            {
                if (string.IsNullOrEmpty(_secondScenePath)) return;

                var isSceneAlreadyOpen = false;

                for (var i = 0; i < SceneManager.sceneCount; i++)
                {
                    if (SceneManager.GetSceneAt(i).path != _secondScenePath) continue;
                    isSceneAlreadyOpen = true;
                    break;
                }

                if (!isSceneAlreadyOpen) EditorSceneManager.OpenScene(_secondScenePath, OpenSceneMode.Additive);
                else Debug.Log($"Сцена '{_secondScenePath}' уже открыта.");

                _secondScenePath = null;
            }
        }
    }
}
