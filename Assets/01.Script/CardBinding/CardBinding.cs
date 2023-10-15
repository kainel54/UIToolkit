using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.InputSystem;
using System.Threading.Tasks;
using System;

public class CardBinding : MonoBehaviour
{
    private UIDocument uiDocument;

    private VisualElement contentBox;

    private TextField txtName;
    private TextField txtDesc;

    public List<CharacterSO> charList;


    private List<Card> cardList = new();
    private Character _currntCharacter = null;
    [SerializeField] private Sprite defaultSprite;
    [SerializeField] private VisualTreeAsset cardTemplate;

    private void Awake()
    {
        uiDocument = GetComponent<UIDocument>();
    }

    private void OnEnable()
    {
        var root = uiDocument.rootVisualElement;

        contentBox = root.Q<VisualElement>("content");
        root.Q<Button>("add-btn").RegisterCallback<ClickEvent>(HandleAddCardClick);
        root.Q<Button>("show-all").RegisterCallback<ClickEvent>(ShowAllClick);

        txtName = root.Q<TextField>("txt-name");
        txtDesc = root.Q<TextField>("txt-desc");

        txtName.RegisterCallback<ChangeEvent<string>>(OnNameChanged);
        txtDesc.RegisterCallback<ChangeEvent<string>>(OnDescChanged);
    }

    private void ShowAllClick(ClickEvent evt)
    {
        
    }

    private void OnNameChanged(ChangeEvent<string> evt)
    {
        if (_currntCharacter == null) return;
        _currntCharacter.Name = evt.newValue;
    }

    private void OnDescChanged(ChangeEvent<string> evt)
    {
        if (_currntCharacter == null) return;
        _currntCharacter.Description= evt.newValue;
    }


    private async void HandleAddCardClick(ClickEvent e)
    {
        var template = cardTemplate.Instantiate().Q<VisualElement>("card-border");

        string name  = txtName.value;
        string desc = txtDesc.value;

        Character character = new Character(name, desc, defaultSprite);
        Card card = new Card(template, character);

        // template에서 Q를 이용해서 알맞은 라벨을 가져온다음에 이 값들을 넣어주면 된다.
        //template.Q<Label>("name-label").text = name;
        //template.Q<Label>("info-label").text = desc;
        cardList.Add(card);

        template.RegisterCallback<ClickEvent>(e =>
        {
            _currntCharacter = character;
            txtName.SetValueWithoutNotify( character.Name );
            txtDesc.SetValueWithoutNotify(character.Description);
        });

        contentBox.Add(template);

        await Task.Delay(100);
        template.AddToClassList("on");
    }
}
