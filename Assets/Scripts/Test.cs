using UnityEngine;

public class test : MonoBehaviour
{
    public float rotationSpeed = 110f;
    
    private Vector3 lastMousePosition;

    void Update()
    {
        if (gameObject.CompareTag("UI"))
        {
            // 按下左鍵時開始旋轉
            if (Input.GetMouseButtonDown(0))
            {
                lastMousePosition = Input.mousePosition;
            }

            // 鼠標拖動時旋轉
            if (Input.GetMouseButton(0))
            {
                Vector3 delta = Input.mousePosition - lastMousePosition;
                float rotationX = delta.x * rotationSpeed * Time.deltaTime;
                float rotationY = delta.y * rotationSpeed * Time.deltaTime;

                // 根據鼠標移動來旋轉物件
                transform.Rotate(Vector3.up, -rotationX, Space.World);
                transform.Rotate(Vector3.right, rotationY, Space.World);

                lastMousePosition = Input.mousePosition;

            }
        }
    }
}