//物件旋轉功能(門)
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Object_Move : MonoBehaviour
{
    public float rotationSpeed = 30f;
    private float duration = 1f;
    private float elapsedTime = 0f;

    void Update()
    {
        if (elapsedTime < duration)
        {
            float angle = rotationSpeed * Time.deltaTime;
            transform.Rotate(Vector3.up, angle);
            elapsedTime += Time.deltaTime;
        }
    }
}