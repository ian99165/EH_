using System.Collections;
using UnityEngine;

public class MoveObject : MonoBehaviour
{
    public float speed = 1f;
    public float movementDuration = 3f; // 可控變數，默認為3秒
    private bool _can_move;
    private bool _open;

    private void Start()
    {
        _can_move = true;
        _open = false;
    }

    // 通用的移動函數
    private IEnumerator Move(Vector3 direction)
    {
        if (_can_move)
        {
            Debug.Log($"Move {direction}");

            float elapsedTime = 0f;
            Vector3 startPosition = transform.position;

            // 移動到目標位置
            while (elapsedTime < movementDuration)
            {
                transform.position = Vector3.Lerp(startPosition, startPosition + direction * speed * movementDuration, 
                    elapsedTime / movementDuration);
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            // 確保到達精確的位置
            transform.position = startPosition + direction * speed * movementDuration;
            _can_move = false;
            _open = true;
            yield return null;
        }

        if (_open)
        {
            Debug.Log($"Move {direction} Close");

            float elapsedTime = 0f;
            Vector3 startPosition = transform.position;

            // 移動回原來的位置
            while (elapsedTime < movementDuration)
            {
                transform.position = Vector3.Lerp(startPosition, startPosition - direction * speed * movementDuration, 
                    elapsedTime / movementDuration);
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            // 確保到達精確的位置
            transform.position = startPosition - direction * speed * movementDuration;
            _can_move = true;
            _open = false;
            yield return null;
        }
    }

    // 上下左右前後的移動，使用通用函數進行移動
    public IEnumerator Move_Up() 
    {
        yield return StartCoroutine(Move(Vector3.up));
    }

    public IEnumerator Move_Down() 
    {
        yield return StartCoroutine(Move(Vector3.down));
    }

    public IEnumerator Move_Left() 
    {
        yield return StartCoroutine(Move(Vector3.left));
    }

    public IEnumerator Move_Right() 
    {
        yield return StartCoroutine(Move(Vector3.right));
    }

    public IEnumerator Move_Forward() 
    {
        yield return StartCoroutine(Move(Vector3.forward));
    }

    public IEnumerator Move_Back() 
    {
        yield return StartCoroutine(Move(Vector3.back));
    }

    // 取消當前移動
    public void CancelMove()
    {
        _can_move = true;
        _open = false;
    }
}
