using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PLHitTest : MonoBehaviour
{
   HitBase hb;     // �R���|�[�l���g�p�ϐ�
        
    // Start is called before the first frame update
    void Start()
    {
        hb = GetComponent<HitBase>();           // Hitbase�R���|�[�l���g�擾
        hb.Setup(Damage, Die);                  // HitBase������
    }

    // Update is called once per frame
    void Update()
    {
        if (hb.PreUpdate()) { return; }         // HitBase�A�b�v�f�[�g�O�����i���S�����炱��ȏ�s��Ȃ��j

        // ���E�ړ�
        Vector3 pos = transform.position;
        float dir = Input.GetAxis("Horizontal");
        pos.x += 0.1f * dir;
        transform.position = pos;
    }

    private void LateUpdate()
    {
        hb.PostUpdate();                        // HitBase�A�b�v�f�[�g�㏈��
    }

    void Damage() {
        Debug.Log("�_���[�W�󂯂܂���");
        // ���G�_�ŃR���[�`���N��
        this.StartCoroutine("DmgCoroutine");
    }

    IEnumerator DmgCoroutine()
    {
        hb.SetDefActive(false);                               // HitBase�̖h�䖳����

        int count = 10;
        while (count > 0){
            //�����ɂ���
            SpriteRenderer sr = GetComponent<SpriteRenderer>();
            sr.color = new Color(1, 1, 1, 0);
            //0.05�b�҂�
            yield return new WaitForSeconds(0.05f);
            //���ɖ߂�
            sr.color = new Color(1, 1, 1, 1);
            //0.05�b�҂�
            yield return new WaitForSeconds(0.05f);
            count--;
        }

        hb.SetDefActive(true);                               // HitBase�̖h�䖳����
    }


    bool Die() {
        Debug.Log("���S");
        Destroy(this.gameObject);
        return true;    // �����I��
    }


    // �O������R���|�[�l���g�擾
    public HitBase getHitBase() {
        return hb;
    }
}
