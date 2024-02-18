using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Presentation
{
    public class BattleCanvas : MonoBehaviour
    {
        [SerializeField]
        private CrosshairController _crosshairController;

        [SerializeField]
        private EnemyTargetAreas _enemyTargetAreas;

        public void Initialized()
        {
            _crosshairController.Initialized();
            _enemyTargetAreas.Initialized();
        }
    }
}
