using UnityEngine.InputSystem;
using UnityEngine;
using System;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        
        Controls control = new Controls();
        control.Player.Enable();
        control.Player.Jump.performed += JumpPerformed;
    }

    private void JumpPerformed(InputAction.CallbackContext context)
    {
        Debug.Log("มกวม");
    }
}
