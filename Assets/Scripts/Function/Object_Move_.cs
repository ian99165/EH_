using System.Collections;
using UnityEngine;

public class Object_Move_ : MonoBehaviour
{
    public float speed = 1f;
    public float movementDuration = 3f; // 可控變數，默認為3秒
    private Coroutine moveCoroutine;
    private bool canMove;
    
    private void Start()
    {
        canMove = true; // 確保初始狀態為可移動
    }
    
    public IEnumerator Move_Up() // 上移
    {
        Debug.Log("Move_Up");
        float elapsedTime = 0f;
        Vector3 startPosition = transform.position;

        while (elapsedTime < movementDuration) 
        {
            transform.position = Vector3.Lerp(startPosition, startPosition + Vector3.up * speed * movementDuration, elapsedTime / movementDuration);
            elapsedTime += Time.deltaTime;
            yield return null; 
        }
        transform.position = startPosition + Vector3.up * speed * movementDuration;
        moveCoroutine = null;
        canMove = false;
    }

    public IEnumerator Move_Down() // 下移
    {
        Debug.Log("Move_Down");
        float elapsedTime = 0f;
        Vector3 startPosition = transform.position;

        while (elapsedTime < movementDuration) 
        {
            transform.position = Vector3.Lerp(startPosition, startPosition + Vector3.down * speed * movementDuration, elapsedTime / movementDuration);
            elapsedTime += Time.deltaTime;
            yield return null; 
        }
        transform.position = startPosition + Vector3.down * speed * movementDuration;
        moveCoroutine = null;
        canMove = false;
    }

    public IEnumerator Move_R() // 右移
    {
        if (canMove)
        {
            Debug.Log("Move_R");
            float elapsedTime = 0f;
            Vector3 startPosition = transform.position;

            while (elapsedTime < movementDuration)
            {
                transform.position = Vector3.Lerp(startPosition,
                    startPosition + Vector3.right * speed * movementDuration, elapsedTime / movementDuration);
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            transform.position = startPosition + Vector3.right * speed * movementDuration;
            moveCoroutine = null;
            canMove = false;
        }
    }

    public IEnumerator Move_L() // 左移
    {
        Debug.Log("Move_L");
        float elapsedTime = 0f;
        Vector3 startPosition = transform.position;

        while (elapsedTime < movementDuration) 
        {
            transform.position = Vector3.Lerp(startPosition, startPosition + Vector3.left * speed * movementDuration, elapsedTime / movementDuration);
            elapsedTime += Time.deltaTime;
            yield return null; 
        }
        transform.position = startPosition + Vector3.left * speed * movementDuration;
        moveCoroutine = null;
        canMove = false;
    }

    public IEnumerator Move_Push() // 推出
    {
        Debug.Log("Move_Push");
        float elapsedTime = 0f;
        Vector3 startPosition = transform.position;

        while (elapsedTime < movementDuration) 
        {
            transform.position = Vector3.Lerp(startPosition, startPosition + Vector3.forward * speed * movementDuration, elapsedTime / movementDuration);
            elapsedTime += Time.deltaTime;
            yield return null; 
        }
        transform.position = startPosition + Vector3.forward * speed * movementDuration;
        moveCoroutine = null;
        canMove = false;
    }

    public IEnumerator Move_Pull() // 推入
    {
        Debug.Log("Move_Pull");
        float elapsedTime = 0f;
        Vector3 startPosition = transform.position;

        while (elapsedTime < movementDuration) 
        {
            transform.position = Vector3.Lerp(startPosition, startPosition + Vector3.back * speed * movementDuration, elapsedTime / movementDuration);
            elapsedTime += Time.deltaTime;
            yield return null; 
        }
        transform.position = startPosition + Vector3.back * speed * movementDuration;
        moveCoroutine = null;
        canMove = false;
    }
}