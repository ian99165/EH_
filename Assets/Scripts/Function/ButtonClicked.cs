using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class ButtonClicked : MonoBehaviour
{
    [SerializeField] private Button _button_Start;
    [SerializeField] private Button _button_Exit;
    [SerializeField] private Image _fadeImage;
    private Vector3 initialScale;

    void Start()
    {
        _button_Start.onClick.AddListener(Start_Game);
        _button_Exit.onClick.AddListener(Exit_Game);

        _fadeImage.color = new Color(_fadeImage.color.r, _fadeImage.color.g, _fadeImage.color.b, 1);
        initialScale = _fadeImage.transform.localScale;
    }

    private void Start_Game()
    {
        StartCoroutine(FadeOutAndSwitchScene());
    }

    private IEnumerator FadeOutAndSwitchScene()
    {
        float duration = 1f;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;

            float alpha = Mathf.Clamp01(1 - (elapsedTime / duration));
            _fadeImage.color = new Color(_fadeImage.color.r, _fadeImage.color.g, _fadeImage.color.b, alpha);
            
            float scale = Mathf.Lerp(1, 1.5f, elapsedTime / duration);
            _fadeImage.transform.localScale = initialScale * scale;

            yield return null;
        }

        _button_Start.gameObject.SetActive(false);
        _button_Exit.gameObject.SetActive(false);

        SceneManager.LoadScene("Game_S1");
    }

    private void Exit_Game()
    {
        Application.Quit();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}