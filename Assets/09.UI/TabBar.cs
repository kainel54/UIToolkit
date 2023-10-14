using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;
using UnityEngine.UIElements;

public class TabBar : MonoBehaviour
{
    UIDocument uiDocument;
    private void Awake()
    {
        uiDocument = GetComponent<UIDocument>();
    }

    private void OnEnable()
    {
        var root = uiDocument.rootVisualElement;
        var slideBox = root.Q("pages");
        root.Q<Label>("home").RegisterCallback<ClickEvent>(e =>
        {
            Debug.Log("home");
            slideBox.style.left = new Length(0, LengthUnit.Percent);
        });
        root.Q<Label>("music").RegisterCallback<ClickEvent>(e =>
        {
            Debug.Log("music");
            slideBox.style.left = new Length(-100, LengthUnit.Percent);
        });
        root.Q<Label>("game").RegisterCallback<ClickEvent>(e =>
        {
            Debug.Log("game");
            slideBox.style.left = new Length(-200, LengthUnit.Percent);
        });
    }
}
