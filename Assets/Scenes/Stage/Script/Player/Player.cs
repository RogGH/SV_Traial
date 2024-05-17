using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Player : MonoBehaviour
{
    int totalExp = 0;
    int exp = 0;
    public int Exp {
        get { return exp; }
        set { exp = value; }
    }

    int nextExpTR = 20;
    int nextExp = 50;
    public int NextExp
    {
        get {
            return TrialVerision ? nextExpTR : nextExp;
        }
        set {
            if (TrialVerision) { nextExpTR = value; }
            else { nextExp = value; }
       }
    }

    int level = 1;
    public int Level {
        get { return level; }
        set { level = value; }
    }

    int money = 0;
    public int Money {
        get { return money; }
        set { money = value; }
    }

    int killNum = 0;
    public int KillNum {
        get { return killNum; }
        set { killNum = value; }
    }

    // �p�u���b�N�̃R���|�[�l���g
    public HitDefData defData;
    // �R���|�[�l���g
    HitBase hb;
    SpriteRenderer spComp;

    // �̌��o�[�W�������`�F�b�N
    [Header("�̌��Ńo�[�W�����t���O")]
    public bool TrialVerision = true;


    // �A�r���e�B�����i���x����
    const float AB_MaxHPRate = 20;
    const int AB_AutoHeal = 1;
    const int AB_DefPow = 3;
    const float AB_MoveSpdRate = 5;
    const float AB_MagnetRate = 100;
    const float AB_ExpRate = 20;
    const float AB_MoneyRate = 20;
    const float AB_LuckRate = 50;
    const float AB_DmgRate = 10;
    const float AB_CriHitRate = 5;
    const float AB_AtkTimeRate = 10;
    const float AB_AtkAreaRate = 10;
    const float AB_AtkMoveRate = 10;
    const float AB_RecastRate = 10;

    // �����p�p�����[�^
    const float LevelUpRateTR = 1.1f;    // ���x���A�b�v�̎w���I�Ȃ��
    const float LevelUpRate = 1.25f;    // ���x���A�b�v�̎w���I�Ȃ��
    const float MoveSpeed = 2.4f;       // ��{�ړ����x
    const float DieCounter = 3.0f;      // ���S�ҋ@
    const float AutoHealCount = 3.0f;   // �����񕜂܂ł̎���
    const float MagnetRange = 60.0f;    // ���W�͈�


    // %����1.0+Rate�̔{���ɕϊ�
    // ��@value = 1, rate = 50(rate�͉��Z���鐔�l�ŁA50�������150���Ōv�Z�����j
    public float ConvRateToValue(float rate) { return 1 + (rate / 100); }
    public float CalcRateToValue(float value, float rate) { return value * ConvRateToValue(rate); }

    // ����֘A�܂Ƃ�
    [System.Serializable]
    public class Weapon {
        public Weapon(int iNo, int lv) {
            iconNo = iNo;
            serialNo = ImageManager.Ins.ConvIconNoToWeaponSerialNo(iNo);
            recastTime = WeaponManager.Ins.GetWeaponRecastDef(serialNo);
            recastCounter = 1.0f;
            level = lv;
        }
        public bool initFlag = false;                                   // �������t���O 
        public int iconNo;                                              // �A�C�R���ԍ�
        public int serialNo;                                            // �ʂ��ԍ�
        public int level;                                               // ���x��
        public float recastCounter;                                     // ���L���X�g�p�J�E���^
        public float recastTime;                                        // ���L���X�g����
        public float recastRate = 0;                                    // ���L���X�g�̃��[�g
        public float atkRate = 0;                                       // �U���͂̃��[�g
        public float areaRate = 0;                                      // �U���͈͂̃��[�g
        public int criHitRate = 0;                                      // �N���e�B�J�����[�g
        public int criDmgRate = 0;                                      // �N���e�B�J���_���[�W
        public float speedRate = 0;                                     // ���x���[�g
        public float timeRate = 0;                                      // ���ԃ��[�g
        public int atkNum = 1;                                          // �e��
        public bool flag1 = false;                                      // �t���O
        public bool flag2 = false;                                      // �t���O
        public bool flag3 = false;                                      // �t���O
        public bool flag4 = false;                                      // �t���O
    };
    const int weaponTblNum = 6;
    public Weapon[] WeaponTbl = new Weapon[weaponTblNum];
    int wEquipNum = 0;
    public int WeaponEquipNum { get { return wEquipNum; } }

    // �����A�C�e���܂Ƃ�
    public class Item {
        public Item(int iNo, int lv) {
            iconNo = iNo; level = lv;
        }
        public int iconNo;
        public int level;
    }
    const int itemTblNum = 6;
    public Item[] ItemTbl = new Item[itemTblNum];
    int iEquipNum = 0;
    public int ItemEquipNum { get { return iEquipNum; } }


    // ����֘A�܂Ƃ�]
    [System.Serializable]
    public class Ability
    {
        public float hpRate = 0f;
        public int autoHealValue = 0;
        public int defPow = 0;
        public float moveSpdRate = 0;
        public float magnetRate = 0;
        public float expRate = 0;
        public float moneyRate = 0;
        public float luckRate = 0;
        public float dmgRate = 0;
        public float criHitRate = 0;
        public float criDmgRate = 0;
        public float atkTimeRate = 0;
        public float atkAreaRate = 0;
        public float atkMoveRate = 0;
        public float recastRate = 0;
        public int addShotNum = 0;
    }
    public Ability ability = new Ability();

    public float MaxHpRate { get { return ability.hpRate; } }
    public int AutoHealValue { get { return ability.autoHealValue; } }
    public int DefPow { get { return ability.defPow; } }
    public float MoveSpdRate { get { return ability.moveSpdRate; } }
    public float MagnetRate { get { return ability.magnetRate; } }
    public float ExpRate { get { return ability.expRate; } }
    public float MoneyRate { get { return ability.moneyRate; } }
    public float LuckRate { get { return ability.luckRate; } }
    public float DamageRate { get { return ability.dmgRate; } }
    public float CriHitRate { get { return ability.criHitRate; } }
    public float AtkTimeRate { get { return ability.atkTimeRate; } }
    public int AddShotNum { get { return ability.addShotNum; } }
    public float AtkAreaRate { get { return ability.atkAreaRate; } }
    public float AtkMoveRate { get { return ability.atkMoveRate; } }
    public float RecastRate { get { return ability.recastRate; } }

    // �v���C���[�̋����p�����[�^
    int initHP = 50;
    int initLuck = 100;
    float magnet = MagnetRange;
    public float Magnet { get { return magnet; } }
    float autoHealCounter = AutoHealCount;
    public bool DieFlag = false;

    // �ėp�p�����[�^
    float fctr0 = 0;
    //float ictr0 = 0;


    void Awake()
    {
        hb = GetComponent<HitBase>();
        hb.MaxHP = initHP;

        spComp = GetComponent<SpriteRenderer>();

        AnimInit();
    }

    void Start()
    {
        animator.Play("Idle");
        // �Ƃ肠�����V���b�g��ݒ�
        SetWeaponTbl(0, (int)IconNo.MShot, 1);
    }

    void Update()
    {
        if (StageManager.Ins.CheckStop()) { return; }

        hb.PreUpdate();
        if (hb.CheckDie()) {
            if (DieFlag == false) {
                DieFlag = true;
                Vector3 bootPos = transform.position;
                bootPos.y += 30.0f;
                Instantiate((GameObject)Resources.Load("Prefabs/Effect/EffDie"), bootPos, Quaternion.identity);
                hb.SetAtkActive(false);
                hb.SetDefActive(false);
                spComp.color = Color.clear;
                fctr0 = DieCounter;
                //ictr0 = 0;
                BgmManager.Instance.Stop();
                SeManager.Instance.Play("Die");
            }
        }

        if (DieFlag) {
            fctr0 -= Time.deltaTime;
            if (fctr0 <= 0) {
                SeManager.Instance.Play("GameOver");
                StageManager.Ins.SetGameOver();
            }
            return;
        }

        if (hb.CheckDefDamage()) {
            SeManager.Instance.Play("Dmg2", 0);
        }

        // �ړ��֘A
        if (TrialVerision)
        {
            playerCore();
        }
        else 
        {
            KeyInputUpdate();

            WeaponControl();
        }

        if (StageManager.Ins.DebugMode) {
            // L�V�t�g�{Z
            if (Input.GetKeyDown(KeyCode.Z) && Input.GetKey(KeyCode.LeftShift))
            {
                exp = NextExp;
                AddExp(1);
                AddMoney(100000);
            }
        }

        // �I�[�g�q�[��
        if (ability.autoHealValue > 0) {
            autoHealCounter -= Time.deltaTime;
            if (autoHealCounter <= 0) {
                HealHP(ability.autoHealValue);
                autoHealCounter = AutoHealCount;
            }
        }


        // ���C���I������
        hb.PostUpdate();
    }

    void ChangeAnim(AnimNo no)
    {
        if (animNo != no) {
            animator.Play(nameTbl[(int)no]);
            animNo = no;
        }
    }



    // PL����J�������ɂ��邩�`�F�b�N
    public bool CheckPLCameraIn(Vector3 chkPos) {
        if (transform.position.x - 640 < chkPos.x
        && transform.position.x + 640 > chkPos.x
        && transform.position.y - 360 < chkPos.y
        && transform.position.y + 360 > chkPos.y
        )
        {
            return true;
        }
        return false;
    }

    // �}�E�X���W
    public Vector3 GetPLMouseWorldPos() {
        // �}�E�X�|�C���^�̈ʒu��\��
        Vector3 mousePos = Input.mousePosition;
        // ���[���h���W�ϊ�
        return Camera.main.ScreenToWorldPoint(mousePos);
    }
    // �}�E�X�|�C���^�ւ̊p�x�i���W�A���l�j
    public float GetAngleToPLMouse()
    {
        Vector3 mousePos = GetPLMouseWorldPos();
        Vector3 basePos = transform.position;
        return Mathf.Atan2(mousePos.y - basePos.y, mousePos.x - basePos.x);
    }

    // �o���l���Z
    public bool AddExp(int value)
    {
        int addExp = (int)CalcRateToValue(value, ability.expRate);
        exp += addExp;
        totalExp += addExp;
        if (exp >= NextExp) {
            // �L�����o�X���\������Ă��Ȃ���΃��x�����オ��Ȃ�
            GameObject gCanvas = StageManager.Ins.BaseUI.gameObject.transform.root.gameObject;
            if (gCanvas.activeSelf) {
                level++;
                exp -= NextExp;
                // ���̌o���l�𑝂₷
                if ( TrialVerision ) { NextExp = (int)(NextExp * LevelUpRateTR); }
                else { NextExp = (int)(NextExp * LevelUpRate); }
                
                // ���x���A�b�v�ݒ�
                StageManager.Ins.SetLevelUp();
                // SE
                SeManager.Instance.Play("LevelUp");
            }
            return true;
        }
        return false;
    }
    // ���x���A�b�v�����p�������`�F�b�N
    public bool CheckLevelUp()
    {
        return AddExp(0);
    }

    // �����Z
    public void AddMoney(int value) {
        money += (int)CalcRateToValue(value, ability.moneyRate);
    }

    // HP��
    public void HealHP(int value) {
        hb.HP += value;
        if (hb.HP >= hb.MaxHP) { hb.HP = hb.MaxHP; }

        GameObject obj =
        Instantiate((GameObject)Resources.Load("HitDispText"), transform.position, Quaternion.identity);
        obj.GetComponent<HitDispText>().SetString(value.ToString(), Color.green);
    }

    // �^���擾
    public int GetCalcLuck() { return (int)CalcRateToValue(initLuck, ability.luckRate); }
    // ���x
    public float GetAtkTimeRate(float time) { return CalcRateToValue(time, ability.atkTimeRate); }

    // ����𑕔����Ă��邩�`�F�b�N
    // ��������Ă����瑕���ԍ��A����Ă��Ȃ���΁|�P��ԋp
    public int CheckWeaponEquip(int iNo) {
        for (int eNo = 0; eNo < wEquipNum; ++eNo) {
            if (WeaponTbl[eNo].initFlag == false)
            {
                return -1;
            }

            if (WeaponTbl[eNo].iconNo == iNo) {
                return eNo;
            }
        }
        return -1;
    }
    // �A�C�e���𑕔����Ă��邩�`�F�b�N
    // ��������Ă����瑕���ԍ��A����Ă��Ȃ���΁|�P��ԋp
    public int CheckItemEquip(int iNo)
    {
        for (int eNo = 0; eNo < iEquipNum; ++eNo)
        {
            if (ItemTbl[eNo].iconNo == iNo)
            {
                return eNo;
            }
        }
        return -1;
    }
    // �A�C�e���܂��͕���𑕔����Ă��邩�`�F�b�N
    public int CheckEquip(int iNo)
    {
        if (ImageManager.Ins.CheckIconNoIsWeapon(iNo))
        {
            return CheckWeaponEquip(iNo);
        }
        else
        {
            return CheckItemEquip(iNo);
        }
    }


    // ����ݒ�
    public void SetWeaponTbl(int eNo, int iNo, int lv)
    {
        WeaponTbl[eNo] = new Weapon(iNo, lv);
        WeaponTbl[eNo].initFlag = true;

        // �����p�����[�^�ݒ�
        int sNo = ImageManager.Ins.ConvIconNoToWeaponSerialNo(iNo);
        WeaponInitSOBJData initData = WeaponManager.Ins.InitTable.data[sNo];
        WeaponTbl[eNo].atkNum = initData.num;

        wEquipNum++;                              // ���������Z
    }
    // �A�C�e���ݒ�
    public void SetItemTbl(int eNo, int iNo, int lv)
    {
        ItemTbl[eNo] = new Item(iNo, lv);
        ItemUpdate(eNo);
        iEquipNum++;                              // �A�C�e�����������Z
    }

    // �������Ă��镐�킪���x���ő傩�`�F�b�N
    public bool CheckWeaponMaxLevel(int eNo)
    {
        if (WeaponTbl[eNo].level >= WeaponDefine.LevelMax) { return true; }
        return false;
    }
    // �������Ă���A�C�e�������x���ő傩�`�F�b�N
    public bool CheckItemMaxLevel(int eNo)
    {
        if (ItemTbl[eNo].iconNo != (int)IconNo.ShotNumUp)
        {
            if (ItemTbl[eNo].level >= ItemDefine.LevelMax) { return true; }
        }
        else
        {
            if (ItemTbl[eNo].level >= 2) { return true; }
        }
        return false;
    }

    // �������Ă��镐��ŁA���x���}�b�N�X����Ȃ����퐔���擾
    public int GetWeaponMaxLevelNum()
    {
        int maxNum = 0;
        for (int chkNo = 0; chkNo < wEquipNum; ++chkNo)
        {
            if (CheckWeaponMaxLevel(chkNo))
            {
                maxNum++;
            }
        }
        return maxNum;
    }
    // �������Ă�����̂����x���}�b�N�X���`�F�b�N
    public bool CheckEquipLevelMax(int eNo, int iNo)
    {
        if (ImageManager.Ins.CheckIconNoIsWeapon(iNo))
        {
            return CheckWeaponMaxLevel(eNo);
        }
        else {
            return CheckItemMaxLevel(eNo);
        }
    }


    // �������Ă�����̂̃��x�����擾
    public int GetEquipLevel(int iNo)
    {
        int eNo;
        if (ImageManager.Ins.CheckIconNoIsWeapon(iNo))
        {
            eNo = CheckWeaponEquip(iNo);
            if (eNo != -1)
            {
                return WeaponTbl[eNo].level;
            }
        }
        else
        {
            eNo = CheckItemEquip(iNo);
            if (eNo != -1)
            {
                return ItemTbl[eNo].level;
            }
        }
        return 0;
    }




    // �������Ă���A�C�e���ŁA���x���}�b�N�X����Ȃ��A�C�e�������擾
    public int GetItemMaxLevelNum()
    {
        int maxNum = 0;
        for (int chkNo = 0; chkNo < iEquipNum; ++chkNo) {
            if (CheckItemMaxLevel(chkNo)) {
                maxNum++;
            }
        }
        return maxNum;
    }


    // �����g�S�Ă̕��킪���x���}�b�N�X���`�F�b�N
    public bool CheckWeaponLevelAllMax() {
        if (GetWeaponMaxLevelNum() >= WeaponDefine.EquipMax)
        {
            return true;
        }
        return false;
    }

    // �����g�S�ẴA�C�e�������x���}�b�N�X���`�F�b�N
    public bool CheckItemLevelAllMax()
    {
        if (GetItemMaxLevelNum() >= ItemDefine.EquipMax)
        {
            return true;
        }
        return false;
    }


    // �I���ł��镐��̐����擾
    public int GetChoiceWeaponNum()
    {
        return WeaponDefine.EquipMax - GetWeaponMaxLevelNum();
    }

    // ��������x���A�b�v
    public void LevelUpWeapon(int eNo, int addLevel) {
        WeaponTbl[eNo].level += addLevel;

        // 
        int iconNo = (int)WeaponTbl[eNo].iconNo;
        int level = WeaponTbl[eNo].level;

        // �ڍו��탌�x���A�b�v�ݒ�
        LevelUpWeaponSetup(eNo, iconNo, level);
    }

    // ���x���A�b�v�f�[�^�̃L�[���
    void LevelUpObjKeyDecode(string key, float value, ref Weapon wep) {
        switch (key) {
            case "Get": break;
            case "Flag1": wep.flag1 = true; break;
            case "Flag2": wep.flag2 = true; break;
            case "Flag3": wep.flag3 = true; break;
            case "Flag4": wep.flag4 = true; break;
            case "Dmg": wep.atkRate = value; break;
            case "CriHit": wep.criHitRate = (int)value; break;
            case "CriDmg": wep.criDmgRate = (int)value; break;
            case "Recast": wep.recastRate = value; break;
            case "Range": wep.areaRate = value; break;
            case "Speed": wep.speedRate = value; break;
            case "Num": wep.atkNum = (int)value; break;
            case "Time": wep.timeRate = value; break;
            default: Debug.Log($"key Error!!  key = {key}"); break;
        }
    }

    // ���펖�̐ݒ�
    void LevelUpWeaponSetup(int eNo, int iNo, int lv) 
    {
        int sNo = ImageManager.Ins.ConvIconNoToWeaponSerialNo(iNo);
        WeaponLVUPSOBJData data = WeaponManager.Ins.LVUPTable.data[sNo];

        // �n���h�K����p����
        if (sNo == 0 ) {
            GunSOBJ gSobj = WeaponManager.Ins.gunSobj;
            data.name = gSobj.wName;
            data.id = gSobj.id;

            data.key[0] = gSobj.key1;
            data.key[1] = gSobj.key2;
            data.key[2] = gSobj.key3;
            data.key[3] = gSobj.key4;
            data.key[4] = gSobj.key5;
            data.key[5] = gSobj.key6;
            data.key[6] = gSobj.key7;

            data.value[0] = gSobj.value1;
            data.value[1] = gSobj.value2;
            data.value[2] = gSobj.value3;
            data.value[3] = gSobj.value4;
            data.value[4] = gSobj.value5;
            data.value[5] = gSobj.value6;
            data.value[6] = gSobj.value7;
        }

        for (int i = 0; i < lv; i++)
        {
            LevelUpObjKeyDecode(data.key[i], data.value[i], ref WeaponTbl[eNo]);
        }

        //switch (iNo) {
        //    case (int)IconNo.TVWeaponPima:
        //        switch (lv)
        //        {
        //            case 2: WeaponTbl[eNo].areaRate = 1.5f; break;
        //            case 3: WeaponTbl[eNo].atkRate = 1.5f; break;
        //            case 4: break;
        //            case 5: WeaponTbl[eNo].recastRate = 0.25f; break;
        //            case 6: WeaponTbl[eNo].criHitRate = 50; break;
        //            case 7:; break;
        //        }
        //        break;
        //}
    }

    // ����̍U���͐ݒ�
    public void SetWeaponAtkPara(GameObject obj, IconNo iNo, HitAtkData atkData)
    {
        int calcAtkPow = atkData.AtkPow;

        int eNo = CheckWeaponEquip((int)iNo);
        // ����̃��[�g����v�Z
        if (eNo != -1) {
            calcAtkPow = Mathf.RoundToInt(calcAtkPow * WeaponTbl[eNo].atkRate);
            atkData.CriticalHit += WeaponTbl[eNo].criHitRate;
        }
        // �������𔽉f
        calcAtkPow = Mathf.RoundToInt(CalcRateToValue(calcAtkPow, ability.dmgRate));
        atkData.AtkPow = calcAtkPow;
        atkData.CriticalHit += (int)(CriHitRate / 100);

        // �g��k�������f
        Vector3 setScale = obj.transform.localScale;
        setScale.x *= ability.atkAreaRate * WeaponTbl[eNo].areaRate;
        setScale.y *= ability.atkAreaRate * WeaponTbl[eNo].areaRate;
        obj.transform.localScale = setScale;
    }


    // �U���͂��擾
    public int GetWeaponAtkPow(int initPow, float wepRate, float abiRate){
        float rate = wepRate + abiRate;                     // �_���[�W���[�g�v�Z
        return (int)CalcRateToValue(initPow, rate);
    }
    // �N���e�B�J�������擾�i�O�[�P�O�O�����擾�j
    public int GetWeaponCriHitRate(float wepRate, float abiRate) {
        return (int)(wepRate + abiRate);
    }
    // �N���e�B�J���_���[�W���擾�i�O�[�P�O�O�����擾�j
    public int GetWeaponCriDmgRate(float wepRate, float abiRate){
        return (int)(wepRate + abiRate);
    }

    // �e�����擾
    public float GetWeaponMoveSpd(float initSpd, float wepRate, float abiRate){
        float rate = wepRate + abiRate;
        return CalcRateToValue(initSpd, rate);
    }
    // �g�嗦���擾�i1.0�����{�Ŏ擾�j
    public float GetWeaponScaleRate(float wepRate, float abiRate) {
        return ConvRateToValue(wepRate + abiRate);
    }
    // �e�����擾
    public int GetWeaponShotNum(int wepNum, int abiNum){return wepNum + abiNum;}
    // �U�����Ԃ��擾
    public float GetWeaponAtkTime(float time, float wepRate, float abiRate) {
        float rate = wepRate + abiRate;
        return CalcRateToValue(time, rate);
    }




    // �A�C�e�������x���A�b�v
    public void LevelUpItem(int eNo, int addLevel)
    {
        ItemTbl[eNo].level += addLevel;
        ItemUpdate(eNo);
    }

    // �������f
    public void ItemUpdate(int eNo)
    {
        int level = ItemTbl[eNo].level;
        switch (ItemTbl[eNo].iconNo)
        {
            case (int)IconNo.MaxHPUp:       // LV x 20%
                ability.hpRate = level * AB_MaxHPRate;

                int setHP = (int)CalcRateToValue(initHP, ability.hpRate);
                int diff = setHP - hb.MaxHP; 
                hb.MaxHP = setHP;
                // �ő�HP���オ�������񕜂���
                HealHP(diff);
                break;

            case (int)IconNo.AutoHealUp:    // LV x 1
                ability.autoHealValue = level * AB_AutoHeal;
                break;

            case (int)IconNo.DefenseUp:     // �h���
                ability.defPow = (int)(level * AB_DefPow);
                defData.DefPow = ability.defPow;
                break;

            case (int)IconNo.MoveSpdUp:     // �ړ����x
                ability.moveSpdRate = level * AB_MoveSpdRate;
                break;

            case (int)IconNo.MagnetUp:      // ���W�͈�
                ability.magnetRate = level * AB_MagnetRate;
                
                break;

            case (int)IconNo.ExpUp:         // �o���l
                ability.expRate = level * AB_ExpRate;
                break;

            case (int)IconNo.GoldUp:        // ��
                ability.moneyRate = level * AB_MoneyRate;
                break;

            case (int)IconNo.LuckUp:        // ���b�N
                ability.luckRate = level * AB_LuckRate;
                break;

            case (int)IconNo.DamageUp:      // �_���[�W
                ability.dmgRate = level * AB_DmgRate;
                break;

            case (int)IconNo.CriticalUp:   // �N���e�B�J���q�b�g��  
                ability.criHitRate = level * AB_CriHitRate;
                break;

            case (int)IconNo.AtkTimeUp:     // �U������
                ability.atkTimeRate = level * AB_AtkTimeRate;
                break;

            case (int)IconNo.ShotNumUp:
                ability.addShotNum = level;
                break;

            case (int)IconNo.AtkAreaUp:
                ability.atkAreaRate = level * AB_AtkAreaRate;
                break;

            case (int)IconNo.AtkMoveSpdUp:
                ability.atkMoveRate = level * AB_AtkMoveRate;
                break;

            case (int)IconNo.RecastUp:
                ability.recastRate = level * AB_RecastRate;
                break;
        }
    }

    public Vector3 GetCenterPos()
    {
        Vector3 center = transform.position;
        center.y += 30.0f;
        return center;
    }

    public bool CheckDie() { return hb.CheckDie(); }

}
