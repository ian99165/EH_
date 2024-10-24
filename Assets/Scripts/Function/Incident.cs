using UnityEngine;
using UnityEngine.InputSystem;
using Fungus;

public class Incident : MonoBehaviour // 撿取物件，附加在物件上
{
    private PlayerControls inputActions; // Input Actions 資源

    public string ItemName; // 道具名稱
    public AudioClip IncidentSound; // 撿取音效
    public GameObject Mask; // 使用更规范的变量命名

    public Camera mainCamera; // 直接引用你的攝像機對象
    private GameObject currentItem; // 當前檢查的物品
    public float raycastDistance = 3f; // 射線檢測距離
    public float requiredPrecision = 5f; // 需要的精確度，越小越精確

    public int EndObject_ = 0;
    public int TrueEndObject_ = 0;

    private bool isMask;
    public GameObject player;
    public GameObject Camera;

    private ControllerMovement3D MoveSp;
    private CameraController CameraSp;
    
    public Flowchart flowchart;

    private void Start()
    {
        MoveSp = player.GetComponent<ControllerMovement3D>();
        CameraSp = Camera.GetComponent<CameraController>();
    }

    private void Awake()
    {
        inputActions = new PlayerControls(); // 初始化 Input Actions
    }

    private void OnEnable()
    {
        inputActions.Enable(); // 啟用 Input Actions
        inputActions.Player.Incident.performed += OnIncident; // 註冊按 "E" 鍵事件
    }

    private void OnDisable()
    {
        inputActions.Player.Incident.performed -= OnIncident; // 取消註冊按 "E" 鍵事件
        inputActions.Disable(); // 禁用 Input Actions
    }

    private void Update()
    {
        CheckForItemWithRay(); // 檢測射線是否命中道具

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (Mask != null && isMask)
            {
                //Mask.SetActive(false); // 關閉指定的 GameObject
                isMask = false;

                //MoveSp.enabled = true;
                //CameraSp.enabled = true;
            }
        }
    }

    // 使用射線檢測物品
    private void CheckForItemWithRay()
    {
        Ray ray = mainCamera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2)); // 使用螢幕中央發射射線
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, raycastDistance)) // 發射射線並檢測物體
        {
            if (hit.collider.CompareTag("Item") || hit.collider.CompareTag("NPC") ||
                hit.collider.CompareTag("SavePoint") || hit.collider.CompareTag("Res") || hit.collider.CompareTag("Door")) // 如果射線命中道具
            {
                // 確認命中點是否接近物體的中心
                Vector3 itemCenter = hit.collider.bounds.center; // 物體的中心
                float distanceToCenter = Vector3.Distance(hit.point, itemCenter); // 計算射線命中點與物體中心的距離

                if (distanceToCenter <= requiredPrecision) // 如果射線命中點靠近物體中心，則允許撿取
                {
                    currentItem = hit.collider.gameObject; // 更新當前道具
                }
                else
                {
                    currentItem = null; // 如果射線命中點不在精確範圍內，則重置當前道具
                }
            }
            else
            {
                currentItem = null; // 如果射線沒有命中道具，重置當前道具
            }
        }
        else
        {
            currentItem = null; // 如果射線沒有命中任何物體，重置當前道具
        }
    }

    private void OnIncident(InputAction.CallbackContext context) // 當按下 "E" 鍵時
    {
        if (currentItem != null) // 檢查是否有對準物體
        {
            PerformIncident(currentItem); // 執行事件處理
        }
    }

    // 事件處理
    void PerformIncident(GameObject item) // 將物品作為參數傳入
    {            
        Object_Move_ object_move_ = item.GetComponent<Object_Move_>(); // 使用選中物件
        if (item.CompareTag("Item")) // 撿起物品
        {
            if (Mask != null && !isMask)
            {

                //Mask.SetActive(true); // 顯示檢查界面
                isMask = true;

                //MoveSp.enabled = false; // 禁用玩家移動
                //CameraSp.enabled = false; // 禁用攝像機移動
            }
            CheckObject(); // 檢查道具
        }

        if (item.CompareTag("Res"))
        {
            if (ItemName == "drawer_R")
            {
                if (!_lock)
                {
                    Debug.Log("drawer_R");
                    StartCoroutine(object_move_.Move_R());
                }
            }
        }

        if (item.CompareTag("SavePoint")) // 存檔點
        {
            Debug.Log("開啟存檔點選單");
        }

        if (item.CompareTag("Door")) // 開門
        {
            if (ItemName == "1F_Button")
            {
                StartCoroutine(object_move_._Button());
                flowchart.ExecuteBlock("1F_Button");
            }
            if (ItemName == "1F_Button_X")
            {
                StartCoroutine(object_move_._Button());
            }
        }

        if (item.CompareTag("NPC")) // 開啟對話
        {
            Debug.Log("對話");
            NPCTalk npcTalk = item.GetComponent<NPCTalk>();
            if (npcTalk != null)
            {
                npcTalk.TalkNPC(); // 開啟 NPC 對話
                Set_State();
            }
        }
    }

    
    void CheckObject()
    {
        if (ItemName == "EndObject")
        {
            EndObject_++;
        }

        if (ItemName == "TrueEndObject_")
        {
            TrueEndObject_++;
        }

        if (ItemName == "Clockwork")
        {
            flowchart.ExecuteBlock("State_Doll");
            Destroy(gameObject);//刪除物件
        }
    }
    //物件管理
    public bool hasClockwork = false;//發條
    public bool _lock;

    private void Set_State()
    {       
        State_Object state_object_ = GetComponent<State_Object>();

        if (hasClockwork)
        {
            state_object_.Set_State_Doll();
            hasClockwork = false;
        }
    }
    //fungus處理變數
    public void _hasClockwork()
    {
        hasClockwork = true;
    }
}

