using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DebugLoadSceneButton : MonoBehaviour
{
    [SerializeField]
    private Text _text = null;

    private string _sceneName = "";

    public void Initialized(string sceneName)
    {
        _sceneName= sceneName;
        gameObject.name = sceneName;
        _text.text = sceneName;

        GetComponent<Button>().onClick.AddListener(OnClick);
    }

    private void OnClick()
    {
        SceneManager.LoadScene(_sceneName);
    }
}
