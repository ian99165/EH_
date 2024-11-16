using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;

public class test : MonoBehaviour
{
    public RectTransform mousePointer; // 滑鼠指針的 UI 元素
    public float speed = 1000f;        // 移動速度

    private Vector2 moveInput;

    void Update()
    {
        if (Gamepad.current != null)
        {
            moveInput = Gamepad.current.rightStick.ReadValue(); // 讀取右搖桿的輸入
        }

        // 將輸入轉換為屏幕座標的移動
        Vector3 newPos = mousePointer.position;
        newPos.x += moveInput.x * speed * Time.deltaTime;
        newPos.y += moveInput.y * speed * Time.deltaTime;

        // 限制滑鼠指針在屏幕內
        newPos.x = Mathf.Clamp(newPos.x, 0, Screen.width);
        newPos.y = Mathf.Clamp(newPos.y, 0, Screen.height);

        mousePointer.position = newPos;
    }

    public void OnClick(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            // 模擬滑鼠點擊事件
            PointerEventData pointerData = new PointerEventData(EventSystem.current)
            {
                position = mousePointer.position
            };
            // 可用於觸發點擊事件的代碼，具體實現需根據 UI 元素或物件調整
        }
    }
}