using UnityEngine;
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

    [Header("Interaction Settings")]
    public float interactionDistance = 3f;

    [Header("Mouse Settings")]
    public GameObject mouseState;
    
    [Header("Cursor Settings")]
    public Image cursor; // 準心圖案
    public Sprite defaultCursor; // 預設準心圖案
    public Sprite interactableCursor; 
    
    private PlayerControls controls;
    private bool _menu = false;
    private bool _talk = false;

    private void Awake()
    {
        controls = new PlayerControls();
        controls.Player.Move.performed += ctx => inputMove = ctx.ReadValue<Vector2>();
        controls.Player.Move.canceled += ctx => inputMove = Vector2.zero;
        controls.Player.Look.performed += ctx => inputLook = ctx.ReadValue<Vector2>();
        controls.Player.Look.canceled += ctx => inputLook = Vector2.zero;
        controls.Player.Run.performed += ctx => isRunning = !isRunning;
        controls.Player.Interact.performed += ctx => Interact();
        controls.Player.Close.performed += ctx => ToggleMenu();
    }

    private void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    private void Update()
    {
        Move();
        RotateCamera();
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

    private void Interact()
    {
        if (!_menu)
        {
            if (!_talk)
            {
                Ray ray = new Ray(playerCamera.position, playerCamera.forward);
                Debug.DrawRay(playerCamera.position, playerCamera.forward * interactionDistance, Color.red, 1f);

                MouseState mousestate = mouseState.GetComponent<MouseState>();
                if (Physics.Raycast(ray, out RaycastHit hit, interactionDistance))
                {
                    switch (hit.collider.tag)
                    {
                        case "Item":
                            var itemInteractionScript = hit.collider.GetComponent<ItemInteraction>();
                            if (itemInteractionScript != null)
                            {
                                itemInteractionScript.Interact_Item();
                                mousestate.MouseMode_II();
                                _menu = true;
                            }

                            break;
                        case "NPC":
                            var npcInteractionScript = hit.collider.GetComponent<NPCInteraction>();
                            if (npcInteractionScript != null)
                            {
                                npcInteractionScript.Interact_NPC();
                                mousestate.MouseMode_II();
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
                            var keyInteractionScript = hit.collider.GetComponent<KeyInteraction>();
                            if (keyInteractionScript != null)
                            {
                                //keyInteractionScript.Interact_Key();
                            }

                            break;
                        case "Door":
                            var doorInteractionScript = hit.collider.GetComponent<DoorInteraction>();
                            if (doorInteractionScript != null)
                            {
                                //doorInteractionScript.Interact_Door();
                            }

                            break;
                        case "SavePoint":
                            var saveInteractionScript = hit.collider.GetComponent<SaveInteraction>();
                            if (saveInteractionScript != null)
                            {
                                //saveInteractionScript.Interact_Save();
                            }

                            break;
                        default:
                            Debug.Log("未識別的物件");
                            break;
                    }
                }
            }
        }
    }

    private void ToggleMenu()
    {
        MouseState mousestate = mouseState.GetComponent<MouseState>();
        if (!_talk)
        {
            if (!_menu)
            {
                _menu = true;
                Debug.Log("呼叫選單");
                mousestate.MouseMode_II();
            }
            else
            {
                _menu = false;
                Debug.Log("關閉選單");
                mousestate.MouseMode_I();
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
