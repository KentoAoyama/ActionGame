using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

/// <summary>
/// �I�[�o�[���C�\���̃e�L�X�g��\������N���X
/// </summary>
[RequireComponent(typeof(TextMeshProUGUI))]
public class OverlayTalkText : MonoBehaviour, IUIComponent
{
    [SerializeField]
    private TextMeshProUGUI _tmp;

    public void Initialized()
    {
        _tmp.text = string.Empty;
    }
}
