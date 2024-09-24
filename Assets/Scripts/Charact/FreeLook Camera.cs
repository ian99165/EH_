using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class InitializeFreeLookCamera : MonoBehaviour
{
    public CinemachineFreeLook freeLookCamera;  // 你的FreeLook Camera
    public Transform character;  // 角色的Transform

    void Start()
    {
        if (freeLookCamera != null && character != null)
        {
            // 设置FreeLook Camera的LookAt和Follow属性
            freeLookCamera.LookAt = character;
            freeLookCamera.Follow = character;

            // 计算摄像机初始位置，使其正对角色
            Vector3 direction = (freeLookCamera.transform.position - character.position).normalized;
            freeLookCamera.transform.position = character.position + direction * Vector3.Distance(freeLookCamera.transform.position, character.position);

            // 设置摄像机初始旋转
            freeLookCamera.transform.rotation = Quaternion.LookRotation(character.position - freeLookCamera.transform.position);
        }
    }
}
