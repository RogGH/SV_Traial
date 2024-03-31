// �X�N���v�^�u���I�u�W�F�N�g����̓ǂݍ���
//#define GDataLoadSObj

// JSON����̓ǂݍ���
//#define GDataLoadJson

// �X�N���v�g����̓ǂݍ���
#define GDataLoadScript

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;



public class EnemyGenerator2 : MonoBehaviour
{
    //private static EnemyGenerator2 ins;
    //public static EnemyGenerator2 Ins
    //{
    //    get
    //    {
    //        if (ins == null)
    //        {
    //            ins = (EnemyGenerator2)FindObjectOfType(typeof(EnemyGenerator2));
    //            if (ins == null) { Debug.LogError(typeof(EnemyGenerator2) + "is nothing"); }
    //        }
    //        return ins;
    //    }
    //}

    const string GeneDataPath = "Stage/";
    const string debugPath = "/Debug/";                 // �f�o�b�O�p
    const string debugJson = "AdvData.json";			// �f�o�b�O�̃t�@�C����

    const float BootWidth = 140.0f; // �G�N���̕�
    // 
    GameObject plObj;
    Player plScr;
    EnemyManager emMng;
    // 
    GenerateData[] gDataTbl = new GenerateData[(int)GLine.Num];
    float[] gLineTimer = new float[(int)GLine.Num];

    int geneSPTblNo = 0;            // ����e�[�u���ԍ�


    // �G�̃f�[�^���ǂ�����ēǂݍ��ނ̂�������
#if GDataLoadSObj
    public GeneSObjData sObjData;
#endif
#if GDataLoadJson
    [SerializeField] JSonGeneData jsonData; // �W�F�C�\������
#endif

    GenerateData[] geneTblData;                 // ���f�[�^
    GenerateSPData[] geneSPTblData;             // ���f�[�^
   
    void Awake()
    {
    }

    void Start()
    {
        plObj = StageManager.Ins.PlObj;
        plScr = StageManager.Ins.PlScr;
        emMng = EnemyManager.Ins;

        int stgNo = (int)SystemManager.Ins.selStgNo;

        //stgNo = (int)StageNo.Practice;
#if GDataLoadSObj
        geneTblData = sObjData.geneTbl;
#endif
#if GDataLoadJson
        // �W�F�C�\������f�[�^�ǂݍ���
        string dataPath = GeneDataPath;
        dataPath += "Stage1Data";
        jsonData = loadTextData(dataPath);
        geneTblData = jsonData.geneTbl;
#endif
#if GDataLoadScript
        geneTblData = GeneDataCtl.geneJagTbl[stgNo];
        geneSPTblData = GeneSPDataCtl.geneSPJagTbl[stgNo];
#endif
    }

    void Update()
    {
        if (plScr.CheckDie()) { return; }

        // �ʏ�N��
        float stgTime = StageManager.Ins.StageTime;

        // ���Y�H��
        for (int lineNo = 0; lineNo < (int)GLine.Num; ++lineNo)
        {
            if (gDataTbl[lineNo] != null)
            {
                GenerateData data = gDataTbl[lineNo];
                if (data.start <= stgTime && stgTime <= data.end)
                {
                    gLineTimer[lineNo] -= Time.deltaTime;
                    if (gLineTimer[lineNo] <= 0)
                    {
                        generateEnemy(data);
                        gLineTimer[lineNo] = data.interval;
                    }
                }
                else
                {
                    // ����
                    gDataTbl[lineNo] = null;
                }
            }
        }


        // �S�Ẵe�[�u�����`�F�b�N
        for (int tblNo = 0; tblNo < geneTblData.Length; ++tblNo)
        {
            GenerateData data = geneTblData[tblNo];
            // ���Y���ԃ`�F�b�N
            if (data.start <= stgTime && stgTime <= data.end) {
                // ���Y���C���ɍڂ���
                GenerateData chkData = gDataTbl[data.lineNo];

                // ���Ƀ��C�����o�^����Ă��邩�`�F�b�N
                if (chkData != null)
                {
                    if (chkData != data)
                    {
                        if (chkData.lineNo == data.lineNo)
                        {
                            Debug.Assert(false, "Already Line Used!! line " + data.lineNo + "tblNo = " + tblNo);
                        }
                    }
                }

                // �Ȃ��ꍇ�͓o�^�ł���
                if ( chkData == null ){
                    // �N��
                    generateEnemy(data);

                    gDataTbl[data.lineNo] = data;
                    gLineTimer[data.lineNo] = data.interval;
                }
            }
        }

        // ����N��
        if (geneSPTblNo < geneSPTblData.Length)
        {
            if (stgTime >= geneSPTblData[geneSPTblNo].start)
            {
                GenerateSPData spData = geneSPTblData[geneSPTblNo];
                generateSPEnemy(spData);
                // �e�[�u���ύX
                geneSPTblNo++;
            }
        }
    }

