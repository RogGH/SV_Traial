using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitAtkData : HitData
{
    // �e�ݒ�
    [SerializeField] HitLayer atkLayer;                     // ���C���[
    // �U����p
    [SerializeField] int atkPow = 1;                        // �U���́i�U���́j
    [SerializeField] int criHitRate = 0;                    // �N���e�B�J�����i�O�`�P�O�O���j
    [SerializeField] int criDmgRate = 0;                    // �N���e�B�J�����Z�_���[�W�i���j
    [SerializeField] HitElement atkEle = HitElement.None;   // �����i�v�Z�p�����j
    [SerializeField] HitAtkKind atkKind = HitAtkKind.Body;  // ��ށi�ڐG���O�ꂽ��ǂ��Ȃ邩�j
    [SerializeField] HitAtkSP   atkSP = HitAtkSP.None;      // �t���i����ȃ^�C�v�j
    [SerializeField] int hitNum = 1;                        // �q�b�g��
    [SerializeField] HitShape atkShape = HitShape.Rect;     // �`��i�����`�̂݁j

    List<EntryData> atkList = new List<EntryData>();

    // �Q�b�^
    public int AtkPow { get { return atkPow; } set { atkPow = value; } }
    public int CriticalHit { get { return criHitRate; } set { criHitRate = value; } }
    public int CriticalDmg { get { return criDmgRate; } set { criDmgRate = value; } }
    public HitElement AtkEle { get { return atkEle; } }
    public HitAtkKind AtkKind { get { return atkKind; } }
    public HitAtkSP AtkSP { get { return atkSP; } }
    public int HitNum { get { return hitNum; } }
    public List<EntryData> AtkList { get { return atkList; } set { atkList = value; } }

    BoxCollider2D box;
    HitBase hb;

    // �G���g���[�f�[�^
    public class EntryData {
        GameObject obj;
        int hitNum;
        float comboCount;
        public EntryData(GameObject obj, int hitNum){
            this.obj = obj;
            this.hitNum = hitNum;
            this.comboCount = HitDefine.ComboInterval;
        }
        public GameObject Obj { get { return obj; } }
        public int HitNum { get { return hitNum; } set { hitNum = value; } }
        public float ComboCount { get { return comboCount; } set { comboCount = value; } }
    };

    void Awake() {
        // �e�ݒ�
        this.Layer = atkLayer;
        this.Type = HitType.ATK;
        // �U���̂�
        atkList.Clear();

#pragma warning disable CS0162
        if (HitDefine.DebugDisp) {
            box = GetComponent<BoxCollider2D>();
        }
#pragma warning disable CS0162
        hb = HitDefine.getHitBase(gameObject);
    }

    void Update(){
        // ���g���������`�F�b�N
        if ( hb != null ) {
            if (!hb.AtkActive) {
                if (AtkList.Count > 0) {
                    AtkList.Clear();
                }
            }
        }

        // �����I�u�W�F�N�g�����X�g��
        if (AtkList.Count > 0) 
        {
            // �I�u�W�F�N�g���S�`�F�b�N
            List<EntryData> temp = new List<EntryData>();
            foreach (var data in AtkList)
            {
                if (data.Obj == null)
                {
                    // �������X�g�֓o�^
                    temp.Add(data);
                }
            }

            // �������X�g�����邩�`�F�b�N
            if (temp.Count > 0)
            {
                foreach (var data in temp)
                {
                    // �U�����X�g�������
                    AtkList.Remove(data);
                }
            }
        }

#pragma warning disable CS0162
        // �\��
        if (HitDefine.DebugDisp) {
            if (hb.AtkActive){
                if (atkShape == HitShape.Rect){
                    HitDefine.RectDisp(
                        transform.root.localScale.x,
                        box.offset,
                        box.size * transform.root.localScale,
                        transform.root.position,
                        Color.red);
                }
            }
        }
#pragma warning disable CS0162
    }
}
