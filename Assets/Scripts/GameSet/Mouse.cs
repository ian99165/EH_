using UnityEngine;
using UnityEngine.InputSystem;

public class Mouse : MonoBehaviour
{
    [Header("虛擬搖桿滑鼠設置")]
    public RectTransform virtualCursor; // 虛擬滑鼠的 UI 物件
    public Canvas canvas;               // UI 所在 Canvas
    public float cursorSpeed = 1000f;   // 滑鼠移動速度

    private Vector2 cursorPos;          // 虛擬滑鼠位置
    private PlayerInput playerInput;    // 新的 Input System
    private InputAction moveAction;     // 右搖桿移動的 Action
    private InputAction clickAction;    // 按鍵點擊的 Action
    private PlayerControls controls;
    
    private enum InputMode
    {
        _key_mod,
        _joy_mod
    }
    private InputMode _mode;

    private void Awake()
    {
        controls = new PlayerControls();
        controls.UI.anyKey.performed += ctx => KeyMod();
        controls.UI.anyJoy.performed += ctx => JoyMod();
    }

    void Start()
    {
        controls.Enable();

        playerInput = GetComponent<PlayerInput>();
        if (playerInput == null)
        {
            enabled = false;
            return;
        }

        // 綁定行為
        moveAction = playerInput.actions["AsMouse"];
        clickAction = playerInput.actions["Click"];
        if (moveAction == null || clickAction == null)
        {
            enabled = false;
            return;
        }

        cursorPos = new Vector2(Screen.width / 2f, Screen.height / 2f);
        UpdateCursorPos();

        if (virtualCursor == null)
        {
            enabled = false;
            return;
        }

        if (canvas == null)
        {
            enabled = false;
            return;
        }
    }

    void Update()
    {
        Vector2 input = moveAction.ReadValue<Vector2>();
        cursorPos += input * cursorSpeed * Time.deltaTime;

        // 限制滑鼠位置在螢幕內
        cursorPos.x = Mathf.Clamp(cursorPos.x, 0, Screen.width);
        cursorPos.y = Mathf.Clamp(cursorPos.y, 0, Screen.height);
        
        UpdateCursorPos();// 更新滑鼠 UI 的位置

        if (clickAction.triggered)
        {
            SimulateMouseClick();
        }
    }

    private void UpdateCursorPos()
    {
        // 將螢幕座標轉換為 Canvas 的局部座標
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            canvas.GetComponent<RectTransform>(),
            cursorPos,
            canvas.worldCamera,
            out Vector2 localCursorPos
        );

        if (virtualCursor != null)
        {
            virtualCursor.anchoredPosition = localCursorPos;
        }
    }

    private void SimulateMouseClick()//搖桿模擬鼠標
    {
        switch (_mode)
        { 
            case InputMode._joy_mod:                
                Ray ray_j = Camera.main.ScreenPointToRay(cursorPos);
                if (Physics.Raycast(ray_j, out RaycastHit hit_j))
                {
                    Debug.Log(hit_j.collider.gameObject.name);
                    if (hit_j.collider.gameObject.CompareTag("UI_Meun")) //書本菜單點擊事件
                    {
                        Debug.Log("UI_Meun");
                        switch (hit_j.collider.gameObject.name)
                        {
                            case "Button_Back":
                                Debug.Log("Button_Back");
                                break;
                            case "Button_Exit":
                                Debug.Log("Button_Exit");
                                break;
                            default:
                                Debug.Log("Nothing");
                                break;
                        }
                    }

                    if (hit_j.collider.gameObject.CompareTag("UI_Button")) //拾取物件點擊事件
                    {
                        Debug.Log("UI_Button");
                    }
                }
                break;
            
            case InputMode._key_mod:
                    Debug.Log("Mouse Button");
                    Ray ray_m = Camera.main.ScreenPointToRay(cursorPos);
                    if (Physics.Raycast(ray_m, out RaycastHit hit_m))
                    {
                        Debug.Log(hit_m.collider.gameObject.name);
                        if (hit_m.collider.gameObject.CompareTag("UI_Meun")) //書本菜單點擊事件
                        {
                            Debug.Log("UI_Meun");
                            switch (hit_m.collider.gameObject.name)
                            {
                                case "Button_Back":
                                    Debug.Log("Button_Back");
                                    break;
                                case "Button_Exit":
                                    Debug.Log("Button_Exit");
                                    break;
                                default:
                                    Debug.Log("Nothing");
                                    break;
                            }
                        }

                        if (hit_m.collider.gameObject.CompareTag("UI_Button")) //拾取物件點擊事件
                        {
                            Debug.Log("UI_Button");
                        }
                    }
                break;
        }
    }
    
    private InputMode currentMode = InputMode._joy_mod;

    private void KeyMod()//搖桿模式
    {
        if (currentMode != InputMode._key_mod)
        {
            currentMode = InputMode._key_mod;
        }
    }
    
    private void JoyMod()//鼠鍵模式
    {
        if (currentMode != InputMode._joy_mod)
        {
            currentMode = InputMode._joy_mod;
        }
    }
}
