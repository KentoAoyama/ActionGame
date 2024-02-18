using UnityEngine;
using Cinemachine;

public class BattleMockCanvas : MonoBehaviour
{
    [SerializeField]
    private GameObject _battleMockCanvas;

    [SerializeField]
    private CinemachineVirtualCamera _virtualCamera;

    private void Start()
    {
        _battleMockCanvas.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            if (_battleMockCanvas.activeSelf)
            {
                _battleMockCanvas.SetActive(false);
            }
            else
            {
                _battleMockCanvas.SetActive(true);
            }
        }   

        if (!_battleMockCanvas.activeSelf)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            _virtualCamera.enabled = true;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;
            _virtualCamera.enabled = false;
        }
    }
}
