using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;
using UnityEngine.UIElements;

public class MainScreen : MonoBehaviour
{
    private UIDocument uIDocument;

    private void Awake()
    {
        uIDocument = GetComponent<UIDocument>();
    }

    private void OnEnable()
    {
        var root = uIDocument.rootVisualElement;
        var slideBox = root.Q("slide-box");
        root.Q<Label>("home-label").RegisterCallback<ClickEvent>(e =>
        {
            Debug.Log("label1");
            slideBox.style.left = new Length(0, LengthUnit.Percent);
        });
        root.Q<Label>("inventory-label").RegisterCallback<ClickEvent>(e =>
        {
            Debug.Log("label2");
            slideBox.style.left = new Length(-100, LengthUnit.Percent);
        });
        root.Q<Label>("equip-label").RegisterCallback<ClickEvent>(e =>
        {
            Debug.Log("label3");
            slideBox.style.left = new Length(-200, LengthUnit.Percent);
        });
        root.Q<Label>("friend-label").RegisterCallback<ClickEvent>(e =>
        {
            Debug.Log("label4");
            slideBox.style.left = new Length(-300, LengthUnit.Percent);
        });
    }

    private void OnClickBtn(ClickEvent evt)
    {
        Debug.Log("Å¬¸¯");
    }
}
