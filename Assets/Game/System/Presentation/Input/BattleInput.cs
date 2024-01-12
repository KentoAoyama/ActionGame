using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Domain;

namespace Presentation
{
    public class BattleInput : IBattleInput
    {
        public Vector2 GetMoveDir()
        {
            Vector2 inputDir = new()
            {
                x = Input.GetAxisRaw("Horizontal"),
                y = Input.GetAxisRaw("Vertical")
            };

            return inputDir;
        }

        public bool GetFire()
        {
            return Input.GetButton("Fire1");
        }
    }
}
