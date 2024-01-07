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
        
        // ����������͂�����΃��O���o��
        if (inputDir != Vector2.zero)
        {
            Debug.Log("���͂��ꂽ��");
        }

        return inputDir;
    }

    public bool GetFire()
    {
        // ����������͂�����΃��O���o��
        if (Input.GetButton("Fire1"))
        {
            Debug.Log("�U�����͂��ꂽ��");
        }

        return Input.GetButton("Fire1");
    }
}
