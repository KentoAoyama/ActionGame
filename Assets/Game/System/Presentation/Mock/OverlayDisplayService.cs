using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// オーバーレイ表示のUIを管理するクラス
/// </summary>
public class OverlayDisplayService : MonoBehaviour, IOverlayDisplayService
{
    [SerializeField]
    private OverlayTalkUI _overlayTalkUI;


    private void Awake()
    {
        _overlayTalkUI.Initialized();
    }
}
