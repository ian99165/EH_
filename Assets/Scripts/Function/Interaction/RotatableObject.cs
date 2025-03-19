using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatableObject : MonoBehaviour
{
    public float rotationSpeed = 300f;
    public float mouseSpeed = 100f;
    private Vector3 lastMousePosition;

    void Update()
    {
        RotateWithMouse();
        RotateWithArrowKeys(); // 方向鍵旋轉
    }

    public void RotateWithMouse() // 滑鼠旋轉物件
    {
        if (gameObject.CompareTag("Item"))
        {
            if (Input.GetMouseButtonDown(0))
            {
                lastMousePosition = Input.mousePosition;
            }

            if (Input.GetMouseButton(0))
            {
                Vector3 delta = Input.mousePosition - lastMousePosition;
                float rotationX = delta.x * mouseSpeed * Time.deltaTime;
                float rotationY = delta.y * mouseSpeed * Time.deltaTime;

                transform.Rotate(Vector3.up, -rotationX, Space.World);
                transform.Rotate(Vector3.right, rotationY, Space.World);

                lastMousePosition = Input.mousePosition;
            }
        }
    }

    public void RotateWithArrowKeys() // 方向鍵旋轉物件
    {
        if (gameObject.CompareTag("Item"))
        {
            float rotationX = 0f;
            float rotationY = 0f;

            // 左右鍵旋轉 Y 軸
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                rotationX = -rotationSpeed * Time.deltaTime;
            }
            else if (Input.GetKey(KeyCode.RightArrow))
            {
                rotationX = rotationSpeed * Time.deltaTime;
            }

            // 上下鍵旋轉 X 軸
            if (Input.GetKey(KeyCode.UpArrow))
            {
                rotationY = rotationSpeed * Time.deltaTime;
            }
            else if (Input.GetKey(KeyCode.DownArrow))
            {
                rotationY = -rotationSpeed * Time.deltaTime;
            }

            // 套用旋轉
            transform.Rotate(Vector3.up, rotationX, Space.World);
            transform.Rotate(Vector3.right, rotationY, Space.World);
        }
    }
}
