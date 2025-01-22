using UnityEngine;

public class Detect_Objects : MonoBehaviour
{
    [Header("移動機率設定")]
    [Range(0f, 1f)] public float teleportChance = 0.1f; // 物件移動機率

    [Header("偵測圖層")]
    public LayerMask detectionLayer; // 偵測專屬圖層

    [Header("角色相機")]
    public Camera playerCamera; // 玩家攝像機

    [Header("擴大範圍")]
    public float detectionRadius = 50f; // 偵測範圍半徑

    private void Update()
    {
        CheckForObjectsInView();
    }

    private void CheckForObjectsInView()
    {
        Plane[] frustumPlanes = GeometryUtility.CalculateFrustumPlanes(playerCamera);
        Debug.Log("正在計算相機視錐體內的物件...");

        Collider[] colliders = Physics.OverlapSphere(playerCamera.transform.position, detectionRadius, detectionLayer);
        foreach (var collider in colliders)
        {
            if (GeometryUtility.TestPlanesAABB(frustumPlanes, collider.bounds))
            {
                Debug.Log($"物件 {collider.name} 位於玩家視錐範圍內");

                Object_Transfer transferScript = collider.GetComponent<Object_Transfer>();
                if (transferScript != null)
                {
                    transferScript.OnEnterView(); // 記錄進入視錐範圍
                }
            }
            else
            {
                Object_Transfer transferScript = collider.GetComponent<Object_Transfer>();
                if (transferScript != null)
                {
                    Debug.Log($"物件 {collider.name} 離開視錐範圍，嘗試移動");
                    transferScript.OnExitView(teleportChance);
                }
                else
                {
                    Debug.Log($"物件 {collider.name} 不包含 Object_Transfer 腳本");
                }
            }
        }
    }
}