using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatableObject : MonoBehaviour
{
    public float rotationSpeed = 300f;

    private Vector3 lastMousePosition;

    public void RotateWithMouse()//滑鼠旋轉物件
    {
        if (gameObject.CompareTag("UI_Object"))
        {
            // 檢測滑鼠按下事件
            if (Input.GetMouseButtonDown(0))
            {
                Debug.Log("Mouse button pressed.");
                lastMousePosition = Input.mousePosition;
            }

            // 檢測滑鼠拖動事件
            if (Input.GetMouseButton(0))
            {
                Debug.Log("Mouse is dragging.");
                Vector3 delta = Input.mousePosition - lastMousePosition;
                float rotationX = delta.x * rotationSpeed * Time.deltaTime;
                float rotationY = delta.y * rotationSpeed * Time.deltaTime;

                transform.Rotate(Vector3.up, -rotationX, Space.World);
                transform.Rotate(Vector3.right, rotationY, Space.World);

                lastMousePosition = Input.mousePosition;
            }
        }
    }

    public void RotateWithJoystick()//搖桿旋轉物件
    {
        if (gameObject.CompareTag("UI_Object"))
        {
            float joystickX = Input.GetAxis("Horizontal");
            float joystickY = Input.GetAxis("Vertical");

            float rotationX = joystickX * rotationSpeed * Time.deltaTime;
            float rotationY = joystickY * rotationSpeed * Time.deltaTime;

            transform.Rotate(Vector3.up, -rotationX, Space.World);
            transform.Rotate(Vector3.right, rotationY, Space.World);
        }
    }
}
