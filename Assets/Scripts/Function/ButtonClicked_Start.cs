using UnityEngine;
using UnityEngine.UI;

public class ButtonClicked_Start : MonoBehaviour
{
    [SerializeField]private Button _button_start;
    [SerializeField]private Button _button_Exit;

    void Start()
    {
        _button_start.onClick.AddListener(Button_Start);
        _button_Exit.onClick.AddListener(Button_Exit);
    }

    private void Button_Start() //開始遊戲
    {
        Scene_Loading  scene_loading= GetComponent<Scene_Loading>();
        scene_loading.SetScene();
        scene_loading.LoadScene();
    }

    private void Button_Exit() //離開遊戲
    {
        Application.Quit();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
