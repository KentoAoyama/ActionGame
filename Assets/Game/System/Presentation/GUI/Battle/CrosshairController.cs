using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Presentation
{
    public class CrosshairController : MonoBehaviour
    {
        [SerializeField]
        private Image _crosshair;

        public void Initialized()
        {
            _crosshair.gameObject.SetActive(true);
        }
    }
}
