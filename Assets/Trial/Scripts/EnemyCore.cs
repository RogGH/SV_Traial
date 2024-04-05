using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class EnemyCmm
{
    // �G�̓����ݒ肷��
    public void EnemyMoveControl()
    {
        // PL�ƓG�Ƃ̋������v�Z
        float distX = plObj.transform.position.x - transform.position.x;
        float distY = plObj.transform.position.y - transform.position.y;

        float radian = 0;
        // �����Ƀv���C���[�ւ̊p�x���v�Z���鏈�����L��
        radian = Mathf.Atan2(distY, distX);

        // �ړ����x��ݒ�
        if (radian != 0) 
        {
            moveVec.x = moveSpd * Mathf.Cos(radian);
            moveVec.y = moveSpd * Mathf.Sin(radian);
        }

        // �ړ�����
        transform.position += moveVec * Time.deltaTime;
    }
}
