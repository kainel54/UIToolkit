using System.Collections;
using System.Collections.Generic;
using ToolkitMessageSystem;
using UnityEngine;
using UnityEngine.InputSystem;

public class TestScript :MonoBehaviour
{
    private void Update()
    {
        if (Keyboard.current.qKey.wasPressedThisFrame)
        {
            MessageHub.OnMessage?.Invoke("�׽�Ʈ �޽��� �Դϴ�.", MessageColor.Red);
        }
    }
}
