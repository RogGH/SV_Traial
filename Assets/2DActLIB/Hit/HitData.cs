using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static HitResult;

public class HitData : MonoBehaviour
{
    HitType type;           // �^�C�v�F�G�APL
    public HitType Type { get { return type; } set { type = value; } }

    HitLayer layer;         // ���C���[�F�U���A�h��
    public HitLayer Layer { get { return layer; } set { layer = value; } }

    // �ڐG���̏���
    private void OnTriggerStay2D(Collider2D collision)
    {
        // �@����E�܂ł̃`�F�b�N
        HitData chkData = collision.gameObject.GetComponent<HitData>();
        if (!CheckCmmHit(chkData)) { return; }

        // �f�[�^���o��
        HitBase atkHB = this.transform.root.gameObject.GetComponent<HitBase>();
        HitBase defHB = chkData.transform.root.GetComponent<HitBase>();
        HitAtkData atkData = this.GetComponent<HitAtkData>();
        HitDefData defData = chkData.GetComponent<HitDefData>();
        // �F�U�����Ă��鑤�����ɓ����I�u�W�F�N�g���U�����Ă��邩�`�F�b�N
        if (!AtkObjCheck(atkHB, atkData, defHB, defData)) { return; }

        // �G�U���������I�I�i�悤�₭�j
        AtkFunc(atkHB, atkData, defHB, defData);
    }

    // �ڐG�I���̏���
    private void OnTriggerExit2D(Collider2D collision)
    {
        // �@����E�܂ł̃`�F�b�N
        HitData chkData = collision.gameObject.GetComponent<HitData>();
        if (!CheckCmmHit(chkData)) { return; }


        // body�^�C�v�͈�x�ڐG���O�ꂽ�������x������̂ŁA����̃`�F�b�N
        // �U���f�[�^�̎�ނ��`�F�b�N
        HitAtkData atk = this.GetComponent<HitAtkData>();
        // body�^�C�v�ȊO�͉������Ȃ�
        if (atk.AtkKind != HitAtkKind.Body) { return; }
        // �q�b�g�����c���Ă��邩�`�F�b�N
        if (atk.AtkList.Count <= 0){ return; }
        // �U���f�[�^�`�F�b�N
        foreach (HitAtkData.EntryData entryData in atk.AtkList)
        {
            // �U���f�[�^���o�^����Ă�����O��
            if (entryData.Obj == collision.gameObject)
            {
                atk.AtkList.Remove(entryData);
                break;
            }
        }
    }

    // ����������U�������Ƃ��̏���
    bool AtkObjCheck(HitBase atkHB, HitAtkData atkData, HitBase defHB, HitDefData defData) {

        // ���U���g�ݒ�
        atkHB.Result.SetAtkFlag(AtkFlag.AtkHit);
        defHB.Result.SetDefFlag(DefFlag.DefHit);

        // �U�����X�g�̐������邩�`�F�b�N
        if (atkData.AtkList.Count > 0) {
            // �U�����X�g�`�F�b�N
            foreach (HitAtkData.EntryData chkData in atkData.AtkList) {
                // null�`�F�b�N
                if (chkData == null) {
                    atkData.AtkList.Remove(chkData);
                    continue;
                }

                // ���Ƀ��X�g�ɓo�^����Ă��邩�`�F�b�N
                if (chkData.Obj.GetInstanceID() != defData.gameObject.GetInstanceID()) { continue; }

                // �U�������I�u�W�F�N�g�̃q�b�g�������Ȃ烊�X�g�O��
                if (!defHB.DefActive ) {                    
                    atkData.AtkList.Remove(chkData);
                    return false;
                }

                // �q�b�g�����߂��Ă�����I��
                if (chkData.HitNum >= atkData.HitNum) { return false; }
                // �R���{�C���^�[�o�����v�Z���A�R���{���Ԃ����Ԃ��߂��Ă��Ȃ���ΏI��
                chkData.ComboCount -= Time.deltaTime;
                if (chkData.ComboCount > 0) { return false; }

                chkData.HitNum++;                                       // �q�b�g�����Z           
                chkData.ComboCount = HitDefine.ComboInterval;           // �C���^�[�o���ݒ� 
                return true;
            }
        }
        else {
            // �U�������I�u�W�F�N�g�̃q�b�g�������Ȃ炱��ȏ�s��Ȃ�
            if (!defHB.DefActive )
            {
                return false;
            }
        }


        // ���X�g�ɐݒ�
        atkData.AtkList.Add(new HitAtkData.EntryData(defData.gameObject, 1));
        return true;
    }

