using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class TabBar : MonoBehaviour
{
    UIDocument uiDocument;
    private void Awake()
    {
        uiDocument = GetComponent<UIDocument>();
    }
}
