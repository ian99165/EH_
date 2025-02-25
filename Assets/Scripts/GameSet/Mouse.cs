using UnityEngine;
using UnityEngine.InputSystem;

public class Mouse : MonoBehaviour
{
    [Header("虛擬搖桿滑鼠設置")]
    public RectTransform virtualCursor;
    public Canvas canvas;
    public float cursorSpeed = 1000f;

    private Vector2 cursorPos;
    private PlayerControls controls;
    private InputMode currentMode = InputMode._joy_mod;

    private enum InputMode { _key_mod, _joy_mod }
    
    private Inventory inventory;

    private void Awake()
    {
        controls = new PlayerControls();
        controls.UI.anyKey.performed += _ => SetMode(InputMode._key_mod);
        controls.UI.anyJoy.performed += _ => SetMode(InputMode._joy_mod);
        controls.Enable();
    }

    private void Start()
    {
        inventory = FindObjectOfType<Inventory>();
        if (inventory == null)
        {
            Debug.LogError("找不到 Inventory 物件");
        }
        cursorPos = new Vector2(Screen.width / 2f, Screen.height / 2f);
        UpdateCursorPos();
    }

    private void Update()
    {
        cursorPos += controls.Player.AsMouse.ReadValue<Vector2>() * cursorSpeed * Time.deltaTime;
        cursorPos = new Vector2(Mathf.Clamp(cursorPos.x, 0, Screen.width), Mathf.Clamp(cursorPos.y, 0, Screen.height));
        UpdateCursorPos();

        if (controls.Player.Click.triggered)
            SimulateMouseClick();
    }

    private void UpdateCursorPos()
    {
        if (canvas && virtualCursor && RectTransformUtility.ScreenPointToLocalPointInRectangle(
                canvas.GetComponent<RectTransform>(), cursorPos, canvas.worldCamera, out Vector2 localCursorPos))
        {
            virtualCursor.anchoredPosition = localCursorPos;
        }
    }

    private void SimulateMouseClick()
    {
        Ray ray = Camera.main.ScreenPointToRay(cursorPos);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            Debug.Log(hit.collider.gameObject.tag);
            Debug.Log(hit.collider.gameObject.name);
            if (hit.collider.CompareTag("UI_Meun"))
            {
                if (hit.collider.name == "Button_Back") Debug.Log("Button_Back");
                else if (hit.collider.name == "Button_Exit") Debug.Log("Button_Exit");
                else Debug.Log("Nothing");
            }
            else if (hit.collider.CompareTag("UI_Button"))
            {
            }
            else if (hit.collider.CompareTag("UI_Item"))
            {
                Debug.Log("丟棄");
                inventory.DropItem(hit.collider.gameObject);
            }
        }
    }

    private void SetMode(InputMode mode)
    {
        if (currentMode != mode)
            currentMode = mode;
    }
}