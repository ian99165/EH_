using UnityEngine;
using System.Collections;

public class State_Object : MonoBehaviour
{
    public GameObject Object;
    
    public float speed = 0.5f;
    public float movementDuration = 0.5f; // 可控變數，默認為3秒
    private Coroutine moveCoroutine;
    private Collider objectCollider;
    
    void Start()
    {
        objectCollider = GetComponent<Collider>();
    }
    
    public void Set_State_Doll()
    {
            Debug.Log("Set_State_Doll");
            Object.SetActive(!Object.activeSelf);
            Fungus_Sp fungusSp_ = GetComponent<Fungus_Sp>();
            fungusSp_.ChangeState_D();
    }

    public IEnumerator Set_State_SI_I()
    {
        float elapsedTime = 0f;
        Vector3 startPosition = transform.position;

        while (elapsedTime < movementDuration)
        {
            transform.position = Vector3.Lerp(startPosition, startPosition + Vector3.up * speed * movementDuration,
                elapsedTime / movementDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.position = startPosition + Vector3.up * speed * movementDuration;
        moveCoroutine = null;
        
        objectCollider.enabled = false;
    }
}
