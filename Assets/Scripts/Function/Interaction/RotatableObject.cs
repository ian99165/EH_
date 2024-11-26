using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatableObject : MonoBehaviour
{
    public float rotationSpeed = 300f;

    public enum RotationMode
    {
        Mouse,
        Joystick
    }

    public RotationMode currentMode = RotationMode.Mouse;

    private Vector3 lastMousePosition;

    void Update()
    {
        DetectInputSource();

        switch (currentMode)
        {
            case RotationMode.Mouse:
                RotateWithMouse();
                break;

            case RotationMode.Joystick:
                RotateWithJoystick();
                break;
        }
    }

    private void DetectInputSource()
    {
        if (Input.anyKey)
        {
            Debug.Log("Keyboard or mouse input detected.");
            if (currentMode != RotationMode.Mouse)
            {
                currentMode = RotationMode.Mouse;
                Debug.Log("Switched to Mouse mode");
            }
        }

        if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0 ||
            Input.GetButton("Fire1") || Input.GetButton("Fire2") ||
            Input.GetButton("Fire3") || Input.GetButton("Jump"))
        {
            Debug.Log("Joystick input detected.");
            if (currentMode != RotationMode.Joystick)
            {
                currentMode = RotationMode.Joystick;
                Debug.Log("Switched to Joystick mode");
            }
        }
    }

    private void RotateWithMouse()
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

    private void RotateWithJoystick()
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
