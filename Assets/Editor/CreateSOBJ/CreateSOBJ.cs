using UnityEngine;
using UnityEngine.Networking;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;
#endif
using System;
using System.IO;
    
public class CreateSOBJ : EditorWindow
{
    //�p�X���܂Ƃ߂��X�N���v�^�u���I�u�W�F�N�g�̃p�X
    private readonly string PathSOBJ = "Assets/Editor/CreateSOBJ/Resources/CreateSOBJPath.asset";
    private CreateSOBJPath pathSOBJ;

    const string EmDataPath = "EnemyData";
    const string WepInitDataPath = "WeaponInitData";
    const string WepLVUPDataPath = "WeaponLVUPData";

    // ������
    //[MenuItem("Tools/Create SObj")]
    //static void Init()
    //{
    //    CreateSOBJ window = (CreateSOBJ)EditorWindow.GetWindow(typeof(CreateSOBJ));
    //    window.Show();
    //}

    // �\���֘A
    private void OnGUI()
	{
        GUILayout.Label("SObj Creator", EditorStyles.boldLabel);
        GUILayout.Label("");

        // �p�X�ݒ���J��
        GUILayout.Label("���p�X�֘A�̐ݒ�");
        if (GUILayout.Button("Open PathSOBJ"))
        {
            OpenPath();
        }

        // URL�J��
        GUILayout.Label("���X�v���b�g�V�[�g���J��");
        if (GUILayout.Button("Open Sheets"))
        {
            OpenURL();
        }

        // �G�f�[�^���_�E�����[�h
        GUILayout.Label("���G�f�[�^�_�E�����[�h");
        if (GUILayout.Button("Enemy Data Download"))
        {
            DownLoadData(EmDataPath);
        }

        // �{�^���������� ScriptableObject ���쐬
        GUILayout.Label("���G�f�[�^����");
        if (GUILayout.Button("Create EnemySObj ALL"))
        {
            CreateEnemySObj();
        }


        // ����f�[�^���_�E�����[�h
        GUILayout.Label("��PL���평���p�����[�^�_�E�����[�h");
        if (GUILayout.Button("WeaponInitData Download"))
        {
            DownLoadData("WeaponInitData");
        }

        // �{�^���������� ScriptableObject ���쐬
        GUILayout.Label("��PL���평���p�����[�^����");
        if (GUILayout.Button("Create WeaponInitSObj ALL"))
        {
            CreateWeaponInitObj();
        }

        // ���탌�x���A�b�v�֘A���_�E�����[�h
        GUILayout.Label("��PL���탌�x���A�b�v�ݒ�_�E�����[�h");
        if (GUILayout.Button("Weapon LVUP Setting Download"))
        {
            DownLoadData("WeaponLVUPData");
        }

        // �{�^���������� ScriptableObject ���쐬
        GUILayout.Label("��PL���탌�x���A�b�v�f�[�^����");
        if (GUILayout.Button("Create Weapon LVUP SOBJ ALL"))
        {
            CreateWeaponLVUPObj();
        }
    }

    // �p�X�Đݒ�
    void ResetPath() {
        pathSOBJ = AssetDatabase.LoadAssetAtPath<CreateSOBJPath>(PathSOBJ);
    }

    // �p�X�I�u�W�F�N�g�J��
    void OpenPath()
    {
        ResetPath();
        UnityEngine.Object obj = AssetDatabase.LoadAssetAtPath<UnityEngine.Object>(pathSOBJ.SettingSObj_PATH);
        if (obj != null) {
            // �t�@�C����I��(Project�E�B���h�E�Ńt�@�C�����I����ԂɂȂ�)
            Selection.activeObject = obj;
        }
    }

    // �V�[�gURL�J��
    void OpenURL() {
        ResetPath();
        //�p�X�̃T�C�g���J��
        Application.OpenURL(pathSOBJ.Sheet_URL);
    }



    // �f�[�^�_�E�����[�h
    void DownLoadData(string key)
    {
        ResetPath();

        string mystringValue = key;
        string requestUrl = pathSOBJ.GAS_URL + "?myString=" + UnityWebRequest.EscapeURL(mystringValue);

        //URL�փA�N�Z�X

        UnityWebRequest req = UnityWebRequest.Get(requestUrl);

        req.SendWebRequest();

        while (!req.isDone)
        {
            // ���N�G�X�g����������̂�ҋ@
        }

        if (req.result != UnityWebRequest.Result.ProtocolError && req.result != UnityWebRequest.Result.ConnectionError)
        {
            Debug.Log("Success");
            string str = req.downloadHandler.text;
            Debug.Log(str);
            string filePath = pathSOBJ.CreateData_PATH;

            filePath += '/' + key + '/' + key + ".json";
            File.WriteAllText(filePath, str);
            Debug.Log(filePath + " Create!!");
        }
        else {
            Debug.Log("Error");
        }
    }


