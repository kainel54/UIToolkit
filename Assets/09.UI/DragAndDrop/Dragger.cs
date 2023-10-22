using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class Dragger : MouseManipulator
{
    private bool _isDagging = false;
    private Vector2 _startPos;
    private Vector2 _origin;
    private Action<Vector2,Vector2> DropCallback;
    public Dragger(Action<Vector2,Vector2> DropCallback = null)
    {
        _isDagging = false;
        activators.Add(new ManipulatorActivationFilter { button = MouseButton.LeftMouse });
        this.DropCallback = DropCallback;
    }
    protected override void RegisterCallbacksOnTarget()
    {
        target.RegisterCallback<MouseDownEvent>(OnMouseDown);
        target.RegisterCallback<MouseMoveEvent>(OnMouseMove);
        target.RegisterCallback<MouseUpEvent>(OnMouseUp);
    }

    protected override void UnregisterCallbacksFromTarget()
    {
        target.UnregisterCallback<MouseDownEvent>(OnMouseDown);
        target.UnregisterCallback<MouseMoveEvent>(OnMouseMove);
        target.UnregisterCallback<MouseUpEvent>(OnMouseUp);
    }

    protected void OnMouseDown(MouseDownEvent evt)
    {
        if(CanStartManipulation(evt))
        {
            _isDagging = true;
            _origin = new Vector2(target.layout.x,target.layout.y);
            _startPos = evt.localMousePosition;
            evt.StopPropagation();
            target.CaptureMouse();//«ÿ¥Á ≈∏∞Ÿ¿Ã ∏∂øÏΩ∫ ¿‚∞Ì æ»≥ˆ¡‹
                                      //Debug.Log(_dragStartPos);
        }
    }
    protected void OnMouseMove(MouseMoveEvent evt)
    {
        if (_isDagging&&target.HasMouseCapture())
        {
            Vector2 diff = evt.localMousePosition - _startPos;

            target.style.top = new Length(target.layout.y + diff.y, LengthUnit.Pixel);
            target.style.left = new Length(target.layout.x + diff.x, LengthUnit.Pixel);

        }
    }
    protected void OnMouseUp(MouseUpEvent evt)
    {
        if (!_isDagging || !target.HasMouseCapture()) return;
        _isDagging = false;
        target.ReleaseMouse();

        DropCallback?.Invoke(_origin,evt.mousePosition);
    }
}
