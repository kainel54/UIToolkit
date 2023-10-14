using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerTest : MonoBehaviour
{
    [SerializeField] private InputReader inputReader;

    private void Update()
    {
        //Keyboard.current.anyKey.wasPressedThisFrame
        if (Keyboard.current.kKey.wasPressedThisFrame)
        {
            var control = inputReader.GetControl();
            control.Player.Disable(); //Ű���� �ϴ°� �غ����
            //Ű ����ÿ��� �ݵ�� �ش� ��ǲ�� disable�������
            Debug.Log("������ ���ϴ� Ű�� �Է��ϼ���");
            var op = control.Player.Jump.PerformInteractiveRebinding()
                .WithControlsExcluding("Mouse")
                .WithCancelingThrough("<keyboard>/escape") //esc�� ���
                .OnComplete(op =>
                {
                    Debug.Log("����Ǿ����ϴ�.");
                    op.Dispose();
                    control.Player.Enable();
                })
                .OnCancel(op =>
                {
                    Debug.Log("��ҵǾ����ϴ�");
                    op.Dispose();
                    control.Player.Enable();
                }).Start(); //������ Ű�� ��ٸ��� ���°� ��
               
        }

        if (Keyboard.current.pKey.wasPressedThisFrame)
        {
            var json = inputReader.GetControl().SaveBindingOverridesAsJson();
            Debug.Log(json);

            inputReader.GetControl().LoadBindingOverridesFromJson(json);
        }
    }
}
