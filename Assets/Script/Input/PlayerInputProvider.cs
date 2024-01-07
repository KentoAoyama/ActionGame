using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputProvider : IInputProvider
{
    public Vector2 GetMoveDir()
    {
        Vector2 inputDir = new();
        inputDir.x = Input.GetAxisRaw("Horizontal");
        inputDir.y = Input.GetAxisRaw("Vertical");
        
        // 何かしら入力があればログを出力
        if (inputDir != Vector2.zero)
        {
            Debug.Log("入力されたよ");
        }

        return inputDir;
    }

    public bool GetFire()
    {
        // 何かしら入力があればログを出力
        if (Input.GetButton("Fire1"))
        {
            Debug.Log("攻撃入力されたよ");
        }

        return Input.GetButton("Fire1");
    }
}
