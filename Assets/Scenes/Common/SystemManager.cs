using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �Z�[�u�f�[�^
[System.Serializable]
public class SaveData {
    public int money = 1000;
    public float seVol = 0.5f;
    public float bgmVol = 0.5f;
    public Lang lang = Lang.Japanese;       // ���{��Œ�
};

// ����v���C���[�v���t�@�X�ł̎���
// �Í����͂܂�
// �����Ƃ���Edit>Clear All PlayerPrefs


public class SystemManager : MonoBehaviour
{
    // �V���O���g������
    private static SystemManager ins;
    public static SystemManager Ins
    {
        get {
            if (ins == null) {
                ins = (SystemManager)FindObjectOfType(typeof(SystemManager));
                if (ins == null) {
                    Debug.LogError(typeof(SystemManager) + "is nothing");
                }
            }
            return ins;
        }
    }

    // �Z�[�u�f�[�^
    public SaveData sData;

    // �Q�[���p
    public StageNo selStgNo;
    public CharNo selCharNo;


    // �v���C���[�v���t�@�X�̃L�[
   const string SaveKey = "SaveJsonKey";

    private void Awake()
    {
        if (this != Ins) {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
    }

    void Update()
    {
    }

    // �V�X�e���f�[�^���[�h
    void LoadSystemData(string json)
    {
    }


    public void SaveSystemData()
    {
    }
}
