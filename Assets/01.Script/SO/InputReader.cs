using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using static Controls;

[CreateAssetMenu(menuName ="SO/InputReader")]
public class InputReader : ScriptableObject, IPlayerActions
{

    private Controls _controls;
    public Controls GetControl()
    {
        return _controls;
    }

    public event Action<bool> JumpEvent;
    public event Action<Vector2> MovementEvent;

    private void OnEnable()
    {
        if (_controls == null)
        {
            _controls = new Controls();
            _controls.Player.SetCallbacks(this);
        }

        _controls.Player.Enable(); //입력 활성화
    }
    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.performed)
            JumpEvent?.Invoke(true);
        else if (context.canceled)
        {
            JumpEvent?.Invoke(false);
        }
    }

    public void OnMovement(InputAction.CallbackContext context)
    {
        Vector2 value = context.ReadValue<Vector2>();
        MovementEvent?.Invoke(value);
    }
}