    // �U�����������̏���
    private void AtkFunc(HitBase atkHB, HitAtkData atkData, HitBase defHB, HitDefData defData) {
        // ���肪�s��ꂽ�̂ŁAHitBase��HP�v�Z����
        AtkDamageCalc(atkHB, atkData, defHB, defData);

        // �U���֐����o�^����Ă�������s
        if (atkHB.AtkReactFunc != null) { atkHB.AtkReactFunc(atkData, defData); }
    }

    // �_���[�W�v�Z����
    void AtkDamageCalc(HitBase atkHB, HitAtkData atk, HitBase defHB, HitDefData def)
    {
        AtkFlag setAtkResult = AtkFlag.AtkHit;
        DefFlag setDefResult = DefFlag.DefHit;

        // �_���[�W�v�Z
        int value = atk.AtkPow;

        // �h��͂�����
        int defPow = def.DefPow;

        // �ڐG�������W�̒��Ԉʒu���v�Z�i���������̍��W�j
        CalcAtkCenterPos(atk, defHB, def);

        // �����Ƃ��̌v�Z�i���ɂȂ��j
        if (value > 0)
        {
            // ���G�����`�F�b�N
            if (def.DefKind == HitDefKind.Invincivle)
            {
                // ���G��Ԃɉ�����
                setAtkResult |= AtkFlag.AtkDoneInvincible;
                // ���G��ԂȂ̂ŁA�_���[�W�v�Z�͍s��Ȃ��i�q�b�g�͂��Ă���j
                setDefResult |= DefFlag.DefDoneInvincible;
            }
            // ��𒆂��`�F�b�N
            else
            if (def.DefKind == HitDefKind.Avoid)
            {
                // �����Ԃɉ�����
                setAtkResult |= AtkFlag.AtkDoneAboid;
                // �����ԂȂ̂ŁA�_���[�W�v�Z�͍s��Ȃ��i�q�b�g�G�t�F�N�g�Ƃ��o���Ȃ��Ƃ��j
                setDefResult |= DefFlag.DefDoneAvoid;
            }
            // �ʏ�_���[�W
            else
            {
                if (def.DefKind == HitDefKind.CheckOnly)
                {
                    // �`�F�b�N����
                    // �`�F�b�N����ɍU��������
                    setAtkResult |= AtkFlag.AtkDoneCheck;
                    // �`�F�b�N����Ŗh�䂵��
                    setDefResult |= DefFlag.DefDoneCheck;
                }
                else {
                    // �_���[�W����
                    // �N���e�B�J��������v�Z
                    if (atk.CriticalHit > 0) {
                        int rand = Random.Range(0, 100);
                        if (rand <= atk.CriticalHit)
                        {
                            // �N���e�B�J������
                            // �_���[�W�v�Z�i1.5�{���펖�̃N���e�B�J���_���[�W�A�b�v�j
                            float criDmgRate = HitDefine.CriDmgRateDef + (atk.CriticalDmg/100);
                            // 
                            value = Mathf.RoundToInt(value * criDmgRate);

                            setAtkResult |= AtkFlag.AtkDoneCritical;
                        }
                    }

                    // �h��͌v�Z
                    value -= defPow;
                    if (value < 1) { value = 1; }            // �Œ�ۏ؂͂P

                    // HP�����炷
                    defHB.HP -= value;
                    if (defHB.HP < 0) { defHB.HP = 0; }      // ���S������O

                    // �U��������
                    setAtkResult |= AtkFlag.AtkDoneDamage;
                    // �_���[�W���󂯂�
                    setDefResult |= DefFlag.DefDoneDamage;

                    // �q�b�g�e�L�X�g�\��
                    string setText = value.ToString();
                    GameObject obj =
                        Instantiate((GameObject)Resources.Load("HitDispText"), defHB.Result.HitPos, Quaternion.identity);
                    Color setCol = Color.white;
                    if ( def.Layer == HitLayer.Player) {setCol = Color.red; }

                    bool criticalFlag = (setAtkResult & AtkFlag.AtkDoneCritical) != 0 ? true : false;
                    if (criticalFlag)
                    {
                        setCol = Color.yellow;
                        setText += "!"; 
                    }
                    obj.GetComponent<HitDispText>().SetString(setText, setCol);

                    // �_���[�W�v�Z
                    DmgCalcSys.Ins.AddTotalDmg(value);

                }
            }
        }
        else if (value < 0)
        {
            // �񕜂�����
            setAtkResult = AtkFlag.AtkDoneDamage;
            // ��
            defHB.HP += value;
            if (defHB.HP > defHB.MaxHP)
            {
                defHB.HP = defHB.MaxHP;                 // �ő�HP�𒴂��Ȃ�
            }
            // �_���[�W���󂯂�
            setDefResult |= DefFlag.DefDoneHeal;
        }
        else {
            // �m�[�_���[�W
            setDefResult |= DefFlag.DefDoneNoDamage;
        }

        // �U����
        {
            // �U������
            atkHB.Result.SetAtkFlag(setAtkResult);
            // SE��炵�Ă݂�
            if (atk.layer == HitLayer.Player) {
                string seName;
                if (atkHB.Result.CheckAtkFlag(AtkFlag.AtkDoneCritical)) {
                    seName = "Hit1";
                }
                else
                {
                    seName = "Hit2";
                }
                // �Đ����͂T�܂łɂ��Ƃ�
                SeManager.Instance.Play(seName, 5);
            }
        }

        // �h�䑤
        {
            // �_���[�W
            defHB.Result.Damage = atk.AtkPow;
            // ���U���g�ݒ�
            defHB.Result.SetDefFlag(setDefResult);
        }
    }

