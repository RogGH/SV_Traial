using UnityEngine;
using System;

// ���Y�^�C�v
public enum GeneType
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

// ���Y���C���ԍ�
public enum GLine{
    Line1, Line2, Line3, Line4, Num
}


// �쐬�f�[�^
[System.Serializable]
public class GenerateData{
    // �p�����[�^
    public int emNo;                    // �G�ԍ�
    public float start;                 // �J�n����
    public float end;                   // �I������
    public float interval;              // �Ԋu�i�b�j
    public int lineNo;                  // ���Y���C���ԍ�

    public GenerateData(EnemySOBJEnum no, float s, float e, float i, GLine lNo = GLine.Line1) {
        emNo = (int)no; start = s; end = e; interval = i; lineNo = (int)lNo;
    }
};


// ����f�[�^
[Serializable]
public class GenerateSPData{
    // �p�����[�^
    public int emNo;                     // �G�ԍ�
    public float start;                  // �J�n����
    public int geneType;                 // �����p�^�[��      
    public float fTemp0 = 0;             // �ėp0
    public float fTemp1 = 0;             // �ėp1

    public GenerateSPData(EnemySOBJEnum no, float s, GeneType gType, float f0 = 0, float f1 = 0){
        emNo = (int)no; start = s; geneType = (int)gType; fTemp0 = f0; fTemp1 = f1;
    }
};


// �܂Ƃ߃f�[�^
public static class GeneDataCtl {
    public static GenerateData[][] geneJagTbl = {
        Stage1Data.stg1DataTbl,
        Stage0Data.stg0DataTbl,
        Stage1Data.stg1DataTbl,
        Stage1Data.stg1DataTbl,
        Stage1Data.stg1DataTbl,
        Stage1Data.stg1DataTbl,
    };
};

public static class GeneSPDataCtl{
    public static GenerateSPData[][] geneSPJagTbl = {
        Stage1SPData.stg1SPDataTbl,
        Stage0SPData.stg0SPDataTbl,
        Stage1SPData.stg1SPDataTbl,
        Stage1SPData.stg1SPDataTbl,
        Stage1SPData.stg1SPDataTbl,
        Stage1SPData.stg1SPDataTbl,
    };
}



// �X�N���v�^�u���I�u�W�F�N�g
[CreateAssetMenu(menuName = "MyScriptable/Create GeneData")]
public class GeneSObjData : ScriptableObject
{
    public GenerateData[] geneTbl;
};

// �W�F�C�\���p�f�[�^
[System.Serializable]
public class JSonGeneData
{
    public GenerateData[] geneTbl;
};
