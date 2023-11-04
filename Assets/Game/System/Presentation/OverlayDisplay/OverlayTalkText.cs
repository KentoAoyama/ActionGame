using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

/// <summary>
/// オーバーレイ表示のテキストを表示するクラス
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
