using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UIElements;

public class DragAndDrop : MonoBehaviour
{
    private UIDocument _uiDocument;

    private bool _isDagging = false;
    private Vector2 _dragStartPos;

    private VisualElement _potion;
    private Label _name;
    [SerializeField] Transform followTarget;

    private void Awake()
    {
        _uiDocument = GetComponent<UIDocument>();
    }

    private void OnEnable()
    {
        var root = _uiDocument.rootVisualElement;
        _potion = root.Q<VisualElement>("potion");
        _name = root.Q<Label>("name-label");
        _potion.AddManipulator(new Dragger(PotionDrop));
    }

    private void LateUpdate()
    {
        Vector3 _UIPos = RuntimePanelUtils.CameraTransformWorldToPanel(_uiDocument.rootVisualElement.panel,followTarget.position,Camera.main);

        float half = _name.layout.width*0.5f;

        _name.style.left = _UIPos.x-half;
        _name.style.top = _UIPos.y+100;
    }

    private void PotionDrop(Vector2 origin,Vector2 endPos)
    {
        Vector2 endScreenPos = new Vector2(endPos.x, Screen.height - endPos.y);

        Vector3 worldPos = Camera.main.ScreenToWorldPoint(endScreenPos);

        //Collider2D[] cols = new Collider2D[1];
        //Physics2D.OverlapCircleNonAlloc(worldPos, 1.2f, cols);

        Collider2D col = Physics2D.OverlapCircle(worldPos, 1.2f, LayerMask.GetMask("Player"));

        if(col != null)
        {
            if(col.TryGetComponent<Health>(out Health hp))
            {
                _potion.RemoveFromHierarchy();
                hp.InCreaseHealth(20);
            }
        }
        else
        {
            _potion.style.left = origin.x;
            _potion.style.top = origin.y;
        }

    }
}
