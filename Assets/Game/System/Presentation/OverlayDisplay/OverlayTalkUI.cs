using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
