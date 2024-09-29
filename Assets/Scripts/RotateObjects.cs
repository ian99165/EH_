using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateObjects : MonoBehaviour //旋轉物件
{
    public float rotationSpeed = 200.0f; // 旋转速度

    private void Start()
    {
        enabled = false; // 初始状态下禁用旋转功能
    }

    private void Update()
    {
        // 根据鼠标移动来旋转物品
        RotateObject();
    }

    private void RotateObject()
    {
        // 获取鼠标移动的水平和垂直位移
        float horizontal = Input.GetAxis("Mouse X") * rotationSpeed * Time.deltaTime;
        float vertical = Input.GetAxis("Mouse Y") * rotationSpeed * Time.deltaTime;

        // 根据鼠标移动旋转物品
        transform.Rotate(Vector3.up, -horizontal, Space.World); // 水平旋转
        transform.Rotate(Vector3.right, vertical, Space.World); // 垂直旋转
    }
}
