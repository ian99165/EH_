using UnityEngine;
using UnityEngine.InputSystem;

public class Incident : MonoBehaviour
{
    private PlayerControls inputActions; // Input Actions 資源
    
    public string ItemName; // 道具名稱
    public AudioClip IncidentSound; // 撿取音效
    
    public Camera mainCamera;  // 直接引用你的摄像機對象
    private GameObject currentItem;  // 當前檢查的物品
    public float raycastDistance = 3f; // 射線檢測距離
    public float requiredPrecision = 5f; // 需要的精確度，越小越精確

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
    }

    // 使用射線檢測物品
    private void CheckForItemWithRay()
    {
        Ray ray = mainCamera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2)); // 使用螢幕中央發射射線
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, raycastDistance)) // 發射射線並檢測物體
        {
            if (hit.collider.CompareTag("Item") || hit.collider.CompareTag("NPC") || hit.collider.CompareTag("SavePoint")) // 如果射線命中道具
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
        //Debug.Log("按下 E 鍵"); // 確認按鍵事件是否被觸發

        if (currentItem != null) // 檢查是否有對準物體
        {
            PerformIncident(currentItem); // 執行事件處理
        }
        else
        {
            Debug.Log("未對準任何物品"); // 如果沒有對準物品，輸出調試信息
        }
    }

    // 事件處理
    void PerformIncident(GameObject item) // 將物品作為參數傳入
    {
        if (item.CompareTag("Item")) // 撿起物品
        {
            Debug.Log("撿起物品");
            //Destroy(item); // 刪除當前物件
            AudioSource.PlayClipAtPoint(IncidentSound, item.transform.position);
        }

        if (item.CompareTag("SavePoint")) // 存檔點
        {
            Debug.Log("開啟選單");
        }

        if (item.CompareTag("NPC")) // 開啟對話
        {
            Debug.Log("對話");
            NPCTalk npcTalk = item.GetComponent<NPCTalk>();

            if (npcTalk != null)
            {
                npcTalk.TalkNPC();
            }
        }
    }
}
