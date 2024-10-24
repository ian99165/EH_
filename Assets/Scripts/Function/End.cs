using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class End : MonoBehaviour
{
    [SerializeField] private string targetTag = "Player"; 
    [SerializeField] private Image endImage; 
    [SerializeField] private float fadeDuration = 1f; 

    private void Start()
    {
        if (endImage != null)
        {
            endImage.color = new Color(endImage.color.r, endImage.color.g, endImage.color.b, 0);
            endImage.gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(targetTag))
        {
            ExecuteEnd();
        }
    }

    private void ExecuteEnd()
    {
        if (endImage != null)
        {
            endImage.gameObject.SetActive(true);
            StartCoroutine(FadeIn());
            
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
    }

    private IEnumerator FadeIn()
    {
        float elapsedTime = 0f;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Clamp01(elapsedTime / fadeDuration);
            endImage.color = new Color(endImage.color.r, endImage.color.g, endImage.color.b, alpha); // 更新 alpha 值
            yield return null;
        }

        endImage.color = new Color(endImage.color.r, endImage.color.g, endImage.color.b, 1);

        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("Start"); 
    }
}