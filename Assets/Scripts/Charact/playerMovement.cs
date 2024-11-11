using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : MonoBehaviour
{
    public float walkSpeed = 2f;        // 走路速度
    public float runSpeed = 5f;         // 跑步速度
    public float crouchSpeed = 1.5f;    // 蹲下速度
    public float crouchHeight = 0.5f;   // 蹲下時的身高
    public float standHeight = 2f;      // 站立時的身高
    public float crouchTime = 0.25f;    // 蹲下的平滑過渡時間
    private float currentHeight;        // 當前身高
    private bool isCrouching = false;   // 是否在蹲下
    private float moveSpeed;            // 當前移動速度

    private CharacterController controller;
    private Vector3 moveDirection;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        currentHeight = standHeight;
        moveSpeed = walkSpeed;
    }

    void Update()
    {
        // 取得玩家的輸入
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        bool isRunning = Input.GetKey(KeyCode.LeftShift);  // Shift鍵來跑步
        bool isCrouchingInput = Input.GetKey(KeyCode.LeftControl); // Ctrl鍵來蹲下

        // 設定移動速度
        moveSpeed = isRunning && !isCrouching ? runSpeed : (isCrouching ? crouchSpeed : walkSpeed);

        // 變換身高（蹲下與站立）
        if (isCrouchingInput && !isCrouching)
        {
            StartCrouch();
        }
        else if (!isCrouchingInput && isCrouching)
        {
            StopCrouch();
        }

        // 根據玩家的輸入來移動角色
        moveDirection = new Vector3(horizontal, 0, vertical).normalized;

        // 使角色根據視角移動
        moveDirection = Camera.main.transform.TransformDirection(moveDirection);
        moveDirection.y = 0;  // 保證角色只在水平面移動

        // 移動角色
        controller.Move(moveDirection * moveSpeed * Time.deltaTime);
    }

    private void StartCrouch()
    {
        isCrouching = true;
        currentHeight = crouchHeight;
        controller.height = Mathf.Lerp(controller.height, crouchHeight, crouchTime);
    }

    private void StopCrouch()
    {
        isCrouching = false;
        currentHeight = standHeight;
        controller.height = Mathf.Lerp(controller.height, standHeight, crouchTime);
    }
}
