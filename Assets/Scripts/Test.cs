using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using System.Collections.Generic;
using UnityEngine.UI;

public class test : MonoBehaviour
{
    public RectTransform mousePointer; // 虛擬滑鼠指針
    public Sprite defaultIcon;         // 預設圖標
    public Sprite interactIcon;        // 互動圖標
    public float speed = 500f;         // 控制滑鼠移動速度
    private Vector2 moveInput;
    private Image pointerImage;

    void Start()
    {
        // 獲取指針圖標的 Image 組件
        pointerImage = mousePointer.GetComponent<Image>();
        pointerImage.sprite = defaultIcon; // 初始化為預設圖標
    }

    void Update()
    {
        if (Gamepad.current != null)
        {
            moveInput = Gamepad.current.rightStick.ReadValue();
        }

        Vector3 newPos = mousePointer.position;
        newPos.x += moveInput.x * speed * Time.deltaTime;
        newPos.y += moveInput.y * speed * Time.deltaTime;
        newPos.x = Mathf.Clamp(newPos.x, 0, Screen.width);
        newPos.y = Mathf.Clamp(newPos.y, 0, Screen.height);
        mousePointer.position = newPos;

        CheckUIUnderPointer();
    }

    void CheckUIUnderPointer()
    {
        PointerEventData pointerData = new PointerEventData(EventSystem.current)
        {
            position = mousePointer.position
        };

        List<RaycastResult> raycastResults = new List<RaycastResult>();
        EventSystem.current.RaycastAll(pointerData, raycastResults);

        bool isHoveringInteractable = false;

        foreach (var result in raycastResults)
        {
            if (result.gameObject.CompareTag("UI"))
            {
                isHoveringInteractable = true;
                break;
            }
        }

        // 改變圖標
        if (isHoveringInteractable)
        {
            pointerImage.sprite = interactIcon;
        }
        else
        {
            pointerImage.sprite = defaultIcon;
        }
    }
}