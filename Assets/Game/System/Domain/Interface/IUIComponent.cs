using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Domain
{
    public interface IUIComponent
    {
        /// <summary>
        /// シーン読み込み時に１度だけ呼び出される
        /// </summary>
        void Initialized();
    }
}