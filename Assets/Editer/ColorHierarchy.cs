using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ColorHierarchy : MonoBehaviour
{
#if UNITY_EDITOR
    private static Dictionary<Object, ColorHierarchy> coloredObject = new();

    static ColorHierarchy()
    {
        EditorApplication.hierarchyWindowItemOnGUI += HandleDraw;
    }

    private static void HandleDraw(int instanceID, Rect selectionRect)
    {
        //에디터에게 내가 instanceID 를 줄테니 그에 따른 오브젝트를 내놔
        Object obj = EditorUtility.InstanceIDToObject(instanceID);

        if(obj != null&&coloredObject.ContainsKey(obj))
        {
            GameObject gameObj = obj as GameObject;
            if(gameObj.TryGetComponent<ColorHierarchy>(out ColorHierarchy ch))
            {
                PaintObject(obj, selectionRect, ch);
            }
            else
            {
                coloredObject.Remove(obj); // 더이상 그릴일이 없으니 제거
            }
        }
    }

    private static void PaintObject(Object obj, Rect selectionRect, ColorHierarchy ch)
    {
        Rect bgRect = new Rect(selectionRect.x, selectionRect.y,selectionRect.width+50,selectionRect.height);

        if(Selection.activeObject != obj)
        {
            EditorGUI.DrawRect(bgRect, ch.backColor);
            string name = $"{ch.prefix}{obj.name}";

            EditorGUI.LabelField(bgRect, name, new GUIStyle()
            {
                normal = new GUIStyleState { textColor = ch.fontColor },
                fontStyle = FontStyle.Bold
            });
        }
    }

    public string prefix;
    public Color backColor;
    public Color fontColor;


    private void Reset()
    {
        OnValidate();
    }

    private void OnValidate()
    {
        if (coloredObject.ContainsKey(this.gameObject) == false)
        {
            coloredObject.Add(this.gameObject, this);
        }
    }
#endif
}
