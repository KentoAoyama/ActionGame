using UnityEngine;
using Domain;

namespace Presentation
{
    public class BattleCanvas : MonoBehaviour, IBattleCanvasService
    {
        [SerializeField]
        private CrosshairController _crosshairController;

        public void Initialized()
        {
            _crosshairController.Initialized();
        }
    }
}
