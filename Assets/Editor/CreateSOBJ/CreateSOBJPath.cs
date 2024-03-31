using UnityEngine;

[CreateAssetMenu(menuName = "MyScriptable/CreateSOBJPath")]
public class CreateSOBJPath : ScriptableObject
{
    [Header("GSS��URL")]
    public string Sheet_URL;

    [Header("GAS��URL")]
    public string GAS_URL;

    [Header("�p�X�ݒ�SOBJ�Fpath")]
    public string SettingSObj_PATH;

    [Header("�쐬�f�[�^�ۑ��ꏊ�Fpath")]
    public string CreateData_PATH;
}