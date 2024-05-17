using UnityEngine;

/*
    �q�b�g�̏�Ԃɂ���

    Active���
    �A�^��������s�����s��Ȃ����B
    �ڐG������s��Ȃ��Ȃ�
    �i�ȒP�ɖ��G������s���Ȃ�R���j
 */ 



// �[�[�[�[�[�[�[�[�[�[�[�[���ʁ[�[�[�[�[�[�[�[�[�[�[�[�[�[�[�[
// �q�b�g��`
static class HitDefine{
    public const float ComboInterval = 0.3f;        // �R���{�Ԋu�i���͈ꗥ�Őݒ�j

    public const float CriDmgRateDef = 1.5f;        // �N���e�B�J���_���[�W�i��{�j

    public const bool DebugDisp = false;             // �f�o�b�O�\���i�܂��g�k��Ή��j


    // �q�b�g�x�[�X�擾
    public static HitBase getHitBase(GameObject obj) {
        HitBase hb = obj.transform.root.GetComponent<HitBase>();
        if (hb == null) { Debug.Log("HitBase is None!"); }
        return hb;
    }


    // ��`�\��
    public static void RectDisp(float dir, Vector3 offset, Vector3 size, Vector3 pos, Color col)
    {
        Vector3 center = pos;

        center.x += offset.x * (dir < 0 ? -1 : 1);
        center.y += offset.y;

        float hx = size.x / 2;
        float hy = size.y / 2;

        Vector3 ul = center; ul.x -= hx; ul.y += hy;
        Vector3 ur = center; ur.x += hx; ur.y += hy;
        Vector3 dl = center; dl.x -= hx; dl.y -= hy;
        Vector3 dr = center; dr.x += hx; dr.y -= hy;

        Debug.DrawLine(ul, ur, col);
        Debug.DrawLine(ul, dl, col);
        Debug.DrawLine(dl, dr, col);
        Debug.DrawLine(ur, dr, col);
    }
}

// �^�C�v�i�U�����h�䂩�j
public enum HitType{ ATK, DEF, };
// ���C���[�iPL,�G�A�����j
public enum HitLayer{ Player, Enemy, Neutral, };

// �����i�e�Q�[���ɂČʐݒ�j
// ��̂ݐݒ�
public enum HitElement { 
    None,
    Fire,
    Thunder,
    Ice,
};

// �`��
public enum HitShape {
    Rect,               // ��`
    Circle,             // �~

    Num
};


// �[�[�[�[�[�[�[�[�[�[�[�[���ʁ[�[�[�[�[�[�[�[�[�[�[�[�[�[�[�[


// �[�[�[�[�[�[�[�[�[�[�[�[�U����p�[�[�[�[�[�[�[�[�[�[�[�[�[�[
// �U�����
public enum HitAtkKind {
    Body,   // �́i��x�G�ꂽ��A����Ă���G���Ƃ�����x������j
    Shell,  // �e�i��x�G�ꂽ��A����Ă���G��Ă�������Ȃ��j
};

// �U���t���i������΂��Ƃ��A�ђʂƂ��A���̕ӂ�ݒ肵���肷��\��j
// �����ݒ�\
public enum HitAtkSP {
    None    = 0,            // ����
    Blow    = (1 << 0),     // �ӂ��Ƃ΂�
    Through = (1 << 1),     // �ђ�
};

// �[�[�[�[�[�[�[�[�[�[�[�[�U����p�[�[�[�[�[�[�[�[�[�[�[�[�[�[


// �[�[�[�[�[�[�[�[�[�[�[�[�h���p�[�[�[�[�[�[�[�[�[�[�[�[�[�[
// �h���ށi���˂Ƃ��A���������̂�ݒ肵���肷��\��j
public enum HitDefKind {
    None,               // �Ȃ�
    Invincivle,         // ���G�i�����邯�ǃ_���[�W�v�Z���Ȃ��j
    Avoid,              // ����i�����邯�ǃ_���[�W�v�Z���Ȃ��j
    CheckOnly,          // �`�F�b�N�p�i���������낤����Ƀ`�F�b�N�����j
};

// �h��ǉ����ʁi�j
public enum HitDefPlus {
    None,
    Refrect,
};

// �[�[�[�[�[�[�[�[�[�[�[�[�h���p�[�[�[�[�[�[�[�[�[�[�[�[�[�[


// �q�b�g����
public class HitResult
{
    public enum AtkFlag {
        None = 0,
        AtkDoneDamage       = (1 << 1),     // �_���[�W��^���� 
        AtkDoneHeal         = (1 << 2),     // �񕜂����� 
        AtkDoneNoDamage     = (1 << 3),     // �m�[�_��������
        AtkDoneInvincible   = (1 << 4),     // ���G���̑���ɍU������
        AtkDoneAboid        = (1 << 5),     // ��𒆂̑���ɍU������
        AtkDoneCheck        = (1 << 6),     // �`�F�b�N����ɍU������
        AtkHit              = (1 << 7),     // �h��ƐڐG���Ă���
        AtkDoneCritical     = (1 << 8),     // �N���e�B�J������
        End
    };
    public enum DefFlag
    {
        None = 0,
        DefDoneDamage       = (1 << 1),     // �_���[�W���󂯂�
        DefDoneHeal         = (1 << 2),     // �񕜂��󂯂�
        DefDoneNoDamage     = (1 << 3),     // �m�[�_���[�W���󂯂�
        DefDoneInvincible   = (1 << 4),     // ���G���Ɏ󂯂�
        DefDoneAvoid        = (1 << 5),     // ��𒆂Ɏ󂯂�
        DefDoneCheck        = (1 << 6),     // �`�F�b�N���������
        DefHit              = (1 << 7),     // �U���ƐڐG���Ă���

        End
    };

    int damage;
    Vector3 hitPos;
    AtkFlag atkFlag;
    DefFlag defFlag;

    // �v���p�e�B
    public int Damage { get { return damage; } set { damage = value; } }
    public Vector3 HitPos { get { return hitPos; } set { hitPos = value; } }

    // �t���O�֘A
    bool checkFlag(int flag) { return flag != 0 ? true : false; }
    // �U���t���O�֘A
    public bool CheckAtkFlag(AtkFlag flag) { return checkFlag((int)(atkFlag & flag)); }
    public void SetAtkFlag(AtkFlag setFlag) { atkFlag |= setFlag; }
    public void ClearAtkFlag() { atkFlag = 0; }
    // �h��t���O�֘A
    public bool CheckDefFlag(DefFlag flag) { return checkFlag((int)(defFlag & flag)); }
    public void SetDefFlag(DefFlag setFlag) { defFlag |= setFlag; }
    public void ClearDefFlag() { defFlag = 0; }
    // �S�t���O
    public void ClearAllFlag() { ClearAtkFlag(); ClearDefFlag(); }
};
