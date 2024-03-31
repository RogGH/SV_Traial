using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatusText : MonoBehaviour
{
    static int no;

    string[] strTbl = new string[] {
        "�ő�HP",
        "������",
        "�h���",
        "�ړ����x",
        "���W�͈�",
        "�o���l",
        "��",
        "�^",
        "�^�_���[�W",
        "�N���e�B�J����",
        "�U������",
        "�e��",
        "�U���͈�",
        "�e��",
        "���L���X�g�Z�k",
    };
    Text textCmp;
    Player plScr;

    void Start()
    {
        textCmp = GetComponent<Text>();
        plScr = StageManager.Ins.PlScr;
    }

    void Update()
    {
        no = transform.GetSiblingIndex();
        textCmp.text = strTbl[no];
        switch (no)
        {
            case (int)IconNo.MaxHPUp:
                textCmp.text += ":" + (100 + plScr.MaxHpRate) + "%";
                break;
            case (int)IconNo.AutoHealUp:
                textCmp.text += ":" + plScr.AutoHealValue;
                break;
            case (int)IconNo.DefenseUp:
                textCmp.text += ":" + plScr.DefPow;
                break;
            case (int)IconNo.MoveSpdUp:
                textCmp.text += ":" + (100 + plScr.MoveSpdRate) + "%";
                break;
            case (int)IconNo.MagnetUp:
                textCmp.text += ":" + (100 + plScr.MagnetRate) + "%";
                break;
            case (int)IconNo.ExpUp:
                textCmp.text += ":" + (100 + plScr.ExpRate) + "%";
                break;
            case (int)IconNo.GoldUp:
                textCmp.text += ":" + (100 + plScr.MoneyRate) + "%";
                break;
            case (int)IconNo.LuckUp:
                textCmp.text += ":" + (100 + plScr.LuckRate) + "%";
                break;

            case (int)IconNo.DamageUp:
                textCmp.text += ":" + (100 + plScr.DamageRate) + "%";
                break;
            case (int)IconNo.CriticalUp:
                textCmp.text += ":" + (plScr.CriHitRate) + "%";
                break;
            case (int)IconNo.AtkTimeUp:
                textCmp.text += ":" + (100 + plScr.AtkTimeRate) + "%";
                break;
            case (int)IconNo.ShotNumUp:
                textCmp.text += ":" + plScr.AddShotNum;
                break;
            case (int)IconNo.AtkAreaUp:
                textCmp.text += ":" + (100 + plScr.AtkAreaRate) + "%";
                break;
            case (int)IconNo.AtkMoveSpdUp:
                textCmp.text += ":" + (100 + plScr.AtkMoveRate) + "%";
                break;
            case (int)IconNo.RecastUp:
                textCmp.text += ":" + (100 + plScr.RecastRate) + "%";
                break;
        }
    }
}
