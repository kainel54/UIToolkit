using UnityEditor;
using UnityEditor.Callbacks;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

public class CreateChar : EditorWindow
{
    [SerializeField]
    private VisualTreeAsset m_VisualTreeAsset = default;

    private TextField _txtName;
    private TextField _txtDesc;
    private ObjectField _objectSprite;

    [MenuItem("GGM/CreateChar")]
    public static void ShowWindow()
    {
        CreateChar wnd = GetWindow<CreateChar>();
        wnd.titleContent = new GUIContent("ĳ���� SO ������");
    }

    [OnOpenAsset]
    public static bool OnOpenAsset(int instanceID,int line)//������� ����
    {
        if(Selection.activeObject is CharacterSO)
        {
            ShowWindow();
            return true;
        }
        return false;
        
    }

    public void CreateGUI()
    {
        VisualElement root = rootVisualElement;
        VisualElement container = m_VisualTreeAsset.Instantiate();
        container.style.flexGrow = 1;
        root.Add(container);

        _txtName = container.Q<TextField>("name-txt");
        _txtDesc = container.Q<TextField>("desc-txt");
        _objectSprite = container.Q<ObjectField>("object-sprite");

        container.Q<Button>("btn-create").RegisterCallback<ClickEvent>(CreateSO);
        OnSelectionChange();
    }

    private void CreateSO(ClickEvent evt)// SO�� ������ ����� ������ ����
    {
        string charname = _txtName.value;
        string filename = $"Assets/08.SO/CharacterSO/{charname}.asset";
        CharacterSO asset = AssetDatabase.LoadAssetAtPath<CharacterSO>(filename);

        if (asset != null)
        {
            asset.charname = _txtName.value;
            asset.description = _txtDesc.value;
            asset.sprite = _objectSprite.value as Sprite;
            EditorUtility.SetDirty(asset);
            AssetDatabase.SaveAssets();
        }
        else
        {
            asset = ScriptableObject.CreateInstance<CharacterSO>();

            asset.charname = _txtName.value;
            asset.description = _txtDesc.value;
            asset.sprite = _objectSprite.value as Sprite;

            string assetPath = AssetDatabase.GenerateUniqueAssetPath(
                            $"Assets/08.SO/CharacterSO/{asset.charname}.asset");
            AssetDatabase.CreateAsset(asset, assetPath);
        }
        AssetDatabase.Refresh(); //������ ��������
    }

    private void OnSelectionChange() //������� ų�� ���� �ҷ�����
    {
        var so = Selection.activeObject as CharacterSO;
        if( so != null )
        {
            _txtName.value = so.charname;
            _txtDesc.value = so.description;
            _objectSprite.value = so.sprite;
        }
    }


}

