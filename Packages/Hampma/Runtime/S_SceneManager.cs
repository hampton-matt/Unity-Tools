using UnityEngine;
using UnityEngine.SceneManagement;

namespace Hampma.Tools
{
    public static class S_SceneManager
    {
        public static void OpenScene(string scene, bool add=true)
        {
            #if UNITY_EDITOR
            #else
            if (SceneUtility.GetBuildIndexByScenePath(scene) == -1) {
                Debug.Log("SceneManager: \tError \tScene not in build settings - Scene: " + scene);
                return;
            }
            #endif
            if (SceneManager.GetSceneByName(scene).isLoaded) {
                Debug.Log("SceneManager: \tError \tScene already loaded - Scene: " + scene);
                return;
            }
            SceneManager.LoadSceneAsync(scene, add ? LoadSceneMode.Additive : LoadSceneMode.Single);
        }

        public static void CloseScene(string scene)
        {
            if (SceneManager.GetSceneByName(scene).isLoaded) {
                SceneManager.UnloadSceneAsync(scene);
            } else Debug.Log("SceneManager: \tWarn \tUnable to close scene as it was never loaded - Scene: " + scene);
        }
    }
}
