using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

public class DebugRootSystem : MonoBehaviour
{
    [SerializeField]
    private DebugLoadSceneButton _loadSceneButtonPrefab = null;

    [SerializeField]
    private Transform _loadSceneButtonRoot = null;

    private void Start()
    {
        var sceneCount = SceneManager.sceneCountInBuildSettings;
        for (var i = 0; i < sceneCount; i++)
        {
            var scenePath = SceneUtility.GetScenePathByBuildIndex(i);
            var sceneName = Path.GetFileNameWithoutExtension(scenePath);
            if (sceneName == "DebugRootScene")
            {
                continue;
            }
            var button = Instantiate(_loadSceneButtonPrefab, _loadSceneButtonRoot);
            button.GetComponent<DebugLoadSceneButton>().Initialized(sceneName);
        }
    }
}
