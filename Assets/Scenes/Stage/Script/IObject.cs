using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �C���^�[�t�F�[�X�I�ȃN���X
public class IObject
{
    // �X�v���C�g�̗D��֘A
    // ��O�ɏo��n�̃G�t�F�N�g�@1000
    // �z�u�� 720
    //  PL�@0
    // �z�u�� -720
    // �\�� -1000
    // �w�i -2000
    // 
    // �z�u���̓V�F����G�t�F�N�g����O�ɗ���悤�ɒl���{���Ă���
    // 

    public static int GetSpriteOrder(Vector3 pos)
    {
        // ���݂̍��W����A�\���D������肳���Ă݂�
        Vector3 camPos = Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, Camera.main.nearClipPlane));
        float distY = camPos.y - pos.y;
        return (int)(Mathf.Clamp(distY, -720, 720));
    }
}
