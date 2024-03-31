using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PLShellMonitor : MonoBehaviour
{
    public GameObject beamObj;

    GameObject plObj;
    Player plScr;

    Player.Weapon weapon;
    Player.Ability ability;

    float dir;
    float ofs = 40.0f;
    float time = 2.0f;
    float shellbootTime = 2.0f - 1.0f;
    bool bootFlag = false;

    // Start is called before the first frame update
    void Start()
    {
        plObj = StageManager.Ins.PlObj;
        plScr = StageManager.Ins.PlScr;
    }

    // Update is called once per frame
    void Update()
    {
        if (StageManager.Ins.CheckStop()) { return; }
        if (plObj == null) { Destroy(gameObject); }

        // �ʒu�␳
        Vector3 setPos = plScr.GetCenterPos();
        setPos.x += ofs * dir;
        transform.position = setPos;

        time -= Time.deltaTime;
        if (bootFlag == false) {
            if (time <= shellbootTime)
            {
                // �I�u�W�F�N�g����
                GameObject tempObj = null;
                foreach (GameObject chkObj in GameObject.FindGameObjectsWithTag("Enemy")) {

                    // ��ʓ��ɂ��邩�ǂ����`�F�b�N
                    if (!plScr.CheckPLCameraIn(chkObj.transform.position)) {
                        continue;
                    }

                    if (dir < 0) {
                        // �������T�[�`
                        if (chkObj.transform.position.x < plObj.transform.position.x) 
                        {
                            tempObj = chkObj;
                            break;
                        }
                    }
                    else {
                        // �E�����T�[�`
                        if (chkObj.transform.position.x > plObj.transform.position.x)
                        {
                            tempObj = chkObj;
                            break;
                        }
                    }
                }

                // ���Ȃ������烉���_���ł�����
                if (tempObj == null)
                {
                    foreach (GameObject chkObj in GameObject.FindGameObjectsWithTag("Enemy"))
                    {
                        // ��ʓ��ɂ��邩�ǂ����`�F�b�N
                        if (!plScr.CheckPLCameraIn(chkObj.transform.position))
                        {
                            continue;
                        }

                        // �V�[����ɑ��݂���I�u�W�F�N�g�Ȃ�Ώ���.
                        if (chkObj.activeInHierarchy)
                        {
                            tempObj = chkObj;
                            break;
                        }
                    }
                }
                // ����ɂ��Ȃ���v���C���[��OK
                if (tempObj == null) { tempObj = StageManager.Ins.PlObj; }

                // �r�[���N��
                GameObject obj = Instantiate(beamObj, tempObj.transform.position, Quaternion.identity);
                obj.GetComponent<PLShellMonitorBeam>().SetWaveBeamPara(plObj, weapon, ability);

                bootFlag = true;
            }
        }

        if (time <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void SetWaveMonitorPara(GameObject pl, Player.Weapon wep, Player.Ability abi, float setDir) {
        // ����
        plObj = pl;
        plScr = pl.GetComponent<Player>();

        weapon = wep;
        ability = abi;

        // ��p
        dir = setDir;
    }
}
