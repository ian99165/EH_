/*using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ItemInteraction : MonoBehaviour
{
    public Transform inspectPosition; // 物品检查位置
    public float resizeScale = 0.7f; // 缩小比例
    public float moveDuration = 1.0f; // 移动和缩小持续时间
    public Camera mainCamera; // 直接引用你的摄像机对象
    public MonoBehaviour playerCameraScript; // 玩家镜头脚本引用
    public MonoBehaviour playerMovementScript; // 玩家移动脚本引用
    private GameObject currentItem; // 当前检查的物品
    private bool isInspecting = false; // 是否在检查模式中
    private bool isRestoring = false; // 是否正在恢复物品状态

    // 保存每个物品的初始变换（位置、旋转、缩放）
    private Dictionary<GameObject, ItemTransformData> itemInitialTransforms = new Dictionary<GameObject, ItemTransformData>();

    // 数据结构用于存储物体的初始值
    private class ItemTransformData
    {
        public Vector3 originalPosition;
        public Quaternion originalRotation;
        public Vector3 originalScale;
    }

    void Update()
    {
        // F 进行检查
        if (Input.GetKey(KeyCode.F) && !isInspecting)
        {
            PerformRaycast();
        }

        // 按下 Escape 退出检查模式
        if (isInspecting && Input.GetKeyDown(KeyCode.Escape))
        {
            StopInspecting();
        }
    }

    // 射线检测交互物品
    void PerformRaycast()
    {
        if (mainCamera != null)
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                if (hit.transform.CompareTag("NPC"))
                {
                    StartCoroutine(StartInspecting(hit.transform.gameObject));
                }
            }
        }
        else
        {
            Debug.LogError("Main camera reference is not set.");
        }
    }

    // 开始检查物品的协程
    IEnumerator StartInspecting(GameObject item)
    {
        currentItem = item;

        // 如果该物品没有被记录过初始值，则记录一次
        if (!itemInitialTransforms.ContainsKey(currentItem))
        {
            SaveInitialTransform(currentItem);
        }

        // 获取该物品的初始位置和缩放
        Vector3 targetPosition = inspectPosition.position;
        Vector3 targetScale = itemInitialTransforms[currentItem].originalScale * resizeScale;

        DisablePlayerControls();

        // 平滑移动和缩放物品
        float elapsedTime = 0;
        while (elapsedTime < moveDuration)
        {
            currentItem.transform.position = Vector3.Lerp(itemInitialTransforms[currentItem].originalPosition, targetPosition, elapsedTime / moveDuration);
            currentItem.transform.localScale = Vector3.Lerp(itemInitialTransforms[currentItem].originalScale, targetScale, elapsedTime / moveDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // 确保物体最终位置和缩放精确设置到目标值
        currentItem.transform.position = targetPosition;
        currentItem.transform.localScale = targetScale;

        currentItem.GetComponent<Rigidbody>().isKinematic = true;
        isInspecting = true;

        // 启用旋转功能
        currentItem.GetComponent<RotateObjects>().enabled = true;
    }

    // 停止检查物品，恢复状态
    void StopInspecting()
    {
        if (currentItem != null && !isRestoring)
        {
            isRestoring = true; // 防止多次调用

            Rigidbody rb = currentItem.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.isKinematic = false;
            }

            // 恢复到该物体的初始位置、旋转和缩放
            RestoreInitialTransform(currentItem);

            // 禁用旋转功能
            currentItem.GetComponent<RotateObjects>().enabled = false;

            isRestoring = false; // 恢复标志位
        }

        isInspecting = false;

        EnablePlayerControls();
    }

    // 保存物体的初始变换
    void SaveInitialTransform(GameObject item)
    {
        ItemTransformData initialData = new ItemTransformData
        {
            originalPosition = item.transform.position,
            originalRotation = item.transform.rotation,
            originalScale = item.transform.localScale
        };
        itemInitialTransforms[item] = initialData;

        // Debug日志输出
        Debug.Log($"Item: {item.name} 初始位置记录: {initialData.originalPosition}, 初始旋转: {initialData.originalRotation.eulerAngles}, 初始缩放: {initialData.originalScale}");
    }

    // 恢复物体到初始变换
    void RestoreInitialTransform(GameObject item)
    {
        if (itemInitialTransforms.ContainsKey(item))
        {
            ItemTransformData initialData = itemInitialTransforms[item];
            item.transform.position = initialData.originalPosition;
            item.transform.rotation = initialData.originalRotation;
            item.transform.localScale = initialData.originalScale;

            // 允许的误差范围
            float tolerance = 0.001f;

            // 比较位置、旋转和缩放是否在容忍范围内
            bool positionMatch = Vector3.Distance(item.transform.position, initialData.originalPosition) < tolerance;
            bool rotationMatch = Quaternion.Angle(item.transform.rotation, initialData.originalRotation) < tolerance;
            bool scaleMatch = Vector3.Distance(item.transform.localScale, initialData.originalScale) < tolerance;

            // 检查物体是否成功恢复到初始值
            if (!positionMatch || !rotationMatch || !scaleMatch)
            {
                Debug.LogError($"物件没有回到初始值！\n" +
                               $"Current Position: {item.transform.position}, Original: {initialData.originalPosition}\n" +
                               $"Current Rotation: {item.transform.rotation.eulerAngles}, Original: {initialData.originalRotation.eulerAngles}\n" +
                               $"Current Scale: {item.transform.localScale}, Original: {initialData.originalScale}");
            }
            else
            {
                Debug.Log("物件成功恢复到初始值。");
            }
        }
        else
        {
            Debug.LogError($"未找到物件 {item.name} 的初始值记录！");
        }
    }

    // 禁用玩家控制
    void DisablePlayerControls()
    {
        if (playerMovementScript != null)
        {
            playerMovementScript.enabled = false; // 禁用玩家移动脚本
        }

        if (playerCameraScript != null)
        {
            playerCameraScript.enabled = false; // 禁用玩家镜头脚本
        }
    }

    // 启用玩家控制
    void EnablePlayerControls()
    {
        if (playerMovementScript != null)
        {
            playerMovementScript.enabled = true; // 启用玩家移动脚本
        }

        if (playerCameraScript != null)
        {
            playerCameraScript.enabled = true; // 启用玩家镜头脚本
        }
    }
}
*/






