using System.Collections;
using UnityEngine;

public class EleButton : MonoBehaviour
{
    public string Name = "p";
    private bool _can_move = true;

    [Header("移動秒數")]
    public float movementDuration = 0.5f;

    [Header("停留秒數")]
    public float stayDuration = 0.5f;

    [Header("移動速度")]
    public float speed = 0.01f;

    public void Interact_Devices()
    {
        if (!_can_move) return; // 如果正在執行動作，不允許再次觸發

        switch (Name)
        {
            case "r":
                StartCoroutine(Move_Button(Vector3.right));
                break;
            case "p":
                StartCoroutine(Move_Button(Vector3.forward));
                break;
            case "u":
                StartCoroutine(Move_Button(Vector3.up));
                break;
        }
    }

    private IEnumerator Move_Button(Vector3 direction)
    {
        if (_can_move)
        {
            _can_move = false; // 禁止重複觸發
            
            
            SoundManager.Instance.PlaySound(SoundManager.Instance.button);
            
            Vector3 startPosition = transform.position;
            Vector3 targetPosition = startPosition + direction * speed * movementDuration;

            // 移動到目標位置
            yield return StartCoroutine(Move(startPosition, targetPosition));

            // 停留指定時間
            yield return new WaitForSeconds(stayDuration);

            // 回到原位
            yield return StartCoroutine(Move(targetPosition, startPosition));

            _can_move = true; // 允許再次觸發
        }
    }

    private IEnumerator Move(Vector3 start, Vector3 end)
    {
        float elapsedTime = 0f;

        while (elapsedTime < movementDuration)
        {
            transform.position = Vector3.Lerp(start, end, elapsedTime / movementDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // 確保移動到最終位置
        transform.position = end;
    }
}