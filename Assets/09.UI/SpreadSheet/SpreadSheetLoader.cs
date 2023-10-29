using System;
using System.Collections;
using System.IO;
using Unity.EditorCoroutines.Editor;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UIElements;

public class SpreadSheetLoader : EditorWindow
{
    [SerializeField]
    private VisualTreeAsset m_VisualTreeAsset = default;

    private string _documentID = "1eM1NxTdEx8oEB1l2nnvOGOXUIfazf6I455PSVhgoJPc";
    private Label _statusLabel;
    private VisualElement _loadingIcon;

    [MenuItem("Window/Utility/SpreadSheetLoader")]
    public static void OpenWindow()
    {
        SpreadSheetLoader wnd = GetWindow<SpreadSheetLoader>();
        wnd.titleContent = new GUIContent("SpreadSheetLoader");
    }

    public void CreateGUI()
    {
        VisualElement root = rootVisualElement;
        m_VisualTreeAsset = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>(
            "Assets/09.UI/SpreadSheet/SpreadSheetLoader.uxml");
        VisualElement templateContainer = m_VisualTreeAsset.Instantiate();
        templateContainer.style.flexGrow = 1;
        root.Add(templateContainer);
        _statusLabel = root.Q<Label>("status-label"); //상태라벨 표시기
        _loadingIcon = root.Q<VisualElement>("loading-icon");
        SetUP();
    }

    private void SetUP()
    {
        TextField txtUrl = rootVisualElement.Q<TextField>("txt-url");
        txtUrl.RegisterCallback<ChangeEvent<string>>(HandleUrlChange);
        txtUrl.SetValueWithoutNotify(_documentID);

        Button loadBtn = rootVisualElement.Q<Button>("btn-load");
        loadBtn.RegisterCallback<ClickEvent>(HandleLoadBtn);
    }

    private void HandleLoadBtn(ClickEvent evt)
    {
        EditorCoroutineUtility.StartCoroutine(GetDataFromSheet("0", (dataArr) =>
        {
            CreateSO(
                name: dataArr[0],
                dex: int.Parse(dataArr[1]),
                str: int.Parse(dataArr[2]),
                hp: int.Parse(dataArr[3]),
                wis: int.Parse(dataArr[4]));
        }), this);

        EditorCoroutineUtility.StartCoroutine(GetDataFromSheet("346585476", (dataArr) =>
        {
            CreateSourceCode(dataArr[0], dataArr[1], dataArr[2], dataArr[3]);
        }), this);
    }

    private IEnumerator GetDataFromSheet(string sheetID, Action<string[]> Process)
    {
        UnityWebRequest req = UnityWebRequest.Get(
         $"https://docs.google.com/spreadsheets/d/{_documentID}/export?format=tsv&gid={sheetID}");

        _statusLabel.text = "데이터 로딩중";
        _loadingIcon.RemoveFromClassList("off");

        yield return req.SendWebRequest();

        //404 error , 500 error
        if (req.result == UnityWebRequest.Result.ConnectionError || req.responseCode != 200)
        {
            Debug.LogError("Error : " + req.responseCode);
            yield break;
        }

        _loadingIcon.AddToClassList("off");

        string resText = req.downloadHandler.text;

        string[] lines = resText.Split("\n");

        int lineNumber = 1;
        try
        {
            for (lineNumber = 1; lineNumber < lines.Length; ++lineNumber)
            {
                string[] dataArr = lines[lineNumber].Split("\t"); //TSV로 뽑아왔으니
                Process?.Invoke(dataArr);
            }
        }
        catch (Exception e)
        {
            _statusLabel.text += $"\n {_documentID} 로딩 중 오류 발생";
            _statusLabel.text += $"\n {lineNumber} : 줄 오류 발생";
            _statusLabel.text += $"\n {e.Message}";
        }

        _statusLabel.text = $"\n 로드 완료! {lineNumber - 1} 개의 파일이 성공적으로 작성됨.";

        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
    }


    private void HandleUrlChange(ChangeEvent<string> evt)
    {
        _documentID = evt.newValue;
    }

    private void CreateSO(string name, int dex, int str, int hp, int wis)
    {
        string filePath = $"Assets/08.SO/StatSO/{name}.asset";
        StatSo asset = AssetDatabase.LoadAssetAtPath<StatSo>(filePath);

        if (asset == null)
        {
            asset = ScriptableObject.CreateInstance<StatSo>();
            asset.charname = name;
            asset.dex = dex;
            asset.str = str;
            asset.hp = hp;
            asset.wis = wis;
            string filename = AssetDatabase.GenerateUniqueAssetPath(filePath);
            AssetDatabase.CreateAsset(asset, filename);
        }
        else
        {
            asset.charname = name;
            asset.dex = dex;
            asset.str = str;
            asset.hp = hp;
            asset.wis = wis;
            EditorUtility.SetDirty(asset);
        }

    }

    private void CreateSourceCode(string name, string className, string speed, string type)
    {
        string code = string.Format(MovementCodeFormat.MovementFormat, name, className, speed, type);
        string path = $"{Application.dataPath}/01.Scripts";
        File.WriteAllText($"{path}/{className}.cs", code);
    }
}
