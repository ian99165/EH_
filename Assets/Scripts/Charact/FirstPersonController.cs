using UnityEngine;
using UnityEngine.InputSystem;

public class FirstPersonController : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 5f;
    public float runSpeed = 8f;
    public float crouchSpeed = 2.5f;
    private CharacterController controller;

    [Header("Camera Settings")]
    public Transform playerCamera;
    public float rotationSpeed = 200f;
    private float xRotation = 0f;
    private Vector2 inputLook;
    private Vector2 inputMove;
    private bool isRunning = false;
    private const float lookThreshold = 0.01f; // 用於忽略細微輸入

    private PlayerControls controls;

    private void Awake()
    {
        controls = new PlayerControls();
        controls.Player.Move.performed += ctx => inputMove = ctx.ReadValue<Vector2>();
        controls.Player.Move.canceled += ctx => inputMove = Vector2.zero;
        controls.Player.Look.performed += ctx => inputLook = ctx.ReadValue<Vector2>();
        controls.Player.Look.canceled += ctx => inputLook = Vector2.zero;
        controls.Player.Run.performed += ctx => isRunning = !isRunning;
    }

    private void Start()
    {
        controller = GetComponent<CharacterController>();

        // 鎖定滑鼠指針並隱藏
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        Move();
        RotateCamera();
    }

    private void Move()
    {
        Vector3 move = transform.right * inputMove.x + transform.forward * inputMove.y;
        float currentSpeed = isRunning ? runSpeed : moveSpeed;

        controller.Move(move * currentSpeed * Time.deltaTime);
    }

    private void RotateCamera()
    {
        // 當滑鼠輸入超過閾值時才進行相機旋轉
        if (Mathf.Abs(inputLook.x) > lookThreshold || Mathf.Abs(inputLook.y) > lookThreshold)
        {
            float mouseX = inputLook.x * rotationSpeed * Time.deltaTime;
            float mouseY = inputLook.y * rotationSpeed * Time.deltaTime;

            // 旋轉角色的水平角度
            transform.Rotate(Vector3.up * mouseX);

            // 更新 X 軸旋轉並限制範圍
            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, -90f, 90f);

            // 應用 X 軸旋轉到相機
            playerCamera.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        }
    }

    private void OnEnable()
    {
        controls.Enable();
    }

    private void OnDisable()
    {
        controls.Disable();
    }
}
