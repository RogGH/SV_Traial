using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class PLShellShot
{
    // ���x�ݒ�
    public void SetSpeed()
    {
        // �����ɂ����x��ݒ肷��
        velocity.x = moveSpeed * Mathf.Cos(radian);
        // �����ɂ����x��ݒ肷��
        velocity.y = moveSpeed * Mathf.Sin(radian);
    }
}