using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ItemInteraction : MonoBehaviour
{
    public Transform inspectPosition; // 物品检查位置
    public float resizeScale = 0.7f; // 缩小比例
    public float moveDuration = 1.0f; // 移动和缩小持续时间
    public Camera mainCamera; // 直接引用你的摄像机对象
    public MonoBehaviour playerCameraScript; // 玩家镜头脚本引用
    public MonoBehaviour playerMovementScript; // 玩家移动脚本引用
    private GameObject currentItem; // 当前检查的物品
    private bool isInspecting = false; // 是否在检查模式中
    private bool isRestoring = false; // 是否正在恢复物品状态
    

    // 保存每个物品的初始变换（位置、旋转、缩放）
    private Dictionary<GameObject, ItemTransformData> itemInitialTransforms =
        new Dictionary<GameObject, ItemTransformData>();

    // 数据结构用于存储物体的初始值
    private class ItemTransformData
    {
        public Vector3 originalPosition;
        public Quaternion originalRotation;
        public Vector3 originalScale;
    }

    void Update()
    {
        // F 进行检查
        if (Input.GetKey(KeyCode.F) && !isInspecting)
        {
            PerformRaycast();
        }

        // 按下 Escape 退出检查模式
        if (isInspecting && Input.GetKeyDown(KeyCode.Escape))
        {
            StopInspecting();
        }
    }

    // 射线检测交互物品
    void PerformRaycast()
    {
        if (mainCamera != null)
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                if (hit.transform.CompareTag("NPC"))
                {
                    StartCoroutine(StartInspecting(hit.transform.gameObject));
                }
            }
        }
        else
        {
            Debug.LogError("Main camera reference is not set.");
        }
    }

    // 开始检查物品的协程
    IEnumerator StartInspecting(GameObject item)
    {
        currentItem = item;

        // 如果该物品没有被记录过初始值，则记录一次
        if (!itemInitialTransforms.ContainsKey(currentItem))
        {
            SaveInitialTransform(currentItem);
        }

        // 获取该物品的初始位置和缩放
        Vector3 targetPosition = inspectPosition.position;
        Vector3 targetScale = itemInitialTransforms[currentItem].originalScale * resizeScale;

        DisablePlayerControls();

        // 平滑移动和缩放物品
        float elapsedTime = 0;
        while (elapsedTime < moveDuration)
        {
            currentItem.transform.position = Vector3.Lerp(itemInitialTransforms[currentItem].originalPosition,
                targetPosition, elapsedTime / moveDuration);
            currentItem.transform.localScale = Vector3.Lerp(itemInitialTransforms[currentItem].originalScale,
                targetScale, elapsedTime / moveDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // 确保物体最终位置和缩放精确设置到目标值
        currentItem.transform.position = targetPosition;
        currentItem.transform.localScale = targetScale;

        currentItem.GetComponent<Rigidbody>().isKinematic = true;
        isInspecting = true;

        // 启用旋转功能
        currentItem.GetComponent<RotateObjects>().enabled = true;
    }

    // 停止检查物品，恢复状态
    void StopInspecting()
    {
        if (currentItem != null && !isRestoring)
        {
            isRestoring = true; // 防止多次调用

            Rigidbody rb = currentItem.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.isKinematic = false;
            }

            // 恢复到该物体的初始位置、旋转和缩放
            RestoreInitialTransform(currentItem);

            // 禁用旋转功能
            currentItem.GetComponent<RotateObjects>().enabled = false;

            isRestoring = false; // 恢复标志位
        }

        isInspecting = false;

        EnablePlayerControls();
    }

    // 保存物体的初始变换
    void SaveInitialTransform(GameObject item)
    {
        ItemTransformData initialData = new ItemTransformData
        {
            originalPosition = item.transform.position,
            originalRotation = item.transform.rotation,
            originalScale = item.transform.localScale
        };
        itemInitialTransforms[item] = initialData;

        // Debug日志输出
        Debug.Log(
            $"Item: {item.name} 初始位置记录: {initialData.originalPosition}, 初始旋转: {initialData.originalRotation.eulerAngles}, 初始缩放: {initialData.originalScale}");
    }

    // 恢复物体到初始变换
    void RestoreInitialTransform(GameObject item)
    {
        if (itemInitialTransforms.ContainsKey(item))
        {
            ItemTransformData initialData = itemInitialTransforms[item];
            item.transform.position = initialData.originalPosition;
            item.transform.rotation = initialData.originalRotation;
            item.transform.localScale = initialData.originalScale;

            // 允许的误差范围
            float tolerance = 0.001f;

            // 比较位置、旋转和缩放是否在容忍范围内
            bool positionMatch = Vector3.Distance(item.transform.position, initialData.originalPosition) < tolerance;
            bool rotationMatch = Quaternion.Angle(item.transform.rotation, initialData.originalRotation) < tolerance;
            bool scaleMatch = Vector3.Distance(item.transform.localScale, initialData.originalScale) < tolerance;

            // 检查物体是否成功恢复到初始值
            if (!positionMatch || !rotationMatch || !scaleMatch)
            {
                Debug.LogError($"物件没有回到初始值！\n" +
                               $"Current Position: {item.transform.position}, Original: {initialData.originalPosition}\n" +
                               $"Current Rotation: {item.transform.rotation.eulerAngles}, Original: {initialData.originalRotation.eulerAngles}\n" +
                               $"Current Scale: {item.transform.localScale}, Original: {initialData.originalScale}");
            }
            else
            {
                Debug.Log("物件成功恢复到初始值。");
            }
        }
        else
        {
            Debug.LogError($"未找到物件 {item.name} 的初始值记录！");
        }
    }

    // 禁用玩家控制
    void DisablePlayerControls()
    {
        if (playerMovementScript != null)
        {
            playerMovementScript.enabled = false; // 禁用玩家移动脚本
        }

        if (playerCameraScript != null)
        {
            playerCameraScript.enabled = false; // 禁用玩家镜头脚本
        }
    }

    // 启用玩家控制
    void EnablePlayerControls()
    {
        if (playerMovementScript != null)
        {
            playerMovementScript.enabled = true; // 启用玩家移动脚本
        }

        if (playerCameraScript != null)
        {
            playerCameraScript.enabled = true; // 启用玩家镜头脚本
        }
    }
}

