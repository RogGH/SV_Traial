using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// �G�ɐݒ肷��^�C�v
public enum DropType
{
    ExpS,      // A�i�o���l�������߁j
    ExpM,      // B�i�o���l�������߁j   
    ExpL,      // C�i�o���l�傪���߁j   
    Tresure,    // ��

    Num
};

// �I�u�W�F�N�g�ԍ�
public enum DropTblNo {
    ExpS,
    ExpM,
    ExpL,
    MoneyS,
    HealS,
    Tresure,

    Num
};


public class DropManager : MonoBehaviour
{
    private static DropManager ins;
    public static DropManager Ins
    {
        get
        {
            if (ins == null)
            {
                ins = (DropManager)FindObjectOfType(typeof(DropManager));
                if (ins == null) { Debug.LogError(typeof(DropManager) + "is nothing"); }
            }
            return ins;
        }
    }

    public GameObject[] ItemObjTbl;

    public const int MoneyRate = 10;
    public const int HealItemRate = 1;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void BootDropItem(Vector3 pos, DropType dType)
    {
        switch (dType) {
            case DropType.ExpS:
                {
                    // �A�C�e���N��
                    Instantiate(ItemObjTbl[(int)DropTblNo.ExpS], pos, Quaternion.identity);
                    // ���̑��h���b�v�͊�{�ʂ�
                    otherItemDropDefault(pos);
                }
                break;

            case DropType.ExpM:
                {
                    // �A�C�e���N��
                    Instantiate(ItemObjTbl[(int)DropTblNo.ExpM], pos, Quaternion.identity);

                    // ���̑��h���b�v�͊�{�ʂ�
                    otherItemDropDefault(pos);
                }
                break;

            case DropType.ExpL:
                {
                    // �A�C�e���N��
                    Instantiate(ItemObjTbl[(int)DropTblNo.ExpL], pos, Quaternion.identity);
                    // ���̑��h���b�v�͊�{�ʂ�
                    otherItemDropDefault(pos);
                }
                break;

            case DropType.Tresure:
                {
                    // �A�C�e���N��
                    Instantiate(ItemObjTbl[(int)DropTblNo.Tresure], pos, Quaternion.identity);
                }
                break;
        }
    }

    // ���̑��A�C�e���h���b�v
    void otherItemDropDefault(Vector3 pos) {
        // �����_���Œǉ��ŗ��Ƃ��A�C�e����ݒ�
        Player plScr = StageManager.Ins.PlScr;

        int luck = plScr.GetCalcLuck();
        float calcRate = luck / 100;

        int rand = Random.Range(0, 100);
        int moneyDrop = Mathf.RoundToInt(MoneyRate * calcRate);
        int healDrop = moneyDrop + Mathf.RoundToInt(HealItemRate * calcRate);
        if (rand < moneyDrop)
        {
            // ���𗎂Ƃ����`�F�b�N
            pos.x += Random.Range(-3.0f, 3.0f) * 10;
            Instantiate(ItemObjTbl[(int)DropTblNo.MoneyS], pos, Quaternion.identity);
        }
        else if (rand < healDrop)
        {
            // �񕜃A�C�e�������Ƃ�
            pos.x += Random.Range(-3.0f, 3.0f) * 10;
            Instantiate(ItemObjTbl[(int)DropTblNo.HealS], pos, Quaternion.identity);
        }
    }
}
