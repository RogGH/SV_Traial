using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ���큄LV4�ŋ����ALV7�Œ������Ȃ�悤�ɂ��邩
// 
// DPS�`�F�b�N
// �n���h�K��LV7�@     150
// �h����LV7           100
// �̂�����LV7         100      
// �Ή�LV7             100
// �^���b�gLV7          85
// �g��LV7             100
//
// ���z�����i�U���A�N���A�Z�k�͊�{
// HG     1000  (�e���r���h�����o������j
// �ǂ�   350     
// �̂�   500
// �Ή�   550   (�e������ŃR�����c)
// �^��   400
// �g��   300

#if false
// ����̃��x���A�b�v�ɂ��ω�
public class WeaponPara {
    int shotNum = 1;                // �e��
    float atkRate = 1.0f;           // �U���̓��[�g�i1�����{�j
    int criHitRate = 0;             // �N���e�B�J���i���w��j
    int criDmgRate = 0;             // �N���e�B�J���_���[�W�i���w��j
    float recastRate = 1.0f;        // 0����1.0f��������ۂ��i0.5����50%�Z�k���ۂ��j
    float areaRate = 1.0f;          // 1.0f�ȏ�̔{���w�肷����ۂ�
    float atkTimeRate = 1.0f;       // �U�����ԃ��[�g
    float moveSpdRate = 1.0f;       // �e��
};
#endif

public class WeaponManager : MonoBehaviour
{
    private static WeaponManager ins;
    public static WeaponManager Ins
    {
        get
        {
            if (ins == null)
            {
                ins = (WeaponManager)FindObjectOfType(typeof(WeaponManager));
                if (ins == null) { Debug.LogError(typeof(WeaponManager) + "is nothing"); }
            }
            return ins;
        }
    }

    public GameObject[] WeaponObjTbl;                   // �N���p�Q�[���I�u�W�F�N�g

    public WeaponInitSOBJDataTable initDataTable;
    public WeaponLVUPSOBJDataTable lvupDataTable;

    public WeaponInitSOBJDataTable InitTable { get { return initDataTable; } }
    public WeaponLVUPSOBJDataTable LVUPTable{ get { return lvupDataTable; } }

    public GunSOBJ gunSobj;

    private void Awake()
    {
        Debug.Assert(!(initDataTable == null), "initDataTable is null");
        Debug.Assert(!(lvupDataTable == null), "lvupDataTable is null");
    }


    // ���L���X�g�f�t�H���g���Ԏ擾
    public float GetWeaponRecastDef(int sNo)
    {
        return initDataTable.data[sNo].recastDef;
    }
    public float GetWeaponRecastMin(int sNo)
    {
        return initDataTable.data[sNo].recastMin;
    }
    public float GetWeaponRecastCalc(int sNo) {
        return GetWeaponRecastDef(sNo) - GetWeaponRecastMin(sNo);
    }
}
