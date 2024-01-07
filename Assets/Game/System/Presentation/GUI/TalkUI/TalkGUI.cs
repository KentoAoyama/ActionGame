using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// �I�[�o�[���C�\���̉�bUI���Ǘ�����N���X
/// </summary>
public class TalkGUI : MonoBehaviour
{
    [SerializeField]
    private TalkGUIText _upperTalkText;

    [SerializeField]
    private TalkGUIText _lowerTalkText;

    public void Initialized()
    {
        _upperTalkText.Initialized();
        _lowerTalkText.Initialized();
    }
}
