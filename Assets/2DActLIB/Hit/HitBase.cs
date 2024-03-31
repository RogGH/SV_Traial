using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static HitResult;
/*
	�q�b�g�̎d�l

	HitBase��e�ɃA�^�b�`����B
	�ő�̗͂�ݒ�Unity�Őݒ肷��B

	�q���Ƃ���HitAtkData��HitDefData�����������I�u�W�F�N�g�𐶐�����


	�v���C���[��RigidBody2D��SleepMode��NeverSleep�ɂ��Ă����������ǂ�
	�i�ڐG��������Ɠ�����Ȃ��Ȃ�j
 */

public class HitBase : MonoBehaviour
{
	[SerializeField] int maxHp = 1;         // �ő�̗�
	[SerializeField] int hp = 0;            // ���݂̗̑�

	[SerializeField] bool atkActive = true; // �U���L��
	[SerializeField] bool defActive = true; // �h��L��

	HitResult result = new HitResult();     // �q�b�g����

	// �_���[�W���֐�
	public delegate void DamageFunc();
	private DamageFunc dmgFunc;
	// ���S���֐�
	public delegate bool DieFunc();
	private DieFunc dieFunc;
	// �ڐG���֐�
	public delegate void AtkReaction(HitAtkData atk, HitDefData def);
	private AtkReaction setAtkReactFunc;

	// �J�n
	private void Awake() {
		hp = maxHp;
	}

	// ---------------------------------------------------------------
	// �O���T�[�r�X��������
	// ---------------------------------------------------------------
	// �v���p�e�B
	public int HP { get { return hp; } set { hp = value; } }			// HP
	public int MaxHP { get { return maxHp; } set { maxHp = value; } }	// �ő�HP
	public HitResult Result {											// �q�b�g����
		get { return result; } set { result = value; }
	}
	public AtkReaction AtkReactFunc {                               // �U�����̊֐�
		get { return setAtkReactFunc; } set { setAtkReactFunc = value; }
	}

	// ���T�[�r�X�i���̂������������j
	public bool AtkActive { get { return atkActive; } set { atkActive = value; } }
	public bool DefActive { get { return defActive; } set { defActive = value; } }

	// ---------------------------------------------------------------
	// �K�{�֐�
	// ---------------------------------------------------------------
	// �q�b�g�x�[�X������
	public void Setup(DamageFunc damage, DieFunc die) {
		dmgFunc = damage;
		dieFunc = die;
	}

	// �A�b�v�f�[�g�揈��
	public bool PreUpdate()
	{
		// ���S���֐�
		if (CheckDie()) {
			if (dieFunc != null) { return dieFunc(); }
		}
		// �_���[�W���֐�
		else if (CheckDamage()) {
			if (dmgFunc != null) { dmgFunc(); }
		}
		return false;       // 
	}

	// �A�b�v�f�[�g�㏈��
	public void PostUpdate() {
		result.ClearAllFlag();
	}


	// ---------------------------------------------------------------
	// ���̑��T�[�r�X
	// ---------------------------------------------------------------
	// ���S������
	public bool CheckDie() { return HP <= 0 ? true : false; }


	// �U���Ń_���[�W��^������
	public bool CheckAttackDamage() { return Result.CheckAtkFlag(AtkFlag.AtkDoneDamage); }
	// �U���ŉ�������ɐڐG������
	public bool CheckAttackHit() { return Result.CheckAtkFlag(AtkFlag.AtkHit); }


	// �h��Ń_���[�W���󂯂���
	public bool CheckDefDamage() { return Result.CheckDefFlag(DefFlag.DefDoneDamage); }
	// �h��`�F�b�N�����������
	public bool CheckDefCheckOnly() { return Result.CheckDefFlag(DefFlag.DefDoneCheck); }
	// �h��ŉ�������ɐڐG������
	public bool CheckDefHit() { return Result.CheckDefFlag(DefFlag.DefHit); }



	// �Â����c
	public bool CheckDamage() { return Result.CheckDefFlag(DefFlag.DefDoneDamage); }
	public bool CheckAttack() { return Result.CheckAtkFlag(AtkFlag.AtkDoneDamage); }


	// ---------------------------------------------------------------
	// ����ꊇ�ݒ�
	// ---------------------------------------------------------------
	/*	
	 	�q�������U���A�h�䓖������ꊇ�Őݒ�o����悤�Ȑݒ�
		�ʂŐݒ肷��ꍇ�͎��O�ōs��
	*/

	// ����L�����i�ڐG���莩�̂��s��Ȃ��Ȃ�j
	public void SetAtkActive(bool value) { atkActive = value; }
	public void SetDefActive(bool value) { defActive = value; }
}
