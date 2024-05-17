using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitDefData : HitData
{
    [SerializeField] HitLayer defLayer;                      // ���C���[
    [SerializeField] HitElement defEle = HitElement.None;    // �����i�_���[�W�v�Z�p�����j
    [SerializeField] HitDefKind defKind;                     // ���
    [SerializeField] HitDefPlus defPlus;                    // �h��t��
    [SerializeField] HitShape defShape = HitShape.Rect;     // �`��i�����`�̂݁j
    [SerializeField] int defPow;                           // �h��́i�h��́j

    private List<GameObject> defList =  new List<GameObject>();

    public HitElement DefEle { get { return defEle; } }
    public HitDefKind DefKind { get { return defKind; } }
    public HitDefPlus DefPlus { get { return defPlus; } }
    public int DefPow { get { return defPow; } set { defPow = value; } }

    BoxCollider2D box;
    HitBase hb;

    void Awake(){
        // �e�ݒ�
        this.Type = HitType.DEF;
        this.Layer = defLayer;

        // �q�b�g�x�[�X�擾
        hb = HitDefine.getHitBase(this.gameObject);
        // �h��̂�
        defList.Clear();

#pragma warning disable CS0162
        if (HitDefine.DebugDisp){
            box = GetComponent<BoxCollider2D>();
        }
#pragma warning disable CS0162
    }

    void Update()
    {
        // �\��
        if (HitDefine.DebugDisp){
            if (hb.DefActive){
                if (defShape == HitShape.Rect){
                    HitDefine.RectDisp(transform.root.localScale.x,
                        box.offset,
                        box.size * transform.root.localScale,
                        transform.root.position,
                        Color.green);
                }
            }
        }
    }
}
