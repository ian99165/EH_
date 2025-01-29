using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public Transform itemsContainer;  // 物品欄容器
    public GameObject itemPrefab;     // 物品的顯示預置物
    private List<GameObject> itemList = new List<GameObject>();  // 存儲物品的列表
    private Vector3 lastItemPosition; // 記錄上一個物品的位置
    private float itemSpacing = 0.2f; // 物品之間的間距

    void Start()
    {
        lastItemPosition = Vector3.zero; // 初始化位置設為 (0, 0, 0)
    }

    public void AddItem(GameObject item)
    {
        // 創建物品並設定位置
        GameObject newItem = Instantiate(itemPrefab, itemsContainer);
        
        // 設定物品的初始位置為物品欄的 (0, 0, 0)
        newItem.transform.localPosition = Vector3.zero;  // 確保物品顯示在物品欄的原點

        // 根據物品欄的排列邏輯調整位置
        newItem.transform.localPosition = lastItemPosition;  // 設置相對於物品欄容器的定位
        lastItemPosition = newItem.transform.localPosition + new Vector3(itemSpacing, 0, 0); // 更新下個物品的位置

        // 將物品加入物品欄列表
        itemList.Add(newItem);
    }

    // 可選：清空物品欄的方法（例如在重新開始遊戲時）
    public void ClearInventory()
    {
        foreach (var item in itemList)
        {
            Destroy(item);  // 刪除物品
        }
        itemList.Clear();  // 清空列表
    }
}


