using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GenerateDataTable
{
    [Header("�o���J�n���ԁi�b�j")]public float start;
    [Header("�o���I�����ԁi�b�j")]public float end;
    [Header("�G�̃v���t�@�u")]public GameObject enemy;
    [Header("�o���Ԋu�i�b�j")]public float bootTimer;
    [HideInInspector]public float timer = 0;
}

public class EnemyGenerator3 : MonoBehaviour
{
    public GenerateDataTable[] tabel;
    GameObject plObj;
    Player plScr;

    void Start()
    {
        plObj = StageManager.Ins.PlObj;
        plScr = StageManager.Ins.PlScr;
    }

    void Update()
    {
        // �v���C���[���S�`�F�b�N
        if (plScr.CheckDie()) { return; }

        // �ʏ�N��
        float stgTime = StageManager.Ins.StageTime;

        // �z����`�F�b�N
        int num = tabel.Length;
        for (int i = 0; i < num; ++i) 
        {
            // �f�[�^���擾
            GenerateDataTable gData = tabel[i];
            // ���Ԃ��`�F�b�N
            if (gData.start <= stgTime && stgTime <= gData.end)
            {
                // �^�C�}�[�`�F�b�N
                gData.timer -= Time.deltaTime;
                if (gData.timer <= 0)
                {
                    // �ēx���Ԃ�ݒ�
                    gData.timer = gData.bootTimer;

                    float sclX = 1280 / 2;
                    float sclY = 720 / 2;
                    float offset = 40;
                    float radius = Mathf.Sqrt(sclX * sclX + sclY * sclY) + offset;
                    float limitX = sclX + offset;
                    float limitY = sclY + offset;
                    Vector2 pos;
                    float degree = UnityEngine.Random.Range(0, 360.0f);
                    float radian = Mathf.Deg2Rad * degree;

                    float setX = radius * Mathf.Cos(radian);
                    if (Mathf.Abs(setX) > limitX) { setX = limitX * Mathf.Sign(setX); }
                    float setY = radius * Mathf.Sin(radian);
                    if (Mathf.Abs(setY) > limitY) { setY = limitY * Mathf.Sign(setY); }

                    pos.x = plObj.transform.position.x + setX;
                    pos.y = plObj.transform.position.y + setY;

                    // 
                    Instantiate(gData.enemy, pos, Quaternion.identity);
                }
            }
        }
    }
}
