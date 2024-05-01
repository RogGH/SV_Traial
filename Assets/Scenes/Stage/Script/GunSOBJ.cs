using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(menuName = "MyScriptable/Create GunSOBJ")]
public class GunSOBJ : ScriptableObject
{
	[HideInInspector] public int id = 0;
	[HideInInspector] public string wName = "HundGun";

	[HideInInspector]public string key1 = "Get";
	[HideInInspector]public float value1 = 1;

	[TextArea(10, 10)]
	public string comment =
		  "Key��Dmg,Recast,Speed,CriHit,Num�������\n"
		+ "value�͐��l�����\n"
		+ "string�͐����̕��͂����"
		+ "�����͗ၡ\n"
		+ "�U����50%UP�Fkey = Dmg, value = 50\n"
		+ "�Ďg�p���x25%UP�Fkey = Recast, value =25\n"
		+ "�e���Q���Fkey = Num, value = 2\n"
		+ "�e��50%UP�Fkey = Speed, value = 50\n"
		+ "�N����50%UP�Fkey = CriHit, value = 50\n"
		;

	[Header("LV2�ݒ�")]
	[SerializeField] public string key2 = "Dmg";
	[SerializeField] public float value2 = 50;
	[SerializeField] public string string2 = "���x���Q�̐���";

	[Header("LV3�ݒ�")]
	[SerializeField] public string key3 = "Recast";
	[SerializeField] public float value3 = 25;
	[SerializeField] public string string3 = "���x���R�̐���";

	[Header("LV4�ݒ�")]
	[SerializeField] public string key4 = "CriHit";
	[SerializeField] public float value4 = 50;
	[SerializeField] public string string4 = "���x���S�̐���";

	[Header("LV5�ݒ�")]
	[SerializeField] public string key5 = "Num";
	[SerializeField] public float value5 = 2;
	[SerializeField] public string string5 = "���x���T�̐���";

	[Header("LV6�ݒ�")]
	[SerializeField] public string key6 = "Speed";
	[SerializeField] public float value6 = 50;
	[SerializeField] public string string6 = "���x���U�̐���";

	[Header("LV7�ݒ�")]
	[SerializeField] public string key7 = "Num";
	[SerializeField] public float value7 = 3;
	[SerializeField] public string string7 = "���x���V�̐���";
}
