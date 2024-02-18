using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Presentation
{
    public class BattleSystems : MonoBehaviour
    {
        [SerializeField]
        private EnemysController _enemysController;

        [SerializeField]
        private BattleCanvas _battleCanvas;

        private void Start()
        {
            _enemysController.Initialized();
            _battleCanvas.Initialized();
        }
    }
}