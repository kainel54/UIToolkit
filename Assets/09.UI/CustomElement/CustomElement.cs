using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CustomElement : MonoBehaviour
{
    private UIDocument _uidocument;

    private void Awake()
    {
        _uidocument = GetComponent<UIDocument>();
    }

    private void OnEnable()
    {
        var root = _uidocument.rootVisualElement;

        VisualElement buttonRow = root.Q<VisualElement>(className: "button-row");

        buttonRow.RegisterCallback<ClickEvent>(evt =>
        {
            var dve = evt.target as DataVisualElement;
            if (dve != null) 
            {
                Debug.Log($"{dve.buttonIndex} 번 버튼, 이름:{dve.buttonName}");
            }
        });

        //List<VisualElement> buttons =  root.Query<VisualElement>(className: "button").ToList();
        //for(int i = 0; i < buttons.Count; ++i)
        //{
        //    int idx = i;
        //    buttons[i].RegisterCallback<ClickEvent>(eve =>
        //    {
        //        Debug.Log($"{idx}번 버튼이 클릭");
        //    });
        //}
    }
}
