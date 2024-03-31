using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �A�C�R���ԍ�
public enum IconNo
{
    // �A�C�e��
    ItemTop,
    MaxHPUp = ItemTop,      // �ő�HP�A�b�v
    AutoHealUp,             // �����񕜃A�b�v
    DefenseUp,              // �h��̓A�b�v
    MoveSpdUp,              // �ړ����x�A�b�v

    MagnetUp,               // ���W�͈̓A�b�v
    ExpUp,                  // �o���l�A�b�v
    GoldUp,                 // ���A�b�v
    LuckUp,                 // �^�A�b�v

    DamageUp,               // �^�_���[�W�A�b�v    
    CriticalUp,             // �N���e�B�J���A�b�v
    AtkTimeUp,              // �U�����ԃA�b�v
    ShotNumUp,              // �e���A�b�v

    AtkAreaUp,              // �U���͈̓A�b�v
    AtkMoveSpdUp,           // �e���A�b�v
    RecastUp,               // ���L���X�g�A�b�v

    ItemEnd,
    ItemNum = ItemEnd - ItemTop,


    // ����
    WeaponTop = ItemEnd,
    MShot = WeaponTop,      // �V���b�g
    MDrill,                 // �h����
    MSaw,                   // ��]�̂�����
    MFrameThrower,          // �Ή�����
    MTurret,                // �^���b�g

    // �I���W�i���F
    TVWeaponPima,           // �҂܁F���m

    WeaponEnd,
    WeaponNum = WeaponEnd - WeaponTop,

    // �擾
    UseItemTop = WeaponEnd,
    Money = UseItemTop,     // ��
    Potion,                 // �|�[�V����

    UseItemEnd,
    UseItemNum = UseItemEnd - UseItemTop,


    // ���̑���`
    LevelUpItemNum = ItemNum + WeaponNum,


    Invalid = -1,
    end
};


// �C���[�W�Ǘ�
public class ImageManager : MonoBehaviour
{
    private static ImageManager ins;
    public static ImageManager Ins
    {
        get
        {
            if (ins == null)
            {
                ins = (ImageManager)FindObjectOfType(typeof(ImageManager));
                if (ins == null) { Debug.LogError(typeof(ImageManager) + "is nothing"); }
            }
            return ins;
        }
    }

    public Sprite[] iconTbl;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }



    // �A�C�R���ԍ����畐�킩�`�F�b�N
    public bool CheckIconNoIsWeapon(int iNo)
    {
        if ((int)IconNo.WeaponTop <= iNo && iNo < (int)IconNo.WeaponEnd) { return true; }
        return false;
    }
    // �A�C�R���ԍ����h���b�v�A�C�e�����`�F�b�N
    public bool CheckIconNoIsUseItem(int iNo)
    {
        if ((int)IconNo.UseItemTop <= iNo && iNo < (int)IconNo.UseItemEnd) { return true; }
        return false;
    }


    // �A�C�R���ԍ����畐��ԍ��i�A�ԁj��
    public int ConvIconNoToWeaponSerialNo(int iNo)
    {
        if (CheckIconNoIsWeapon(iNo)) { 
            return (iNo - (int)IconNo.WeaponTop);
        }
        return (int)IconNo.Invalid;
    }

    // �A�C�e���A�Ԃ���A�C�R���ԍ���
    public int ConvItemSerialNoToIconNo(int iNo) {
        return iNo;
    }

    // ����A�Ԃ���A�C�R���ԍ���
    public int ConvWeaponSerialNoToIconNo(int wsNo)
    {
        return wsNo + (int)IconNo.WeaponTop;
    }


    // �X�v���C�g�擾
    public Sprite GetSprite(int no)
    {
        if (iconTbl.Length < no) {
            Debug.Log("Error!! iconLength = " + iconTbl.Length + ": no = " + no);
        }
        return iconTbl[no];
    }
}
