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
            case "M_u" :
                StartCoroutine(Move_Up());
                break;
            case "M_p" :
                StartCoroutine(Move_Forward());
                break;
            case "M_r" :
                StartCoroutine(Move_Right());
                break;
            case "R_u":
                StartCoroutine(Rotate_Up());
                break;
            case "R_r":
                StartCoroutine(Rotate_Right());
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
    
    //旋轉
    public float rotationSpeed = 45f; // 每秒旋轉的角度

    private IEnumerator Rotate(Vector3 rotationAxis, float angle)
    {
        if (_can_move)
        {
            _can_move = false;

            //Debug.Log($"Rotate {rotationAxis} by {angle} degrees");

            float elapsedTime = 0f;
            Quaternion startRotation = transform.rotation;

            Quaternion targetRotation = startRotation * Quaternion.Euler(rotationAxis * angle);

            while (elapsedTime < movementDuration)
            {
                float t = elapsedTime / movementDuration;
                transform.rotation = Quaternion.Lerp(startRotation, targetRotation, t);
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            transform.rotation = targetRotation;

            _can_move = true;
        }
    }

    public IEnumerator Rotate_Left()
    {
        if (!_open)
        {
            Debug.Log("旋轉物件到左邊");
            yield return StartCoroutine(Rotate(Vector3.up, -rotationSpeed * movementDuration)); // Y軸反向旋轉
            _open = true;
            yield break;
        }

        if (_open)
        {
            yield return StartCoroutine(Rotate(Vector3.up, rotationSpeed * movementDuration)); // Y軸正向旋轉
            _open = false;
        }
    }

    public IEnumerator Rotate_Right()
    {
        if (!_open)
        {
            Debug.Log("旋轉物件到右邊");
            yield return StartCoroutine(Rotate(Vector3.up, rotationSpeed * movementDuration)); // Y軸正向旋轉
            _open = true;
            yield break;
        }

        if (_open)
        {
            yield return StartCoroutine(Rotate(Vector3.up, -rotationSpeed * movementDuration)); // Y軸反向旋轉
            _open = false;
        }
    }

    public IEnumerator Rotate_Up()
    {
        if (!_open)
        {
            Debug.Log("旋轉物件到上面");
            yield return StartCoroutine(Rotate(Vector3.right, -rotationSpeed * movementDuration)); // X軸反向旋轉
            _open = true;
            yield break;
        }
        if (_open)
        {
            yield return StartCoroutine(Rotate(Vector3.right, rotationSpeed * movementDuration)); // X軸正向旋轉
            _open = false;
        }
    }

    public IEnumerator Rotate_Down()
    {
        if (!_open)
        {
            Debug.Log("旋轉物件到下面");
            yield return StartCoroutine(Rotate(Vector3.right, rotationSpeed * movementDuration)); // X軸正向旋轉
            _open = true;
            yield break;
        }
        if (_open)
        {
            yield return StartCoroutine(Rotate(Vector3.right, -rotationSpeed * movementDuration)); // X軸反向旋轉
            _open = false;
        }
    }
}
