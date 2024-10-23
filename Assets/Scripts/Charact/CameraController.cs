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
    
    public bool canRotate = true;

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
        Cursor.lockState = CursorLockMode.Locked;
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
        if (!canRotate) return; // 如果不允许旋转，直接返回

        float mouseX = _cameraInput.x * mouseSensitivity * Time.deltaTime;
        float mouseY = _cameraInput.y * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        playerBody.Rotate(Vector3.up * mouseX);
    }

    // 停止相机旋转的方法
    public void StopCamera()
    {
        canRotate = false;
    }

    // 恢复相机旋转的方法
    public void ResumeCamera()
    {
        canRotate = true;
    }
}
