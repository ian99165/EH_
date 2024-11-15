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

    [Header("Interaction Settings")]
    public float interactionDistance = 3f;
    public LayerMask interactableLayer; // 定義互動層

    private PlayerControls controls;

    private void Awake()
    {
        controls = new PlayerControls();
        controls.Player.Move.performed += ctx => inputMove = ctx.ReadValue<Vector2>();
        controls.Player.Move.canceled += ctx => inputMove = Vector2.zero;
        controls.Player.Look.performed += ctx => inputLook = ctx.ReadValue<Vector2>();
        controls.Player.Look.canceled += ctx => inputLook = Vector2.zero;
        controls.Player.Run.performed += ctx => isRunning = !isRunning;
        controls.Player.Interact.performed += ctx => Interact(); // 新增互動輸入
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

    private void Interact()
    {
        Ray ray = new Ray(playerCamera.position, playerCamera.forward);
        Debug.DrawRay(playerCamera.position, playerCamera.forward * interactionDistance, Color.red, 1f);
    
        if (Physics.Raycast(ray, out RaycastHit hit, interactionDistance))
        {
            switch (hit.collider.tag)
            {
                case "Item":
                    var itemInteractionScript = hit.collider.GetComponent<ItemInteraction>();
                    if (itemInteractionScript != null)
                    {
                        itemInteractionScript.Interact_Item();
                    }
                    break;
                case "NPC":
                    var npcInteractionScript = hit.collider.GetComponent<NPCInteraction>();
                    if (npcInteractionScript != null)
                    {
                        npcInteractionScript.Interact_NPC();
                    }
                    break;
                case "Devices":
                    break;
                case "Key":
                    break;
                case "Door":
                    break;
                case "SavePoint":
                    break;
                default:
                    Debug.Log("未識別的物件");
                    break;
            }
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