    // �G�N��
    void generateEnemy(GenerateData data)
    {
        GeneType gType = GeneType.Default;
        float sclX = 1280 / 2;
        float sclY = 720 / 2;
        float offset = 40;
        float radius = Mathf.Sqrt(sclX * sclX + sclY * sclY) + offset;
        float limitX = sclX + offset;
        float limitY = sclY + offset;
        int tNo = (int)data.emNo;
        Vector2 pos;

        switch (gType)
        {
            case GeneType.Default:
            case GeneType.Boss:
                // ��{
                {
                    // �����_�����W�ɋN��
                    float degree = UnityEngine.Random.Range(0, 360.0f);
                    float radian = Mathf.Deg2Rad * degree;

                    float setX = radius * Mathf.Cos(radian);
                    if (Mathf.Abs(setX) > limitX) { setX = limitX * Mathf.Sign(setX); }
                    float setY = radius * Mathf.Sin(radian);
                    if (Mathf.Abs(setY) > limitY) { setY = limitY * Mathf.Sign(setY); }

                    pos.x = plObj.transform.position.x + setX;
                    pos.y = plObj.transform.position.y + setY;

                    InsEnemy(tNo, pos);
                }
                break;
        }
    }

    // �G�N��
    void generateSPEnemy(GenerateSPData spData)
    {
        GeneType gType = (GeneType)spData.geneType;
        float sclX = 1280 / 2;
        float sclY = 720 / 2;
        float offset = 40;
        float radius = Mathf.Sqrt(sclX * sclX + sclY * sclY) + offset;
        float limitX = sclX + offset;
        float limitY = sclY + offset;
        int tNo = (int)spData.emNo;
        Vector2 pos;

        switch (gType){

            case GeneType.Default:
            case GeneType.Boss:
                // ��{
                // �����_�����W�ɋN��
                {
                    float degree = UnityEngine.Random.Range(0, 360.0f);
                    float radian = Mathf.Deg2Rad * degree;

                    float setX = radius * Mathf.Cos(radian);
                    if (Mathf.Abs(setX) > limitX) { setX = limitX * Mathf.Sign(setX); }
                    float setY = radius * Mathf.Sin(radian);
                    if (Mathf.Abs(setY) > limitY) { setY = limitY * Mathf.Sign(setY); }

                    pos.x = plObj.transform.position.x + setX;
                    pos.y = plObj.transform.position.y + setY;

                    InsEnemy(tNo, pos);
                }
                break;

            case GeneType.Circle:
            case GeneType.CircleTitan:
                // �~��ɔz�u
                {
                    int loopNum = (int)spData.fTemp0;
                    float degInterval = 360 / loopNum;
                    float shotRadius = radius / 1.25f;
                    float longRadius = radius;
                    if (gType == GeneType.CircleTitan)
                    {
                        shotRadius *= 1.2f;
                        longRadius *= 1.2f;
                    }

                    for (int loopNo = 0; loopNo < loopNum; ++loopNo)
                    {
                        float radian = Mathf.Deg2Rad * (degInterval * loopNo);

                        float setX = longRadius * Mathf.Cos(radian);
                        float setY = shotRadius * Mathf.Sin(radian);

                        pos.x = plObj.transform.position.x + setX;
                        pos.y = plObj.transform.position.y + setY;

                        InsEnemy(tNo, pos);
                    }
                }
                break;

            case GeneType.Group:
                // �W�c�ŋN��
                {
                    int loopNum = (int)(spData.fTemp0);
                    float degree = UnityEngine.Random.Range(0, 360.0f);
                    float radian = Mathf.Deg2Rad * degree;
                    float setX = radius * Mathf.Cos(radian);
                    if (Mathf.Abs(setX) > limitX) { setX = limitX * Mathf.Sign(setX); }
                    float setY = radius * Mathf.Sin(radian);
                    if (Mathf.Abs(setY) > limitY) { setY = limitY * Mathf.Sign(setY); }

                    float degInterval = 360 / loopNum;
                    float groupOffset = 40.0f;

                    pos.x = plObj.transform.position.x + setX;
                    pos.y = plObj.transform.position.y + setY;
                    GameObject obj = InsEnemy(tNo, pos);

                    for (int loopNo = 0; loopNo < loopNum; ++loopNo)
                    {
                        float offsetX = groupOffset * Mathf.Cos(degInterval * loopNo);
                        float offsetY = groupOffset * Mathf.Sin(degInterval * loopNo);

                        pos.x = plObj.transform.position.x + setX + offsetX;
                        pos.y = plObj.transform.position.y + setY + offsetY;

                        InsEnemy(tNo, pos);
                    }
                }
                break;


            case GeneType.Vertical:
                // �c�ɔz�u
                {
                    int bootNum = (int)spData.fTemp0;
                    float dir = spData.fTemp1;
                    float width = BootWidth;

                    float setX = plObj.transform.position.x + sclX * dir;
                    float setY = plObj.transform.position.y;
                    setY += width * bootNum / 2;

                    for (int loopNo = 0; loopNo < bootNum; ++loopNo)
                    {
                        pos.x = setX;
                        pos.y = setY - width * loopNo;

                        GameObject obj = InsEnemy(tNo, pos);
                        // �ړ��^�C�v�������ύX
                        obj.GetComponent<EnemyCmm>().moveType =
                            (dir < 0 ? EnemyCmm.MoveType.LToR : EnemyCmm.MoveType.RToL);
                    }
                }
                break;

            case GeneType.Horizontal:
                // ���ɔz�u
                {
                    int bootNum = (int)spData.fTemp0;
                    float dir = spData.fTemp1;
                    float width = BootWidth;

                    float setY = plObj.transform.position.y + sclY * dir;
                    float setX = plObj.transform.position.x;
                    setX -= width * bootNum / 2;

                    for (int loopNo = 0; loopNo < bootNum; ++loopNo)
                    {
                        pos.x = setX + width * loopNo;
                        pos.y = setY;

                        GameObject obj = InsEnemy(tNo, pos);
                        // �ړ��^�C�v�������ύX
                        obj.GetComponent<EnemyCmm>().moveType =
                            (dir < 0 ? EnemyCmm.MoveType.DToU : EnemyCmm.MoveType.UToD);
                    }
                }
                break;

        }
    }


