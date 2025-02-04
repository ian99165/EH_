using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public Transform itemsContainer; // 物品欄容器
    private List<GameObject> itemList = new List<GameObject>(); // 存儲物品的列表
    private Vector3 startPosition = new Vector3(0, 0.3f, 0); // 初始位置 (0, 0.3, 0)
    private Vector3 lastItemPosition; // 記錄上一個物品的位置
    private float itemSpacing = 0.15f; // 物品之間的水平間距
    private float rowSpacing = -0.15f; // 物品之間的垂直間距
    private int maxColumns = 3; // 每列最多 3 個圖標
    private int currentColumn = 0; // 當前行內的物品數量

    [Header("Icon Prefabs")]
    public GameObject Key;
    public GameObject Clockwork;
    public GameObject Pages;

    // 物品名稱與對應圖示的映射
    private Dictionary<string, GameObject> itemIcons;

    void Start()
    {
        lastItemPosition = startPosition; // 初始化起始位置

        // 初始化物品對應的圖標
        itemIcons = new Dictionary<string, GameObject>
        {
            { "Key", Key },
            { "Clockwork", Clockwork },
            { "Pages", Pages }
        };
    }

    public void AddItem(string itemName)
    {
        // 確保物品名稱在字典中存在
        if (!itemIcons.ContainsKey(itemName))
        {
            Debug.LogWarning($"未找到對應的圖標: {itemName}");
            return;
        }

        // 創建對應的物品圖標
        GameObject newItem = Instantiate(itemIcons[itemName], itemsContainer);
        
        // 設定物品的顯示位置
        newItem.transform.localPosition = lastItemPosition;

        // 記錄當前行內的物品數量
        currentColumn++;

        if (currentColumn >= maxColumns)
        {
            // 換行：重置 X 軸，向下移動 Y 軸
            lastItemPosition = new Vector3(startPosition.x, lastItemPosition.y + rowSpacing, startPosition.z);
            currentColumn = 0; // 重置列計數
        }
        else
        {
            // 向右移動 X 軸
            lastItemPosition += new Vector3(itemSpacing, 0, 0);
        }

        // 將物品加入物品欄列表
        itemList.Add(newItem);
    }

    public void ClearInventory()
    {
        foreach (var item in itemList)
        {
            Destroy(item);
        }
        itemList.Clear();
        lastItemPosition = startPosition; // 重置位置
        currentColumn = 0; // 重置列數計數
    }
}
