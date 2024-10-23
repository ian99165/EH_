//物件平移功能(機關)
using System.Collections;
using UnityEngine;

public class Object_Move_ : MonoBehaviour
{
    public float speed = 1f;
    private Coroutine moveCoroutine;

    public IEnumerator Move_Up()//上移
    {
        Debug.Log("Move_Up");
        float elapsedTime = 0f;
        Vector3 startPosition = transform.position;

        while (elapsedTime < 3f) 
        {
            transform.position = Vector3.Lerp(startPosition, startPosition + Vector3.up * speed * 3f, elapsedTime / 3f);
            elapsedTime += Time.deltaTime;
            yield return null; 
        }
        transform.position = startPosition + Vector3.up * speed * 3f;
        moveCoroutine = null;
    }

    public IEnumerator Move_Down()//下移
    {
        
        Debug.Log("Move_Down");
        float elapsedTime = 0f;
        Vector3 startPosition = transform.position;

        while (elapsedTime < 3f) 
        {
            transform.position = Vector3.Lerp(startPosition, startPosition + Vector3.down * speed * 3f, elapsedTime / 3f);
            elapsedTime += Time.deltaTime;
            yield return null; 
        }
        transform.position = startPosition + Vector3.down * speed * 3f;
        moveCoroutine = null;
    }
    
    public IEnumerator Move_R()//右移
    {
        Debug.Log("Move_Down");
        float elapsedTime = 0f;
        Vector3 startPosition = transform.position;

        while (elapsedTime < 3f) 
        {
            transform.position = Vector3.Lerp(startPosition, startPosition + Vector3.right * speed * 3f, elapsedTime / 3f);
            elapsedTime += Time.deltaTime;
            yield return null; 
        }
        transform.position = startPosition + Vector3.right * speed * 3f;
        moveCoroutine = null;
    }
    
    public IEnumerator Move_L()//左移
    {
        Debug.Log("Move_Down");
        float elapsedTime = 0f;
        Vector3 startPosition = transform.position;

        while (elapsedTime < 3f) 
        {
            transform.position = Vector3.Lerp(startPosition, startPosition + Vector3.left * speed * 3f, elapsedTime / 3f);
            elapsedTime += Time.deltaTime;
            yield return null; 
        }
        transform.position = startPosition + Vector3.left * speed * 3f;
        moveCoroutine = null;
    }
    
    public IEnumerator Move_Push()//推出
    {
        Debug.Log("Move_Down");
        float elapsedTime = 0f;
        Vector3 startPosition = transform.position;

        while (elapsedTime < 3f) 
        {
            transform.position = Vector3.Lerp(startPosition, startPosition + Vector3.forward * speed * 3f, elapsedTime / 3f);
            elapsedTime += Time.deltaTime;
            yield return null; 
        }
        transform.position = startPosition + Vector3.forward * speed * 3f;
        moveCoroutine = null;
    }

    public IEnumerator Move_Pull()//推出
    {
        Debug.Log("Move_Down");
        float elapsedTime = 0f;
        Vector3 startPosition = transform.position;

        while (elapsedTime < 3f) 
        {
            transform.position = Vector3.Lerp(startPosition, startPosition + Vector3.back * speed * 3f, elapsedTime / 3f);
            elapsedTime += Time.deltaTime;
            yield return null; 
        }
        transform.position = startPosition + Vector3.back * speed * 3f;
        moveCoroutine = null;
    }
}
