using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class Mouse : MonoBehaviour
{
    [Header("滑鼠設置")]
    public RectTransform virtualCursor; // 虛擬滑鼠的 UI 物件
    public Canvas canvas;               // UI 所在 Canvas
    public float cursorSpeed = 1000f;   // 滑鼠移動速度

    private Vector2 cursorPos;          // 虛擬滑鼠位置
    private PlayerInput playerInput;    // 新的 Input System
    private InputAction moveAction;     // 右搖桿移動的 Action
    private InputAction clickAction;    // 按鍵點擊的 Action

    void Start()
    {
        // 初始化 PlayerInput
        playerInput = GetComponent<PlayerInput>();
        if (playerInput == null)
        {
            Debug.LogError("PlayerInput 未綁定！請確保已正確添加 PlayerInput 組件。");
            enabled = false; // 停止腳本執行
            return;
        }

        // 綁定行為
        moveAction = playerInput.actions["AsMouse"];
        clickAction = playerInput.actions["Click"];
        if (moveAction == null || clickAction == null)
        {
            Debug.LogError("行為 'AsMouse' 或 'Click' 未在 Input Actions 中正確配置！");
            enabled = false;
            return;
        }

        // 初始化虛擬滑鼠位置
        cursorPos = new Vector2(Screen.width / 2f, Screen.height / 2f);
        UpdateCursorPos();

        // 檢查 Canvas 和虛擬滑鼠
        if (virtualCursor == null)
        {
            Debug.LogError("虛擬滑鼠未設置！請將虛擬滑鼠拖入 Inspector 中的 Virtual Cursor 欄位。");
            enabled = false;
            return;
        }

        if (canvas == null)
        {
            Debug.LogError("Canvas 未設置！請將 UI Canvas 拖入 Inspector 中的 Canvas 欄位。");
            enabled = false;
            return;
        }
    }

    void Update()
    {

        // 更新滑鼠位置
        Vector2 input = moveAction.ReadValue<Vector2>();
        cursorPos += input * cursorSpeed * Time.deltaTime;

        // 限制滑鼠位置在螢幕內
        cursorPos.x = Mathf.Clamp(cursorPos.x, 0, Screen.width);
        cursorPos.y = Mathf.Clamp(cursorPos.y, 0, Screen.height);

        // 更新滑鼠 UI 的位置
        UpdateCursorPos();

        // 模擬滑鼠點擊
        if (clickAction.triggered)
        {
            Debug.Log("triggered !!");
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

    private void SimulateMouseClick()
    {
        Debug.Log($"cursorPos:{cursorPos}");
        Ray ray = Camera.main.ScreenPointToRay(cursorPos);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            Debug.Log(hit.collider.gameObject.name);
            //hit.collider.gameObject.SendMessage("OnMouseDown", SendMessageOptions.DontRequireReceiver);
        }


        // 檢查 EventSystem 是否存在
        /*
        if (EventSystem.current == null)
        {
            Debug.LogError("EventSystem 不存在！請確保場景中有一個 EventSystem。");
            return;
        }

        // 檢測滑鼠下方的物件
        PointerEventData pointerData = new PointerEventData(EventSystem.current)
        {
            position = cursorPos
        };

        // 將滑鼠點擊發送到 UI
        var results = new System.Collections.Generic.List<RaycastResult>();
        EventSystem.current.RaycastAll(pointerData, results);

        if (results.Count > 0)
        {
            // 點擊第一個檢測到的 UI 元素
            var clickedObject = results[0].gameObject;
            ExecuteEvents.Execute(clickedObject, pointerData, ExecuteEvents.pointerClickHandler);
        }
        else
        {
            // 如果沒有檢測到 UI，檢測 3D 世界
            Ray ray = Camera.main.ScreenPointToRay(cursorPos);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                Debug.Log(hit.collider.gameObject.name);
                //hit.collider.gameObject.SendMessage("OnMouseDown", SendMessageOptions.DontRequireReceiver);
            }
        }
        */
    }
}