    // �f�[�^�z�񕶎���̍쐬
    string CreateDataTableString(string original)
    {
        string table = "{\"table\":";
        table += original;
        table += '}';
        return table;
    }
    // enum�̃X�N���v�g�𐶐����郁�\�b�h
    public static void GenerateEnum(string enumName, List<string> values, string path)
    {
        string enumContent = $"public enum {enumName}\n{{\n";

        // string�z�񂩂�擾�����l��񋓌^�̗v�f�Ƃ��Ēǉ�
        foreach (var value in values)
        {
            enumContent += $"    {value},\n";
        }

        enumContent += "}";

        // enum�̒�`���X�N���v�g�Ƃ��ĕۑ�
        Debug.Log(enumContent);
        // �����̃t�@�C�������݂���ꍇ�A�㏑��
        System.IO.File.WriteAllText(path, enumContent);
        // ��������enum�����t���b�V��
        UnityEditor.AssetDatabase.Refresh();
    }


    // �G�r�n�a�i�쐬
    void CreateEnemySObj()
    {
        string dataPath = "Editor/" + EmDataPath  + '/' + EmDataPath;
        string dataStr = CreateDataTableString(Resources.Load<TextAsset>(dataPath).ToString());
        EnemyJsonDataTable jData = JsonUtility.FromJson<EnemyJsonDataTable>(dataStr);

        // enum�̕�������擾
        List<string> enumNameList = new List<string>();
        // �z��I�u�W�F�N�g������Ă��܂���
        EnemySOBJDataTable table = ScriptableObject.CreateInstance<EnemySOBJDataTable>(); 

        // JSon����ScriptableObject ���쐬
        for (int no = 0; no < jData.table.Length; ++no)
        {
            EnemySOBJData newObj = ScriptableObject.CreateInstance<EnemySOBJData>();
            // �f�[�^�̃R�s�[
            newObj.id = jData.table[no].id;
            newObj.enumName = jData.table[no].enumName;
            newObj.sprite = jData.table[no].sprite;
            newObj.prefab = jData.table[no].prefab;
            newObj.hp = jData.table[no].hp;
            newObj.speed = jData.table[no].speed;
            newObj.attack = jData.table[no].attack;
            newObj.defense = jData.table[no].defense;

            if (Enum.TryParse(jData.table[no].dropType, out newObj.dropType)) {
            }
            else {
                Debug.LogError("�h���b�v�^�C�v�ϊ��~�X");
            }

            // �쐬���� ScriptableObject ��ۑ�
            string path = "Assets/Resources/Editor/"+ EmDataPath + '/' + EmDataPath;
            path += newObj.id.ToString();
            path += ".asset";

            AssetDatabase.CreateAsset(newObj, path);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
            Debug.Log("ScriptableObject created and saved at: " + path);

            // �z��ɂ��ǉ�
            table.data.Add(newObj);

            // enum�p���X�g�ɒǉ�
            string enumStr = newObj.enumName;
            enumStr = enumStr.Replace('-', '_');
            enumNameList.Add(enumStr);
        }

        // enum�쐬
        GenerateEnum("EnemySOBJEnum", enumNameList, $"Assets/Scenes/Stage/Script/CreateFiles/EnemySOBJEnum.cs");

        // �e�[�u���I�u�W�F�N�g���쐬
        string tablePath = "Assets/Resources/Editor/" + EmDataPath + '/' + "EmDataTable.asset";
        AssetDatabase.CreateAsset(table, tablePath);
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
        Debug.Log("ScriptableObject created and saved at: " + tablePath);

        Debug.Log("End...");
    }

