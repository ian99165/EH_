using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DevicesInteraction : MonoBehaviour
{
    public string Name = "Name";
    
    public void Interact_Devices()
    {
        switch (Name)
        {
            case "書桌抽屜" :
                Debug.Log("書桌抽屜");
                StartCoroutine(Move_Up());
                break;
            default:
                Debug.Log("No thing");
                break;
        }
    }
    
    //移動
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
            // 禁止再次移動
            _can_move = false;

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

            // 移動結束，允許再次移動
            _can_move = true;
        }
    }

    // 上下左右前後的移動，使用通用函數進行移動
    public IEnumerator Move_Up() 
    {
        if (!_open)
        {
            yield return StartCoroutine(Move(Vector3.up));
            _open = true;
            yield break;
        }
        if (_open)
        {
            yield return StartCoroutine(Move(Vector3.down));
            _open = false;
        }
    }

    public IEnumerator Move_Down() 
    {
        if (!_open)
        {
            yield return StartCoroutine(Move(Vector3.down));
            _open = true;
            yield break;
        }
        if (_open)
        {
            yield return StartCoroutine(Move(Vector3.up));
            _open = false;
        }
    }

    public IEnumerator Move_Left() 
    {
        if (!_open)
        {
            yield return StartCoroutine(Move(Vector3.left));
            _open = true;
            yield break;
        }
        if (_open)
        {
            yield return StartCoroutine(Move(Vector3.right));
            _open = false;
        }
    }

    public IEnumerator Move_Right()
    {
        if (!_open)
        {
            yield return StartCoroutine(Move(Vector3.right));
            _open = true;
            yield break;
        }
        if (_open)
        {
            yield return StartCoroutine(Move(Vector3.left));
            _open = false;
        }
    }

    public IEnumerator Move_Forward()
    {
        if (!_open)
        {
            yield return StartCoroutine(Move(Vector3.forward));
            _open = true;
            yield break;
        }
        if (_open)
        {
            yield return StartCoroutine(Move(Vector3.back));
            _open = false;
        }
    }

    public IEnumerator Move_Back()
    {
        if (!_open)
        {
            yield return StartCoroutine(Move(Vector3.back));
            _open = true;
            yield break;
        }
        if (_open)
        {
            yield return StartCoroutine(Move(Vector3.forward));
            _open = false;
        }
    }
}
