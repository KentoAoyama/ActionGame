using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Domain;

namespace Presentation
{
    /// <summary>
    /// オーバーレイ表示のUIを管理するクラス
    /// </summary>
    public class PrepareGUI : MonoBehaviour, IPrepareGUI
    {
        [SerializeField]
        private TalkGUI _overlayTalkUI;


        private void Awake()
        {
            _overlayTalkUI.Initialized();
        }
    }
}