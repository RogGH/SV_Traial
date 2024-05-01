using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponFlag : MonoBehaviour
{
    private static WeaponFlag ins;
    public static WeaponFlag Ins
    {
        get
        {
            if (ins == null)
            {
                ins = (WeaponFlag)FindObjectOfType(typeof(WeaponFlag));
                if (ins == null) { Debug.LogError(typeof(WeaponFlag) + "is nothing"); }
            }
            return ins;
        }
    }

    [Header("�n���h�K��")] [SerializeField] bool Shot = false;
    [Header("�h����")] [SerializeField] bool Drill = false;
    [Header("��]�̂�����")] [SerializeField] bool Saw = false;
    [Header("�Ή�����")] [SerializeField] bool Frame = false;
    [Header("�^���b�g")] [SerializeField] bool Turret = false;
    [Header("�g���C")] [SerializeField] bool WaveCannon = false;

    [HideInInspector]
    public List<bool> FlagList;

    void Start()
    {
        FlagList.Add(Shot);
        FlagList.Add(Drill);
        FlagList.Add(Saw);
        FlagList.Add(Frame);
        FlagList.Add(Turret);
        FlagList.Add(WaveCannon);
    }
}
