using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ItemFlag : MonoBehaviour
{
    private static ItemFlag ins;
    public static ItemFlag Ins
    {
        get
        {
            if (ins == null)
            {
                ins = (ItemFlag)FindObjectOfType(typeof(ItemFlag));
                if (ins == null) { Debug.LogError(typeof(ItemFlag) + "is nothing"); }
            }
            return ins;
        }
    }

    [Header("�ő�HP�A�b�v")][SerializeField] bool MaxHPUp = false;
    [Header("�����񕜃A�b�v")] [SerializeField] bool AutoHealUp = false;
    [Header("�h��̓A�b�v")] [SerializeField] bool DefenseUp = false;
    [Header("�ړ����x�A�b�v")] [SerializeField] bool MoveSpdUp = false;
    [Header("���W�͈̓A�b�v")] [SerializeField] bool MagnetUp = false;
    [Header("�o���l�A�b�v")] [SerializeField] bool ExpUp = false;
    [Header("���A�b�v")] [SerializeField] bool GoldUp = false;
    [Header("�^�A�b�v")] [SerializeField] bool LuckUp = false;
    [Header("�^�_���[�W�A�b�v")] [SerializeField] bool DamageUp = false;
    [Header("�N���e�B�J���A�b�v")] [SerializeField] bool CriticalUp = false;
    [Header("�U�����ԃA�b�v")] [SerializeField] bool AtkTimeUp = false;
    [Header("�e���A�b�v")] [SerializeField] bool ShotNumUp = false;
    [Header("�U���͈̓A�b�v")] [SerializeField] bool AtkAreaUp = false;
    [Header("�e���A�b�v")] [SerializeField] bool AtkMoveSpdUp = false;
    [Header("���L���X�g�A�b�v")] [SerializeField] bool RecastUp = false;

    [HideInInspector]
    public List<bool> FlagList;

	private void Start()
	{
        FlagList.Add(MaxHPUp);
        FlagList.Add(AutoHealUp);
        FlagList.Add(DefenseUp);
        FlagList.Add(MoveSpdUp);
        FlagList.Add(MagnetUp);
        FlagList.Add(ExpUp);
        FlagList.Add(GoldUp);
        FlagList.Add(LuckUp);
        FlagList.Add(DamageUp);
        FlagList.Add(CriticalUp);
        FlagList.Add(AtkTimeUp);
        FlagList.Add(ShotNumUp);
        FlagList.Add(AtkAreaUp);
        FlagList.Add(AtkMoveSpdUp);
        FlagList.Add(RecastUp);
    }
};