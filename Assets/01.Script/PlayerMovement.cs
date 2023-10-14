using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private InputReader _inputReader;

    private Rigidbody2D rigidbody;
    private float speed = 3f;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();

        _inputReader.JumpEvent += JumpHandle;
        _inputReader.MovementEvent += MovementHandle;
    }

    private void OnDestroy()
    {
        _inputReader.JumpEvent -= JumpHandle;
        _inputReader.MovementEvent -= MovementHandle;
    }

    public void JumpHandle(bool value)
    {
        Debug.Log($"점프 핸들 {value}");
    }

    public void MovementHandle(Vector2 movement)
    {
        rigidbody.velocity = movement * speed;
    }
}
