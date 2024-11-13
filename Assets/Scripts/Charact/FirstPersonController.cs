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
    private const float lookThreshold = 0.01f;

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
        if (Mathf.Abs(inputLook.x) > lookThreshold || Mathf.Abs(inputLook.y) > lookThreshold)
        {
            float mouseX = inputLook.x * rotationSpeed * Time.deltaTime;
            float mouseY = inputLook.y * rotationSpeed * Time.deltaTime;

            transform.Rotate(Vector3.up * mouseX);

            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, -90f, 90f);

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
