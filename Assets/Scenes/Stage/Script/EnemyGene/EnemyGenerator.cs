using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * �G�̗N�����ɂ���
 * 
 * �Q�l �ق낫�゠�P��
 * �ŏ����1�b�ɂ��P��
 * 30�b�ŕʂ̓G���P�́A�͂ނ悤�ɍ����N����
 * 1���Œ��̌Q�ꂪ�o��
 * 1���R�O�b�ō����c��ɕ���ŏo��
 * �Q���Ń{�X
 * �Q���R�O�b�����͂�
 * �Q���S�T�b�@����������C��
 * �R���V���ȓG���݂����Ȃ��
 * �R���P�T�b�㉺���͂ޗp�ɓG���N��
 * �R���Q�O�b���̓ˌ�
 * �R���S�O�b�㉺�̓ːi
 * �S���{�X�{�V�����G�i���j
 * �S���Q�O�b�͂ޓG
 * �T�����̌Q��A�V�����G�i������j
 * �T���R�O�b������A���[�g�S�C�T��̌Q��
 * �U���{�X�A�V�����G�i�������c�j
 * �U���R�O�b�V�����G�i���j�j
 * �V���i�Z�͂��j
 * �V���R�O�b
 * �W���{�X
 * �W���R�O�b���������́Z�݂͂X�A���炢�@�ւ̎��̐V�����G
 * �X�����̌Q��A�T�b���ƂɂS��
 * �P�O���{�X
 * �P�O���P�T�b�㉺���E����A���[�g
 * �P�P���@�V�G�i���[�[���g���j
 * �P�P���R�O�b���́Z�͂�
 * �P�Q���V�G�i���v�j
 * �P�Q���Q�O�b���E�A���[�g
 * �P�Q���Q�T�b�㉺�A���[�g
 * �P�Q���R�O�b���v�F�Ⴂ
 * �P�Q���p�b�㉺���E�A���[�g
 * �P�R�����v�{�X
 * �P�R���R�O�b���Q��A�T�b���ƂɂS��
 * �P�S�����ƕ������̍��E����A���o��
 * �P�S���Q�O�b�ォ��A���[�g���T�b���ƂS��i��A���A�㉺�A���E�S�A�j
 * �P�S���S�T�b�A���[�g�㉺
 * �P�T���Q�̃{�X
 * �P�T���S�T�b�V�G�i�t�N���E�A���Łj
 * �P�U���P�T�b�@���͂�
 * �P�V���㉺���E�A���[�g
 * �P�V���R�O�b�{�X
 * �P�W���ł��G
 * �P�X���S���ł���
 * �Q�O���X�e�[�W�{�X�A�V�G
 * 
 * 
 * 
 * ���@���p�C�A�T�o�C�o�[�@�P�����ɑ�̓G�̕��������ς��
 * */


// �Ƃ肠�����G�̎�ނP�O��ނ��炢��邩�c�H
public enum EnemyNo
{
    SP1Blue,                        // �X�v���K��
    SP2Green,   
    SP3Pink,

    SUB1,                           // �T�{�e���_�[
    SUB2Black,                      // 
    SUB3Red,                        // 

    Bomb1Red,                       // �{��   
    Bomb2Blue,
    Bomb3Green,

    Cat1Black,                      // �Q�C���L���b�g
    Cat2Brown,
    Cat3Blue,

    Purin1Green,                    // �v����
    Purin2Red,
    Purin3Blue,
    Purin4Purple,
    Purin5Yellow,

    Ahriman1Yellow,                 // �A�[���}��
    Ahriman2Blue,
    Ahriman3Red,                    

    Tonberi1Green,                  // �g���x��
    Tonberi2Blue,
    Tonberi3Black,

    BossSpBlue,                     // �{�X�F�X�v���K��
    BossWallPurinPurble,            // �{�X�p�F�ǃv����
    BossCatBlack,                   // �{�X�F�Q�C���L���b�g
    BossAhriman,                    // �{�X�F�A�[���}��
    BossBomb,                       // �{�X�F�{��
    BossTaitan,                     // �{�X�F�^�C�^��

    Num

};

public class EnemyGenerator : MonoBehaviour
{
    public GameObject[] enemyTbl;
    GameObject pl;
    const float InvalidTimer = 100;

    enum GeneType
    {
        Default,
        Circle,
        Group,
        Boss,
        Vertical,
        Horizontal,
        CircleTitan,

        Num
    };

    // �쐬�f�[�^
    class GeneratData
    {
        public GeneratData(float sec, EnemyNo eNo, float gTime)
        {
            startSec = sec;
            enemyNo = eNo;
            geneTime = gTime;
            geneType = GeneType.Default;
        }
        public GeneratData(float sec, EnemyNo eNo, GeneType gType, float t1 = 0, float t2 = 0)
        {
            startSec = sec;
            enemyNo = eNo;
            geneTime = InvalidTimer;
            geneType = gType;
            fTemp1 = t1;
            fTemp2 = t2;
        }

        // �ʏ�
        public float startSec;
        public EnemyNo enemyNo;
        public float geneTime;
        public GeneType geneType;
        public float fTemp1 = 0;
        public float fTemp2 = 0;
    };
    GeneratData[] geneTbl = new GeneratData[]{
        // 0-120
        new GeneratData(0, EnemyNo.SP1Blue, 1.0f),
        new GeneratData(30, EnemyNo.Cat1Black, 0.9f),
        new GeneratData(60, EnemyNo.Purin1Green, 1.0f),
        new GeneratData(90, EnemyNo.Ahriman1Yellow, 0.9f),

        // 120-240
        new GeneratData(120, EnemyNo.SP1Blue, 1.0f),
        new GeneratData(150, EnemyNo.Tonberi1Green, 1.0f),
        new GeneratData(180, EnemyNo.SP2Green, 0.7f),
        new GeneratData(210, EnemyNo.Purin2Red, 0.8f),

        // 240-360
        new GeneratData(240, EnemyNo.Cat1Black, 0.3f),
        new GeneratData(300, EnemyNo.Ahriman2Blue, 0.6f),
        new GeneratData(330, EnemyNo.Cat2Brown, 0.6f),

        // 360-480
        new GeneratData(360, EnemyNo.Ahriman2Blue, 0.8f),
        new GeneratData(420, EnemyNo.Tonberi2Blue, 0.7f),
        new GeneratData(450, EnemyNo.SP3Pink, 0.4f),

        // 480-600
        new GeneratData(480, EnemyNo.Purin3Blue, 0.8f),
        new GeneratData(510, EnemyNo.Cat3Blue, 0.5f),
        new GeneratData(540, EnemyNo.Ahriman3Red, 0.5f),
        new GeneratData(570, EnemyNo.Tonberi3Black, 0.6f),

        // 600
        new GeneratData(600, EnemyNo.SP3Pink, 1.0f),
        new GeneratData(660, EnemyNo.Ahriman3Red, 1.0f),
        new GeneratData(720, EnemyNo.Tonberi3Black, 0.1f),
    };

    int geneTblNo = 0;
    float timer = 0;

    GeneratData[] geneSPTbl = new GeneratData[]{
        // 0-120
        new GeneratData(20, EnemyNo.SP1Blue, GeneType.Circle, 20),              // �͂�
        new GeneratData(50, EnemyNo.SUB1, GeneType.Group, 6),                   // �ˌ�
        new GeneratData(80, EnemyNo.SUB1, GeneType.Group, 10),                  // �ˌ�

        // 120-240
        new GeneratData(120, EnemyNo.BossSpBlue, GeneType.Default),             // �{�X�P     
        new GeneratData(120, EnemyNo.BossWallPurinPurble, GeneType.Circle, 60), // �͂�        
        new GeneratData(180, EnemyNo.Bomb1Red, GeneType.Circle, 4),             // �͂�
        new GeneratData(210, EnemyNo.Bomb1Red, GeneType.Circle, 8),             // �͂�

        // 240-360
        new GeneratData(240, EnemyNo.BossCatBlack, GeneType.Default),           // �{�X�Q        
        new GeneratData(260, EnemyNo.SUB1, GeneType.Horizontal, 15, 1),         // �E����ˌ�
        new GeneratData(270, EnemyNo.SUB1, GeneType.Vertical, 15, 1),           // �ォ��c�ˌ�  
        new GeneratData(280, EnemyNo.SUB1, GeneType.Horizontal, 15, -1),        // �����牡�ˌ�  
        new GeneratData(290, EnemyNo.SUB1, GeneType.Vertical, 15, 1),           // ������c�ˌ�  

        // 360-480
        new GeneratData(360, EnemyNo.BossAhriman, GeneType.Default),            // �{�X�R       
        new GeneratData(380, EnemyNo.SUB2Black, GeneType.Circle, 4),            // 
        new GeneratData(390, EnemyNo.SUB2Black, GeneType.Circle, 6),            // 
        new GeneratData(400, EnemyNo.SUB2Black, GeneType.Circle, 8),            // 
        new GeneratData(410, EnemyNo.SUB2Black, GeneType.Circle, 10),           // 
        new GeneratData(420, EnemyNo.SUB2Black, GeneType.Circle, 12),           //  

        new GeneratData(450, EnemyNo.Bomb2Blue, GeneType.Circle, 4),           //
        new GeneratData(460, EnemyNo.Bomb2Blue, GeneType.Circle, 6),           //
        new GeneratData(470, EnemyNo.Bomb2Blue, GeneType.Circle, 8),           //

        // 480-600
        new GeneratData(480, EnemyNo.BossBomb, GeneType.Default),               // �{�X�S        
        new GeneratData(480, EnemyNo.BossWallPurinPurble, GeneType.Circle, 70), // �͂�        
        new GeneratData(530, EnemyNo.SUB3Red, GeneType.Horizontal, 15, 1),         // �E����ˌ�
        new GeneratData(540, EnemyNo.SUB3Red, GeneType.Vertical, 15, 1),           // �ォ��c�ˌ�  
        new GeneratData(550, EnemyNo.SUB3Red, GeneType.Horizontal, 15, -1),        // �����牡�ˌ�  
        new GeneratData(560, EnemyNo.SUB3Red, GeneType.Vertical, 15, 1),           // ������c�ˌ�  
        new GeneratData(570, EnemyNo.SUB3Red, GeneType.Circle, 15),             // 
        new GeneratData(580, EnemyNo.Bomb3Green, GeneType.Circle, 10),             // 

        // 600
        new GeneratData(600, EnemyNo.BossTaitan, GeneType.Default),             // �{�X�T        
        new GeneratData(600, EnemyNo.Purin5Yellow, GeneType.Circle, 80), // �͂�        
        new GeneratData(660, EnemyNo.Purin5Yellow, GeneType.Circle, 80), // �͂�        
   };

    int geneSPTblNo = 0;
    const float BootWidth = 140.0f;

    void Start()
    {
        pl = StageManager.Ins.PlObj;
    }

    void Update()
    {
        // �ʏ�N��
        float stgTime = StageManager.Ins.StageTime;
        if (geneTblNo < geneTbl.Length - 1)
        {
            if (stgTime >= geneTbl[geneTblNo + 1].startSec)
            {
                // �e�[�u���ύX
                geneTblNo++;
                // �p�����[�^������
                timer = 0;
            }
        }
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            generateEnemy(geneTbl[geneTblNo]);
            timer = geneTbl[geneTblNo].geneTime;
        }


        // ����N��
        if (geneSPTblNo < geneSPTbl.Length)
        {
            if (stgTime >= geneSPTbl[geneSPTblNo].startSec)
            {
                generateEnemy(geneSPTbl[geneSPTblNo]);
                // �e�[�u���ύX
                geneSPTblNo++;
                // �p�����[�^������
                timer = 0;
            }
        }
    }

    void generateEnemy(GeneratData data)
    {
        GeneType gType = data.geneType;
        float sclX = 1280 / 2;
        float sclY = 720 / 2;
        float offset = 40;
        float radius = Mathf.Sqrt(sclX * sclX + sclY * sclY) + offset;
        float limitX = sclX + offset;
        float limitY = sclY + offset;
        int tNo = (int)data.enemyNo;
        Vector2 pos;

        switch (gType)
        {
            case GeneType.Default:
            case GeneType.Boss:
                // ��{
                {
                    float degree = Random.Range(0, 360.0f);
                    float radian = Mathf.Deg2Rad * degree;

                    float setX = radius * Mathf.Cos(radian);
                    if (Mathf.Abs(setX) > limitX) { setX = limitX * Mathf.Sign(setX); }
                    float setY = radius * Mathf.Sin(radian);
                    if (Mathf.Abs(setY) > limitY) { setY = limitY * Mathf.Sign(setY); }

                    pos.x = pl.transform.position.x + setX;
                    pos.y = pl.transform.position.y + setY;

                    // �Ƃ肠���������_���ŏo��
                    //int rand = Random.Range(0, enemyTbl.Length);
                    Instantiate(enemyTbl[tNo], pos, Quaternion.identity);
                }
                break;

            case GeneType.Circle:
            case GeneType.CircleTitan:
                // �~��ɔz�u
                {
                    int loopNum = (int)data.fTemp1;
                    float degInterval = 360 / data.fTemp1;
                    float shotRadius = radius / 1.25f;
                    float longRadius = radius;
                    if (gType == GeneType.CircleTitan) {
                        shotRadius *= 1.2f;
                        longRadius *= 1.2f;
                    }

                    for (int loopNo = 0; loopNo < loopNum; ++loopNo)
                    {
                        float radian = Mathf.Deg2Rad * (degInterval * loopNo);

                        float setX = longRadius * Mathf.Cos(radian);
                        float setY = shotRadius * Mathf.Sin(radian);

                        pos.x = pl.transform.position.x + setX;
                        pos.y = pl.transform.position.y + setY;

                        Instantiate(enemyTbl[tNo], pos, Quaternion.identity);
                    }
                }
                break;

            case GeneType.Group:
                // �W�c�ŋN��
                {
                    int loopNum = (int)(data.fTemp1);
                    float degree = Random.Range(0, 360.0f);
                    float radian = Mathf.Deg2Rad * degree;
                    float setX = radius * Mathf.Cos(radian);
                    if (Mathf.Abs(setX) > limitX) { setX = limitX * Mathf.Sign(setX); }
                    float setY = radius * Mathf.Sin(radian);
                    if (Mathf.Abs(setY) > limitY) { setY = limitY * Mathf.Sign(setY); }

                    float degInterval = 360 / loopNum;
                    float groupOffset = 40.0f;

                    pos.x = pl.transform.position.x + setX;
                    pos.y = pl.transform.position.y + setY;
                    Instantiate(enemyTbl[tNo], pos, Quaternion.identity);

                    for (int loopNo = 0; loopNo < loopNum; ++loopNo)
                    {
                        float offsetX = groupOffset * Mathf.Cos(degInterval * loopNo);
                        float offsetY = groupOffset * Mathf.Sin(degInterval * loopNo);

                        pos.x = pl.transform.position.x + setX + offsetX;
                        pos.y = pl.transform.position.y + setY + offsetY;

                        Instantiate(enemyTbl[tNo], pos, Quaternion.identity);
                    }
                }
                break;

            case GeneType.Vertical:
                // �c�ɔz�u
                {
                    int bootNum = (int)data.fTemp1;
                    float dir = data.fTemp2;
                    float width = BootWidth;

                    float setX = pl.transform.position.x + sclX * dir;
                    float setY = pl.transform.position.y;
                    setY += width * bootNum / 2;

                    for (int loopNo = 0; loopNo < bootNum; ++loopNo)
                    {
                        pos.x = setX;
                        pos.y = setY - width * loopNo;

                        GameObject obj = Instantiate(enemyTbl[tNo], pos, Quaternion.identity);
                        // �ړ��^�C�v�������ύX
                        obj.GetComponent<Enemy1>().moveType =
                            (dir < 0 ? Enemy1.MoveType.LToR : Enemy1.MoveType.RToL);
                    }
                }
                break;

            case GeneType.Horizontal:
                // ���ɔz�u
                {
                    int bootNum = (int)data.fTemp1;
                    float dir = data.fTemp2;
                    float width = BootWidth;

                    float setY = pl.transform.position.y + sclY * dir;
                    float setX = pl.transform.position.x;
                    setX -= width * bootNum / 2;

                    for (int loopNo = 0; loopNo < bootNum; ++loopNo)
                    {
                        pos.x = setX + width * loopNo;
                        pos.y = setY;

                        GameObject obj = Instantiate(enemyTbl[tNo], pos, Quaternion.identity);
                        // �ړ��^�C�v�������ύX
                        obj.GetComponent<Enemy1>().moveType =
                            (dir < 0 ? Enemy1.MoveType.DToU : Enemy1.MoveType.UToD);
                    }
                }
                break;
        }
    }
}