using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PLShellSaw : MonoBehaviour
{
    public HitAtkData atkData;

    HitBase hb;
    SpriteRenderer spComp;

    GameObject plObj;
    Player plScr;

    const float initDeg = 90;

    float deg = initDeg;
    float rotSpd;
    float rotTime = 1.0f;
    float ofs = 0;

    void Start()
    {
        if (plObj == null)
        {
            plObj = StageManager.Ins.PlObj;
            plScr = StageManager.Ins.PlScr;
        }

        hb = GetComponent<HitBase>();
        spComp = GetComponent<SpriteRenderer>();

        Destroy(gameObject, WeaponDefine.LiveCountDef);
    }

    // Update is called once per frame
    void Update()
    {
        if (StageManager.Ins.CheckStop()) { return; }

        hb.PreUpdate();

        deg += rotSpd * Time.deltaTime;

        rotTime -= Time.deltaTime;
        if( rotTime <= 0) { 
            Destroy(gameObject);
            return;
        }

        setPos();
        // ���݂̍��W����A�\���D������肳���Ă݂�
        spComp.sortingOrder = IObject.GetSpriteOrder(transform.position);


        hb.PostUpdate();
    }

    public void SetSawPara(GameObject pl, Player.Weapon wep, Player.Ability abi, int type)
    {
        // ����
        plObj = pl;
        plScr = pl.GetComponent<Player>();
        WeaponInitSOBJData initData = WeaponManager.Ins.InitTable.data[(int)WeaponInitSOBJEnum.Saw];

        // �U���͊֘A
        atkData.AtkPow = plScr.GetWeaponAtkPow(initData.atk, wep.atkRate, abi.dmgRate);
        atkData.CriticalHit = plScr.GetWeaponCriHitRate(wep.criHitRate, abi.criHitRate);
        atkData.CriticalDmg = plScr.GetWeaponCriDmgRate(wep.criDmgRate, abi.criDmgRate);
        // �g�嗦
        transform.localScale *= plScr.GetWeaponScaleRate(wep.areaRate, abi.atkAreaRate);

        // �e��
        rotSpd = plScr.GetWeaponMoveSpd(initData.speed, wep.speedRate, abi.atkMoveRate);
        // ���΂܂��p�^�[��
        //rotSpd *= (type == 0) ? 1 : -1;
        // �Y��ɉ��p�^�[��
        deg = (type == 0) ? initDeg : -initDeg;

        // ���Ԑݒ�
        rotTime = plScr.GetWeaponAtkTime(rotTime, wep.timeRate, abi.atkTimeRate);

        // ���W�ݒ�
        setPos();
    }

    void setPos()
    {
        Vector3 localRot = transform.localEulerAngles;
        localRot.z = deg;
        transform.localEulerAngles = localRot;

        Vector3 pos = plScr.GetCenterPos();
        float radian = deg * Mathf.Deg2Rad;
        pos.x += ofs * Mathf.Cos(radian);
        pos.y += ofs * Mathf.Sin(radian);
        transform.position = pos;
    }
}
