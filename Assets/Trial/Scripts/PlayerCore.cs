using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Player
{
    // �v���C���[�̏���
    void playerCore() 
    {
        // �����ɃL�[���䖽�߂����
        KeyInputUpdate();

        // �����ɕ��퐧�䖽�߂����
        WeaponControl();
    }

    // �L�[����
    void KeyInputUpdate()
    {
        float lr = Input.GetAxisRaw("Horizontal");
        float ud = Input.GetAxisRaw("Vertical");

        if (lr != 0 || ud != 0)
        {

            float spd = CalcRateToValue(MoveSpeed, ability.moveSpdRate);
            float rate = 1.0f;
            if (lr != 0 && ud != 0) { rate = 0.71f; }   // �΂߈ړ��΍�
            spd *= rate * 60 * Time.deltaTime;
            transform.position += new Vector3(spd * lr, spd * ud, 0);

            ChangeAnim(AnimNo.Move);
        }
        else
        {
            ChangeAnim(AnimNo.Idle);
        }

        if (lr != 0)
        {
            Vector3 scale = transform.localScale;
            scale.x = Mathf.Abs(scale.x) * lr;
            scale.x *= defaultDir;
            transform.localScale = scale;
        }
    }

    // ���퐧��
    void WeaponControl()
    {
        for (int i = 0; i < weaponTblNum; ++i)
        {
            if (WeaponTbl[i].initFlag == true)
            {                    // �������`�F�b�N
                WeaponTbl[i].recastCounter -= Time.deltaTime;       // �^�C�}�[�`�F�b�N
                if (WeaponTbl[i].recastCounter <= 0)
                {              // ���L���X�g�`�F�b�N
                    int sNo = WeaponTbl[i].serialNo;
                    WeaponBoot(sNo, ref WeaponTbl[i]);            // �N��

                    // ���L���X�g���Ԑݒ�
                    float setRecast = WeaponManager.Ins.GetWeaponRecastMin(sNo);    // �Œ჊�L���X�g��ݒ�
                    float calcRate = 1 - (WeaponTbl[i].recastRate + ability.recastRate) / 100;  // ���[�g�v�Z
                    if (calcRate <= 0) { calcRate = 0; }                            // 

                    float calcRecast = WeaponManager.Ins.GetWeaponRecastCalc(sNo);  // �ő�-�ŏ��̒l���擾
                    setRecast += calcRecast * calcRate;
                    WeaponTbl[i].recastCounter = setRecast;
                    WeaponTbl[i].recastTime = WeaponTbl[i].recastCounter;
                }
            }
        }
    }


    void WeaponBoot(int sNo, ref Weapon wep)
    {

        int lv = wep.level;
        GameObject wepObj = WeaponManager.Ins.WeaponObjTbl[sNo];

        switch (sNo)
        {
            case 0:
                // �n���h�K��
                {
                    GameObject obj = Instantiate(wepObj, transform.position, Quaternion.identity);
                    obj.GetComponent<PLShellShotControl>().SetShotCtlPara(gameObject, wep, ability, GetAngleToPLMouse());
                }
                break;

            case 1:
                // �h����
                {
                    GameObject obj = Instantiate(wepObj, GetCenterPos(), Quaternion.identity);
                    obj.GetComponent<PLShellDrill>().SetDrillPara(gameObject, wep, ability);
                    SeManager.Instance.Play("Drill");
                }
                break;

            case 2:
                // ��]�̂�����
                {
                    for (int i = 0; i < wep.atkNum; ++i)
                    {
                        GameObject obj = Instantiate(wepObj, GetCenterPos(), Quaternion.identity);
                        obj.GetComponent<PLShellSaw>().SetSawPara(gameObject, wep, ability, i);
                    }

                    SeManager.Instance.Play("Saw");
                }
                break;

            case 3:
                // �Ή�����
                {
                    GameObject obj = Instantiate(wepObj, transform.position, Quaternion.identity);
                    obj.GetComponent<PLShellFireControl>().SetFireCtlPara(gameObject, wep, ability);

                    SeManager.Instance.Play("Fire");
                }
                break;

            case 4:
                // �^���b�g
                {
                    for (int i = 0; i < wep.atkNum; ++i)
                    {
                        Vector3 pos = transform.position;
                        float radian = GetAngleToPLMouse();
                        float ofs = 50 * (i == 0 ? -1 : 1);
                        pos.x += Mathf.Cos(radian) * ofs;
                        pos.y += Mathf.Sin(radian) * ofs;
                        GameObject obj = Instantiate(wepObj, pos, Quaternion.identity);
                        obj.GetComponent<PLShellTurretBody>().SetTurretBodyPara(gameObject, wep, ability);
                    }
                }
                break;

            case 5:
                // ���m
                {
                    SeManager.Instance.Play("Monitor");

                    for (int i = 0; i < wep.atkNum; ++i)
                    {
                        GameObject obj = Instantiate(wepObj, transform.position, Quaternion.identity);
                        float dir;
                        if (wep.atkNum == 1)
                        {
                            dir = (Random.Range(0, 2) == 0) ? -1 : 1;   // �����_���ō��E�ǂ��炩
                        }
                        else
                        {
                            dir = (i == 0 ? -1 : 1);                    // ����
                        }
                        obj.GetComponent<PLShellMonitor>().SetWaveMonitorPara(gameObject, wep, ability, dir);
                    }
                }
                break;
        }
    }

}
