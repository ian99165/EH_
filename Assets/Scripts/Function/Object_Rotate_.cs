using UnityEngine;
using System.Collections;

public class Object_Rotate_ : MonoBehaviour
{
    public float rotationSpeed = 30f; // 旋轉速度
    private float duration = 1f; // 旋轉持續時間

    public IEnumerator Rotate_R() // 向右推開
    {
        float elapsedTime = 0f; // 重置經過的時間

        while (elapsedTime < duration) // 使用 while 循環直到經過的時間達到持續時間
        {
            float angle = rotationSpeed * Time.deltaTime; // 計算當前幀的旋轉角度
            transform.Rotate(Vector3.up, angle); // 進行旋轉
            elapsedTime += Time.deltaTime; // 更新經過的時間
            yield return null; // 等待下一幀
        }
    }  

    public IEnumerator Rotate_L() // 向左拉開
    {
        float elapsedTime = 0f; // 重置經過的時間

        while (elapsedTime < duration) // 使用 while 循環直到經過的時間達到持續時間
        {
            float angle = -rotationSpeed * Time.deltaTime; // 使用負值以達到反方向旋轉
            transform.Rotate(Vector3.up, angle); // 進行旋轉
            elapsedTime += Time.deltaTime; // 更新經過的時間
            yield return null; // 等待下一幀
        }
    }   
}