    // ����f�[�^SOBJ�쐬
    void CreateWeaponInitObj()
    {
        string dataPath = "Editor/" + WepInitDataPath + '/' + WepInitDataPath;
        string dataStr = CreateDataTableString(Resources.Load<TextAsset>(dataPath).ToString());
        WeaponInitJsonDataTable jData = JsonUtility.FromJson<WeaponInitJsonDataTable>(dataStr);

        // enum�̕�������擾
        List<string> enumNameList = new List<string>();
        // �z��I�u�W�F�N�g������Ă��܂���
        WeaponInitSOBJDataTable table = ScriptableObject.CreateInstance<WeaponInitSOBJDataTable>();

        // JSon����ScriptableObject ���쐬
        for (int no = 0; no < jData.table.Length; ++no)
        {
            WeaponInitSOBJData newObj = ScriptableObject.CreateInstance<WeaponInitSOBJData>();
            // �f�[�^�̃R�s�[
            newObj.id = jData.table[no].id;
            newObj.enumName = jData.table[no].enumName;
            newObj.atk = jData.table[no].atk;
            newObj.recastDef = jData.table[no].recastDef;
            newObj.recastMin = jData.table[no].recastMin;
            newObj.speed = jData.table[no].speed;
            newObj.num = jData.table[no].num;
            newObj.time = jData.table[no].time;

            // �쐬���� ScriptableObject ��ۑ�
            string path = "Assets/Resources/Editor/" + WepInitDataPath + '/' + WepInitDataPath;
            path += newObj.id.ToString();
            path += ".asset";

            AssetDatabase.CreateAsset(newObj, path);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
            Debug.Log("ScriptableObject created and saved at: " + path);

            // enum�p���X�g�ɒǉ�
            string enumStr = newObj.enumName;
            enumStr = enumStr.Replace('-', '_');
            enumNameList.Add(enumStr);

            // �z��ɂ��ǉ�
            table.data.Add(newObj);
        }

        // enum�쐬
        GenerateEnum("WeaponInitSOBJEnum", enumNameList, $"Assets/Scenes/Stage/Script/CreateFiles/WeaponInitSOBJEnum.cs");

        // �e�[�u���I�u�W�F�N�g���쐬
        string tablePath = "Assets/Resources/Editor/" + WepInitDataPath + '/' + "WeaponInitDataTable.asset";
        AssetDatabase.CreateAsset(table, tablePath);
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
        Debug.Log("ScriptableObject created and saved at: " + tablePath);

        Debug.Log("End...");
    }

    // ����f�[�^SOBJ�쐬
    void CreateWeaponLVUPObj()
    {
        string dataPath = "Editor/" + WepLVUPDataPath + '/' + WepLVUPDataPath;
        string dataStr = CreateDataTableString(Resources.Load<TextAsset>(dataPath).ToString());
        WeaponLVUPJsonDataTable jData = JsonUtility.FromJson<WeaponLVUPJsonDataTable>(dataStr);

        // enum�̕�������擾
        List<string> enumNameList = new List<string>();
        // �z��I�u�W�F�N�g������Ă��܂���
        WeaponLVUPSOBJDataTable table = ScriptableObject.CreateInstance<WeaponLVUPSOBJDataTable>();

        // JSon����ScriptableObject ���쐬
        for (int no = 0; no < jData.table.Length; ++no)
        {
            WeaponLVUPSOBJData newObj = ScriptableObject.CreateInstance<WeaponLVUPSOBJData>();
            // �f�[�^�̃R�s�[
            newObj.id = jData.table[no].id;
            newObj.weaponName = jData.table[no].weaponName;
            newObj.key = new string[7];
            newObj.key[0] = jData.table[no].key1;
            newObj.key[1] = jData.table[no].key2;
            newObj.key[2] = jData.table[no].key3;
            newObj.key[3] = jData.table[no].key4;
            newObj.key[4] = jData.table[no].key5;
            newObj.key[5] = jData.table[no].key6;
            newObj.key[6] = jData.table[no].key7;
            newObj.value = new float[7];
            newObj.value[0] = jData.table[no].value1;
            newObj.value[1] = jData.table[no].value2;
            newObj.value[2] = jData.table[no].value3;
            newObj.value[3] = jData.table[no].value4;
            newObj.value[4] = jData.table[no].value5;
            newObj.value[5] = jData.table[no].value6;
            newObj.value[6] = jData.table[no].value7;

            // �쐬���� ScriptableObject ��ۑ�
            string path = "Assets/Resources/Editor/" + WepLVUPDataPath + '/' + WepLVUPDataPath;
            path += newObj.id.ToString();
            path += ".asset";

            AssetDatabase.CreateAsset(newObj, path);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
            Debug.Log("ScriptableObject created and saved at: " + path);

            // enum�p���X�g�ɒǉ�
            string enumStr = newObj.weaponName;
            enumStr = enumStr.Replace('-', '_');
            enumNameList.Add(enumStr);

            // �z��ɂ��ǉ�
            table.data.Add(newObj);
        }

        // enum�쐬
        GenerateEnum("WeaponLVUPSOBJEnum", enumNameList, $"Assets/Scenes/Stage/Script/CreateFiles/WeaponLVUPSOBJEnum.cs");

        // �e�[�u���I�u�W�F�N�g���쐬
        string tablePath = "Assets/Resources/Editor/" + WepLVUPDataPath + '/' + "WeaopnLVUPDataTable.asset";
        AssetDatabase.CreateAsset(table, tablePath);
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
        Debug.Log("ScriptableObject created and saved at: " + tablePath);

        Debug.Log("End...");
    }
}
