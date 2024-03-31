using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleButton : MonoBehaviour
{
    public TitleManager titleMngScr;
    public OptionManager optMngScr;
    public ShopManager shopMngScr;

    void Start()
    {
    }

    void Update()
    {        
    }

    // �X�^�[�g�{�^���i�L�����Z���j
    public void OnClickStart()
    {
        if (TitleDebugManager.Ins.trialVersion == true) {
            SeManager.Instance.Play("TitleDeside");
            FadeManager.Instance.LoadScene("Stage", 1.0f);
            BgmManager.Instance.Stop();

            Button btn = GetComponent<Button>();
            btn.interactable = false;

            // �X�e�[�W��ݒ�
            SystemManager.Ins.selCharNo = (CharNo)0;
            SystemManager.Ins.selStgNo = (StageNo)0;
        }
        else {
            SeManager.Instance.Play("Button1");
            titleMngScr.charSel.SetActive(true);
        }
    }
    // �L�����Z��
    public void OnClickCharaSelDeside() {
        SeManager.Instance.Play("Button1");
        titleMngScr.stageSel.SetActive(true);
        titleMngScr.charSel.SetActive(false);
        // �L�����N�^�[��ݒ�
        SystemManager.Ins.selCharNo = (CharNo)transform.GetSiblingIndex();
    }
    public void OnClickCharaSelBack()
    {
        SeManager.Instance.Play("Button1");
        titleMngScr.charSel.SetActive(false);
    }

    // �X�e�[�W�Z���N�g
    public void OnClickStageSelDeside() {
        SeManager.Instance.Play("TitleDeside");
        FadeManager.Instance.LoadScene("Stage", 1.0f);
        BgmManager.Instance.Stop();

        Button btn = GetComponent<Button>();
        btn.interactable = false;

        // �X�e�[�W��ݒ�
        if (TitleDebugManager.Ins.trialVersion == true)
        {
            SystemManager.Ins.selStgNo = StageNo.Trial;
        }
        else {
            SystemManager.Ins.selStgNo = (StageNo)transform.GetSiblingIndex();
        }
    }
    public void OnClickStageSelBack() {
        SeManager.Instance.Play("Button1");
        titleMngScr.stageSel.SetActive(false);
        titleMngScr.charSel.SetActive(true);
    }


    // �I�v�V�����ɓ���
    public void OnClickOption()
    {
        SeManager.Instance.Play("Button1");
        titleMngScr.option.SetActive(true);
        optMngScr.SetPara();
        titleMngScr.titleButGrp.SetActive(false);
    }
    // �I�v�V��������߂�
    public void OnClickOptionBack()
    {
        SeManager.Instance.Play("Button1");
        optMngScr.ExitOptoin();                 // �I�v�V��������
        titleMngScr.option.SetActive(false);    // ��ʏ���
        titleMngScr.titleButGrp.SetActive(true);
    }

    // �V���b�v��
    public void OnClickShop()
    {
        SeManager.Instance.Play("Button1");
        titleMngScr.shop.SetActive(true);
        shopMngScr.SetPara();
        titleMngScr.titleButGrp.SetActive(false);
    }
    // �V���b�v����߂�
    public void OnClickShopBack()
    {
        // �V���b�v��ʂ���߂�
        SeManager.Instance.Play("Button1");

        // �V���b�v�I�𒆂��`�F�b�N
        if ( titleMngScr.shopButGrp.activeInHierarchy )
        {
            // �V���b�v���猳�̉�ʂ�
            shopMngScr.ExitShop();                 // �V���b�v����
            titleMngScr.shop.SetActive(false);      // ��ʏ���
            titleMngScr.titleButGrp.SetActive(true);
        }
        else {
            // �V���b�v�̂���ɉ��Ȃ̂ŁA�V���b�v�I���ɖ߂�
            titleMngScr.shopButGrp.SetActive(true);
            titleMngScr.charPanel.SetActive(false);
            titleMngScr.wepPanel.SetActive(false);
            titleMngScr.itemPanel.SetActive(false);

            // �Ƃ肠���������ŃZ�[�u
            SystemManager.Ins.SaveSystemData();
        }
    }

    public void OnClickExit()
    {
        SeManager.Instance.Play("Button1");
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;//�Q�[���v���C�I��
#else
        Application.Quit();//�Q�[���v���C�I��
#endif
    }


}
