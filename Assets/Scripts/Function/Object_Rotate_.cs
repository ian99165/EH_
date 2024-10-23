//物件旋轉功能(門)
using UnityEngine;

public class Object_Rotate_ : MonoBehaviour
{
    //Object_Rotate_ object_rotate_ = GetComponent<Object_Rotate_>();
    public float rotationSpeed = 30f;
    private float duration = 1f;
    private float elapsedTime = 0f;

    public void Rotate_R()  //object_rotate_.Rotate_R();//推開
    {
        if (elapsedTime < duration)
        {
            float angle = rotationSpeed * Time.deltaTime;
            transform.Rotate(Vector3.up, angle);
            elapsedTime += Time.deltaTime;
        }
    }  
    
    public void Rotate_L()  //object_rotate_.Rotate_L();//拉開
    {
        if (elapsedTime < duration)
        {
            float angle = -rotationSpeed * Time.deltaTime; // 使用負值以達到反方向旋轉
            transform.Rotate(Vector3.up, angle);
            elapsedTime += Time.deltaTime;
        }
    }   
}