    // �G����
    public GameObject InsEnemy(int sobjID, Vector2 pos)
    {
        EnemySOBJData sobj = EnemyManager.Ins.GetEnemySOBJData(sobjID);

        // �f�[�^���疼�O���擾
        string path = "Prefabs/EnemyP/" + sobj.prefab;
        GameObject prefab = Resources.Load<GameObject>(path);
        // �N��
        GameObject obj = Instantiate(prefab, pos, Quaternion.identity);
        obj.GetComponent<EnemyCmm>().SetSOBJData(sobj);
        return obj;
    }


    // �f�[�^�ǂݍ���
    public JSonGeneData loadTextData(string path)
    {
        string datastr = "";

        //if (false)
        //{
        //    // �f�o�b�O�̏ꍇ�̓A�v���P�[�V�����p�X�̒�����svedata.json��ǂݍ���
        //    // ���s�t�@�C���쐬����t�@�C����_data�t�H���_�̒��ɓ�����OK
        //    path = Application.dataPath + debugPath + debugJson;
        //    Debug.Log(path);
        //    StreamReader reader;
        //    reader = new StreamReader(path);

        //    // �������ǂݍ���
        //    datastr = reader.ReadToEnd();
        //    reader.Close();
        //}
        //else
        {
            // �ʏ�t�@�C��
            // �e�L�X�g�t�@�C���Ƃ��ăW�F�C�\����ǂݍ���
            Debug.Log(path);
            datastr = Resources.Load<TextAsset>(path).ToString();
        }
        // �W�F�C�\���ɕϊ�
        return JsonUtility.FromJson<JSonGeneData>(datastr);
    }
}