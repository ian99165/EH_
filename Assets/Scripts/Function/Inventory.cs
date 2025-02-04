using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public Transform itemsContainer; // 物品欄容器
    private List<GameObject> itemList = new List<GameObject>(); // 存儲物品的列表
    private Vector3 lastItemPosition; // 記錄上一個物品的位置
    private float itemSpacing = 0.2f; // 物品之間的間距

    [Header("Icon Prefabs")]
    public GameObject Key;
    public GameObject Clockwork;
    public GameObject Pages;

    // 物品名稱與對應圖示的映射
    private Dictionary<string, GameObject> itemIcons;

    void Start()
    {
        lastItemPosition = Vector3.zero;

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

        // 更新下個物品的位置
        lastItemPosition = newItem.transform.localPosition + new Vector3(itemSpacing, 0, 0);

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
        lastItemPosition = Vector3.zero; // 重置位置
    }
}