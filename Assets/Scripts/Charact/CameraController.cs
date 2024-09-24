using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{
    [Header("Camera Settings")]
    [SerializeField] private Transform playerBody; // 确保在 Inspector 中已分配
    [SerializeField] private float mouseSensitivity = 100f;
    private float xRotation = 0f;

    private PlayerControls _controls;
    private Vector2 _cameraInput;

    private void Awake()
    {
        _controls = new PlayerControls();
    }

    private void OnEnable()
    {
        _controls.Enable();
    }

    private void OnDisable()
    {
        _controls.Disable();
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked; // 锁定鼠标
    }

    public void HandleLook(InputAction.CallbackContext context)
    {
        _cameraInput = context.ReadValue<Vector2>();
    }

    private void Update()
    {
        HandleCamera();
    }

    private void HandleCamera()
    {
        float mouseX = _cameraInput.x * mouseSensitivity * Time.deltaTime;
        float mouseY = _cameraInput.y * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        playerBody.Rotate(Vector3.up * mouseX);
    }
}
