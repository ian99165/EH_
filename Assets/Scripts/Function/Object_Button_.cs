using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Fungus;

public class Object_Button_ : MonoBehaviour
{
    public float rotationAngle = 45f; // 每次旋轉的角度
    public float rotationDuration = 1f; // 旋轉持續時間
    public string ObjectName;
    public string Number;

    public GameObject gameobject; 
    private bool isRotating = false; // 標記是否正在旋轉

    [SerializeField]
    private Button _button_I;
    
    public Flowchart variable;
    
    void Start()
    {
        _button_I.onClick.AddListener(OnButtonClick);
    }

    private void OnButtonClick()
    {
        if (!isRotating) // 檢查是否正在旋轉
        {
            StartCoroutine(RotateObject());
            variable.ExecuteBlock("SI_I");
        }
        /*Variable_Answer_ variable_answer_ = GetComponent<Variable_Answer_>();
        if (ObjectName == "SI_I")
        {
            if (Number == "I")
            {
            }

            if(Number == "II")
            {
            }

            if (Number == "III")
            {
            }
        }*/
    }

    private IEnumerator RotateObject()
    {
        isRotating = true; // 設置為正在旋轉
        float elapsedTime = 0f;
        Quaternion startingRotation = gameobject.transform.rotation;
        Quaternion targetRotation = startingRotation * Quaternion.Euler(0, -rotationAngle, 0);

        while (elapsedTime < rotationDuration)
        {
            gameobject.transform.rotation = Quaternion.Slerp(startingRotation, targetRotation, elapsedTime / rotationDuration);
            elapsedTime += Time.deltaTime;
            yield return null; // 等待下一幀
        }

        gameobject.transform.rotation = targetRotation; // 確保最終角度正確
        isRotating = false; // 旋轉結束，允許再次點擊
    }
}