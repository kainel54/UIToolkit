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
            control.Player.Disable(); //키변경 하는거 해볼라고
            //키 변경시에는 반드시 해당 인풋맵 disable해줘야함
            Debug.Log("변경을 원하는 키를 입력하세요");
            var op = control.Player.Jump.PerformInteractiveRebinding()
                .WithControlsExcluding("Mouse")
                .WithCancelingThrough("<keyboard>/escape") //esc로 취소
                .OnComplete(op =>
                {
                    Debug.Log("변경되었습니다.");
                    op.Dispose();
                    control.Player.Enable();
                })
                .OnCancel(op =>
                {
                    Debug.Log("취소되었습니다");
                    op.Dispose();
                    control.Player.Enable();
                }).Start(); //변경할 키를 기다리는 상태가 됨
               
        }

        if (Keyboard.current.pKey.wasPressedThisFrame)
        {
            var json = inputReader.GetControl().SaveBindingOverridesAsJson();
            Debug.Log(json);

            inputReader.GetControl().LoadBindingOverridesFromJson(json);
        }
    }
}
