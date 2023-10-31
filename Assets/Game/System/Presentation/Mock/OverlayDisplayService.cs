using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverlayDisplayService : MonoBehaviour, IOverlayDisplayService
{
    [SerializeField]
    private OverlayTalkUI _overlayTalkUI;


    private void Awake()
    {
        _overlayTalkUI.Initialized();
    }
}
