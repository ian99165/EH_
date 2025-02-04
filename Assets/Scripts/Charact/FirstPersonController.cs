using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

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
    [SerializeField] private GameObject _mainCamera;

    [Header("Interaction Settings")]
    public float interactionDistance = 3f;

    [Header("Mouse Settings")]
    public GameObject mouseState;

    [Header("Cursor Settings")]
    public Image cursor; // 準心圖案
    public Sprite defaultCursor; // 預設準心圖案
    public Sprite interactableCursor;
    public GameObject menubook;
    
    // Gravity Settings
    private PlayerControls controls;
    private bool _menu = false;
    private bool _talk = false;
    private Vector3 velocity; // 角色速度
    private float gravity = -9.81f; // 重力加速度
    private bool isGrounded; // 是否在地面
    
    private InteractionController _interactionController;
    private MouseState _mousestate;
    
    public Inventory inventory;

    private void Awake()
    {
        controls = new PlayerControls();
        controls.Player.Move.performed += ctx => inputMove = ctx.ReadValue<Vector2>();
        controls.Player.Move.canceled += ctx => inputMove = Vector2.zero;
        controls.Player.Look.performed += ctx => inputLook = ctx.ReadValue<Vector2>();
        controls.Player.Look.canceled += ctx => inputLook = Vector2.zero;
        controls.Player.Run.performed += ctx => isRunning = !isRunning;
        
        _interactionController = _mainCamera.GetComponent<InteractionController> ();
        _mousestate = mouseState.GetComponent<MouseState>();
    }

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        
        menubook.SetActive(false);
    }

    private void Update()
    {
        Move();
        RotateCamera();
        ApplyGravity();
        UpdateCursor();
    }

    private void Move()
    {
        if (!_menu)
        {
            if (!_talk)
            {
                Vector3 move = transform.right * inputMove.x + transform.forward * inputMove.y;
                float currentSpeed = isRunning ? runSpeed : moveSpeed;

                controller.Move(move * currentSpeed * Time.deltaTime);
            }
        }
    }

    private void RotateCamera()
    {
        if (!_menu)
        {
            if (!_talk)
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
        }
    }

    private void ApplyGravity()
    {
        isGrounded = controller.isGrounded;

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);
    }
    
    public void OnInteraction(InputValue value)
    { 
        if (!_menu)
        {
            if (!_talk)
            {
                Ray ray = new Ray(playerCamera.position, playerCamera.forward);
                Debug.DrawRay(playerCamera.position, playerCamera.forward * interactionDistance, Color.red, 1f);

                if (Physics.Raycast(ray, out RaycastHit hit, interactionDistance))
                {
                    switch (hit.collider.tag)
                    {
                        case "Item":
                            _interactionController.IsPickup = !_interactionController.IsPickup;
                            break;
                        case "NPC":
                            var npcInteractionScript = hit.collider.GetComponent<NPCInteraction>();
                            if (npcInteractionScript != null)
                            {
                                npcInteractionScript.Interact_NPC();
                                _mousestate.MouseMode_II();
                                _talk = true;
                            }
                            break;
                        case "Devices":
                            var devicesInteractionScript = hit.collider.GetComponent<DevicesInteraction>();
                            if (devicesInteractionScript != null)
                            {
                                devicesInteractionScript.Interact_Devices();
                            }
                            break;
                        case "Key":
                            inventory.AddItem("Key");
                            Destroy(hit.collider.gameObject);
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
        }
    }

    public void OnMenu(InputValue value)
    {
        if (!_talk)
        {
            if (!_menu)
            {
                _menu = true;
                Debug.Log("呼叫選單");
                _mousestate.MouseMode_II();
                menubook.SetActive(true);
            }
            else
            {
                _menu = false;
                Debug.Log("關閉選單");
                _mousestate.MouseMode_I();
                menubook.SetActive(false);
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

    private void UpdateCursor()
    {
        if (!_menu)
        {
            if (!_talk)
            {
                Ray ray = new Ray(playerCamera.position, playerCamera.forward);

                if (Physics.Raycast(ray, out RaycastHit hit, interactionDistance))
                {
                    // 防止未定義的 Tag 報錯
                    if (IsInteractableTag(hit.collider.tag))
                    {
                        cursor.sprite = interactableCursor; // 更改為可互動圖案
                    }
                    else
                    {
                        cursor.sprite = defaultCursor; // 恢復為預設圖案
                    }
                }
                else
                {
                    cursor.sprite = defaultCursor; // 恢復為預設圖案
                }
            }
        }
    }

    // 判斷是否為可互動的 Tag
    private bool IsInteractableTag(string tag)
    {
        return tag == "Item" || tag == "NPC" ||
               tag == "Devices" || tag == "Key" ||
               tag == "Door" || tag == "SavePoint";
    }

    public void TalkingSet()
    {
        _talk = false;
    }
}
