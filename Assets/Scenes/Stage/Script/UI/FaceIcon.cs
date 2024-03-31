using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FaceIcon : MonoBehaviour
{
    public Sprite[] spIconTbl;

    void Awake()
    {
        // �X�v���C�g�ύX
        int selCharNo = (int)SystemManager.Ins.selCharNo;

        // �x��
        if (spIconTbl.Length != (int)CharNo.Num)
        {
            Debug.LogError("palyer animator tbl num is different" + "tbl length = "
                + spIconTbl.Length + "charNum = " + CharNo.Num);
        }

        Image imgCmp = GetComponent<Image>();
        imgCmp.sprite = spIconTbl[(int)selCharNo];
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
