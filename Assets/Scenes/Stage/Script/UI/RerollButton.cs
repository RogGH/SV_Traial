using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RerollButton : MonoBehaviour
{
    public LevelUpManager LvupMng;
    Player plScr;

    void Start()
    {
        plScr = StageManager.Ins.PlScr;
    }

    void Update()
    {
    }

    // �����[��
    public void PushRerollButton()
    {
        // �M���m�F
        if (plScr.Money >= ItemDefine.ReRollCost) {
            // �Đݒ�
            LvupMng.SetUp();
            // �M������
            plScr.Money -= ItemDefine.ReRollCost;

            SeManager.Instance.Play("Buy");
        }
        else
        {
            // �M���s��
            SeManager.Instance.Play("Beap");
        }
    }


}
