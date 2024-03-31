using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageSelButton : MonoBehaviour
{
    string[] stageNameTbl = {
       "�ؐl���Ő�",
       "�^�C�^�����Ő�",
        "�K���[�_���Ő�",
        "�C�t���[�g���Ő�",
        "�A���e�}�E�F�|���j����",
    };
    Text childText;

    void Start()
    {
        int butNo = transform.GetSiblingIndex();

        childText = GetComponentInChildren<Text>();
        childText.text = stageNameTbl[butNo];
    }

    void Update()
    {        
    }
}