    // �ڐG���W�̌v�Z
    void CalcAtkCenterPos(HitAtkData atk, HitBase defHB, HitDefData def)
    {
        // �R���C�_�[���m�̒��Ԉʒu���v�Z�iY���W�����킹��j
        Collider2D atkCol = def.GetComponent<Collider2D>();
        Vector3 atkPos = atk.transform.position;
        atkPos.y += atkCol.offset.y;

        Collider2D defCol = def.GetComponent<Collider2D>();
        Vector3 defPos = def.transform.position;
        defPos.y += defCol.offset.y;

        // X�����N�\�����A�^������̏ꍇ�A���W�����������Ȃ�̂ŁA
        // X���W��h�䑤�̒[�����ɍ��킹��
        // �Ȃ��AY���W�������A�^������͖��Ή�
        Vector3 calcPos = Vector3.Lerp(atkPos, defPos, 0.5f);
        float width = defCol.bounds.size.x / 2;
        if (defPos.x - width > calcPos.x || defPos.x + width < calcPos.x)
        {
            float dir = calcPos.x < defPos.x ? -1 : 1;
            calcPos.x = defPos.x + width * dir;
        }

        // �ڐG�������W���m�̒��Ԓn�_��ۑ�
        defHB.Result.HitPos = calcPos;
    }

    // ���ʃ`�F�b�N
    private bool CheckCmmHit(HitData chkData) {
        // �@�ڐG���������HitData�������Ă��邩�`�F�b�N
        if (!chkData) { return false; }

        // �U����VS�h��̌`�̂ݍs��
        // �A���g���U�����`�F�b�N
        if (this.Type != HitType.ATK) { return false; }
        // �B�U�����̃f�[�^���ݒ肳��Ă��邩�`�F�b�N
        HitBase atkHB = this.transform.root.gameObject.GetComponent<HitBase>();
        if (atkHB == null) { Debug.Log(this + "atk HitBase is None"); }
        if (!atkHB.AtkActive) { return false; }


        // �C�ڐG�������肪�h�䂩�`�F�b�N
        if (chkData.Type != HitType.DEF) { return false; }
        // �D�h�䑤�̃f�[�^���ݒ肳��Ă��邩�`�F�b�N
        HitBase defHB = chkData.transform.root.GetComponent<HitBase>();
        if (defHB == null) { Debug.Log(chkData + "target HitBase is None"); return false; }

        // �E���C���[�ł̓����蔻��`�F�b�N
        if (!AtkLayerHitCheck(this.Layer, chkData.Layer)) { return false; }

        // �F����HP�O�Ȃ画�肵�Ȃ�����
        if (atkHB.HP <= 0 || defHB.HP <= 0) { return false; }


        return true;
    }

    // ���C���[�ɂ��U������`�F�b�N
    bool AtkLayerHitCheck(HitLayer aktLayer, HitLayer defLayer)
    {
        switch (aktLayer)
        {
            // PL�U�� VS EM�h��܂��͒����h��
            case HitLayer.Player:
                if (defLayer == HitLayer.Enemy || defLayer == HitLayer.Neutral) { return true; }
                break;
            // EM�U�� VS PL�h��܂��͒����h��
            case HitLayer.Enemy:
                if (defLayer == HitLayer.Player || defLayer == HitLayer.Neutral) { return true; }
                break;
            // �����U�� VS PL�h��܂��͓G�h��
            case HitLayer.Neutral:
                if (defLayer == HitLayer.Player || defLayer == HitLayer.Enemy) { return true; }
                break;
        }
        return false;
    }
}