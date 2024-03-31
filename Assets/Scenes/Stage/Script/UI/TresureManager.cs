using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TresureManager : MonoBehaviour
{
    public GameObject openObj;
    public GameObject getButObj;
    public LevelUpManager levelUpScr;
    public LevelUpChoiceButton butScr;
    public int ChoiceNo;
    public GameObject cancelButton;
    public GameObject TresureImage;

    Player plScr;

    void Start()
    {
        plScr = StageManager.Ins.PlScr;
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void SetUp()
    {
        // �A�N�e�B�u��
        gameObject.SetActive(true);
        openObj.SetActive(true);
        getButObj.SetActive(false);
        cancelButton.SetActive(false);

        TresureImage.GetComponent<TresureImage>().TresureClose();
        SeManager.Instance.Play("TresureScreen");
    }

    public void OpenButton()
    {
        openObj.SetActive(false);
        getButObj.SetActive(true);
        cancelButton.SetActive(true);

        TresureImage.GetComponent<TresureImage>().TresureOpen();

        SeManager.Instance.StopImmediately("TresureScreen");
        SeManager.Instance.Play("TresureOpen");


        // ������A�C�e�������Ȃ���
        if (plScr.CheckWeaponLevelAllMax() && plScr.CheckItemLevelAllMax())
        {
            // ��
            // �{�^���ݒ�
            ChoiceNo = (int)IconNo.Money;
            // �{�^���֘A�̃A�b�v�f�[�g
            butScr.ImageUpdate(0);
        }
        else
        {
            // �I���\���X�g���쐬
            List<int> randList = levelUpScr.CreateList();

            // �Ƃ肠�����P��
            for (int choiceNo = 0; choiceNo < 1; ++choiceNo)
            {
                // �I�����ݒ�
                if (randList.Count > 0)
                {
                    int rand = Random.Range(0, randList.Count);
                    int iconNo = randList[rand];
                    int level = plScr.GetEquipLevel(iconNo);

                    // �{�^���ݒ�
                    ChoiceNo = iconNo;
                    // �{�^���֘A�̃A�b�v�f�[�g
                    butScr.ImageUpdate(level);
                    // ���X�g����폜
                    randList.Remove(iconNo);
                }
            }
        }
    }
}
