using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// オーバーレイ表示の会話UIを管理するクラス
/// </summary>
public class OverlayTalkUI : MonoBehaviour
{
    [SerializeField]
    private OverlayTalkText _upperTalkText;

    [SerializeField]
    private OverlayTalkText _lowerTalkText;

    public void Initialized()
    {
        _upperTalkText.Initialized();
        _lowerTalkText.Initialized();
    }
}
