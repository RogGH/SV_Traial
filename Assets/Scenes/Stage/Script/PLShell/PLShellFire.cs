using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PLShellFire : MonoBehaviour
{
    public HitAtkData atkData;
    HitBase hb;
    SpriteRenderer spComp;

    float radian;
    float hitTime = 30;
    Vector3 vel = Vector3.zero;
    float moveSpd = 3.5f * 60;
    int level;

    // Start is called before the first frame update
    void Start()
    {
        hb = GetComponent<HitBase>();
        spComp = GetComponent<SpriteRenderer>();

        float setSpd = moveSpd;
        vel.x = setSpd * Mathf.Cos(radian);
        vel.y = setSpd * Mathf.Sin(radian);

        Destroy(gameObject, WeaponDefine.LiveCountDef);
    }

    // Update is called once per frame
    void Update()
    {
        if (StageManager.Ins.CheckStop()) { return; }
        if (WeaponDefine.CameraOut(gameObject)) { Destroy(gameObject); return; }

        hb.PreUpdate();

        hitTime -= Time.deltaTime;
        if (hitTime <= 0) {
            hb.AtkActive = false;
        }

        transform.position += vel * Time.deltaTime;
        // ���݂̍��W����A�\���D������肳���Ă݂�
        spComp.sortingOrder = IObject.GetSpriteOrder(transform.position);

        hb.PostUpdate();
    }

    public void SetFirePara(GameObject pl, Player.Weapon wep, Player.Ability abi, float setRad, int type)
    {
        Player plScr = pl.GetComponent<Player>();
        WeaponInitSOBJData initData = 
            (type == 0) ?
            WeaponManager.Ins.InitTable.data[(int)WeaponInitSOBJEnum.FrameThrower] :
            WeaponManager.Ins.InitTable.data[(int)WeaponInitSOBJEnum.BioBlast];

        // �U����
        atkData.AtkPow = plScr.GetWeaponAtkPow(initData.atk, wep.atkRate, abi.dmgRate);
        atkData.CriticalHit = plScr.GetWeaponCriHitRate(wep.criHitRate, abi.criHitRate);
        atkData.CriticalDmg = plScr.GetWeaponCriDmgRate(wep.criDmgRate, abi.criDmgRate);
        // �g�嗦
        transform.localScale *= plScr.GetWeaponScaleRate(wep.areaRate, abi.atkAreaRate);

        // �e��
        moveSpd = plScr.GetWeaponMoveSpd(initData.speed, wep.speedRate, abi.atkMoveRate);

        // ��p
        radian = setRad;
        // �o�C�I�u���X�g��p
        if (type == 1) {
            moveSpd *= 0.75f;
        }
    }

    public void HitEnd() {
        hb.SetAtkActive(false);
    }

    public void AnimEnd() {
        Destroy(this.gameObject);
    }
}
