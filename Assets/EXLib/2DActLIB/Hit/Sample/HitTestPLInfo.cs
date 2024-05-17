using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HitTestPLInfo : MonoBehaviour
{
    public GameObject pl;
    PLHitTest plScr;
    HitBase plHB;
    Text texComp;

    void Start()
    {
        texComp = GetComponent<Text>();
        plScr = pl.GetComponent<PLHitTest>();
        plHB = pl.GetComponent<HitBase>();
    }

    void Update()
    {
        if (pl == null) {
            texComp.text = "PL���S";
            return;
        }

        texComp.text = "PLHP:" + plScr.getHitBase().HP;

        // �`�F�b�N�p����Ɏ󂯂�
        if (plHB.CheckDefCheckOnly() ) {
            texComp.text += "\nPL�`�F�b�N�p����ɍU�����󂯂�";
        }
        // �ڐG�͂��Ă���
        if (plHB.CheckDefHit() )
        {
            texComp.text += "\n�h��ōU���ƐڐG����";
        }
        // �_���[�W���󂯂�
        if (plHB.CheckDamage())
        {
            texComp.text += "\n�_���[�W���󂯂�";
        }
    }
}
