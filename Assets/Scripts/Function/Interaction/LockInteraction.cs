using System.Collections;
using UnityEngine;

public class LockInteraction : MonoBehaviour
{
    public string Name = "Name";
    public string Number = "Number";
    public LockPassword lockPassword;

    // 移動變數
    public float speed = 1f;
    public float movementDuration = 3f;
    private bool _can_move;

    // 旋轉變數
    public float rotationSpeed = 45f;

    // 按鈕交互
    private bool _isInteracting = false;  // 用來防止在進行動作時再次點擊

    private void Start()
    {
        _can_move = true;
    }

    // **互動動作完成後才執行檢查**
    public void Interact_Devices()
    {
        // 確保在動作完成前不再觸發
        if (_isInteracting) return;
        
        StartCoroutine(PerformInteraction());
    }

    private IEnumerator PerformInteraction()
    {
        _isInteracting = true; // 設置為正在交互，防止重複點擊

        switch (Name)
        {
            case "M_u":
                SwitchPass();
                yield return StartCoroutine(Move_Up());
                break;
            case "M_p":
                SwitchPass();
                yield return StartCoroutine(Move_Forward());
                break;
            case "M_r":
                SwitchPass();
                yield return StartCoroutine(Move_Right());
                break;
            case "R_u":
                SwitchPass();
                yield return StartCoroutine(Rotate_Up());
                break;
            case "R_r":
                SwitchPass();
                yield return StartCoroutine(Rotate_Right());
                break;
            default:
                Debug.Log("No action executed.");
                yield break;
        }

        // **互動動作結束後執行檢查**
        if (lockPassword != null)
        {
            lockPassword.inspection();
        }

        _isInteracting = false; // 動作完成，允許再次點擊
    }

    // **移動函數**
    private IEnumerator Move(Vector3 direction)
    {
        if (_can_move)
        {
            _can_move = false;
            float elapsedTime = 0f;
            Vector3 startPosition = transform.position;
            Vector3 targetPosition = startPosition + direction * speed * movementDuration;

            while (elapsedTime < movementDuration)
            {
                transform.position = Vector3.Lerp(startPosition, targetPosition, elapsedTime / movementDuration);
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            transform.position = targetPosition;
            _can_move = true;
        }
    }

    public IEnumerator Move_Up() { yield return StartCoroutine(Move(Vector3.up)); }
    public IEnumerator Move_Right() { yield return StartCoroutine(Move(Vector3.right)); }
    public IEnumerator Move_Forward() { yield return StartCoroutine(Move(Vector3.forward)); }

    // **旋轉函數**
    private IEnumerator Rotate(Vector3 rotationAxis, float angle)
    {
        if (_can_move)
        {
            _can_move = false;
            float elapsedTime = 0f;
            Quaternion startRotation = transform.rotation;
            Quaternion targetRotation = startRotation * Quaternion.Euler(rotationAxis * angle);

            while (elapsedTime < movementDuration)
            {
                transform.rotation = Quaternion.Lerp(startRotation, targetRotation, elapsedTime / movementDuration);
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            transform.rotation = targetRotation;
            _can_move = true;
        }
    }

    public IEnumerator Rotate_Right() { yield return StartCoroutine(Rotate(Vector3.up, 36f)); }
    public IEnumerator Rotate_Up() { yield return StartCoroutine(Rotate(Vector3.right, -rotationSpeed * movementDuration)); }
    
    void SwitchPass()
    {
        if (lockPassword != null)
        {
            switch (Number)
            {
                case "1":
                    lockPassword.One = IncrementWithReset(lockPassword.One, lockPassword.maxValue);
                    break;
                case "2":
                    lockPassword.Two = IncrementWithReset(lockPassword.Two, lockPassword.maxValue);
                    break;
                case "3":
                    lockPassword.Three = IncrementWithReset(lockPassword.Three, lockPassword.maxValue);
                    break;
                case "4":
                    lockPassword.Four = IncrementWithReset(lockPassword.Four, lockPassword.maxValue);
                    break;
                case "5":
                    lockPassword.Five = IncrementWithReset(lockPassword.Five, lockPassword.maxValue);
                    break;
            }
        }
    }

    // 遞增並檢查是否達到最大值，若達到則重置為 0
    private int IncrementWithReset(int value, int maxValue)
    {
        value++;
        if (value > maxValue)
        {
            value = 0;  // 超過最大值時重置
        }
        return value;
    }
}
