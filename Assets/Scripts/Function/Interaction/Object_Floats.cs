using UnityEngine;

public class FloatingItem : MonoBehaviour
{
    public Transform target; // 要跟隨的物件
    public float floatSpeed = 1f; // 晃動速度
    public float floatAmplitude = 0.2f; // 晃動幅度

    private Vector3 offset; // 與目標物件的初始偏移量

    void Start()
    {
        if (target != null)
        {
            offset = transform.position - target.position; // 計算初始偏移量
        }
    }

    void Update()
    {
        if (target != null)
        {
            // 目標位置加上初始偏移
            Vector3 targetPosition = target.position + offset;

            // 計算晃動值（世界座標）
            float newY = Mathf.Sin(Time.time * floatSpeed) * floatAmplitude;

            // 更新物件位置（使用世界座標）
            transform.position = new Vector3(targetPosition.x, targetPosition.y + newY, targetPosition.z);
        }
    }
}