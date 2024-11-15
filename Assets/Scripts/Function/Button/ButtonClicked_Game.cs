using UnityEngine;
using UnityEngine.UI;

public class ButtonClick_Game : MonoBehaviour
{
    [SerializeField]private Button _button_start;
    [SerializeField]private Button _button_Exit;
    [SerializeField]private Button _button_Back;
    [SerializeField]private Button _button_Settings;
    [SerializeField]private Button _button_Save;

    void Start()
    {
        _button_start.onClick.AddListener(Button_Start);
        _button_Exit.onClick.AddListener(Button_Exit);
        _button_Back.onClick.AddListener(Button_Back);
        _button_Settings.onClick.AddListener(Button_Settings);
        _button_Save.onClick.AddListener(Button_Save);
    }

    private void Button_Start() //開始遊戲
    {
        Debug.Log("Button_Start");
    }

    private void Button_Exit() //離開遊戲
    {
        Application.Quit();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }

    private void Button_Back() //返回遊戲
    {
        Debug.Log("Button_Back");
    }

    private void Button_Settings() //設定
    {
        Debug.Log("Button_Settings");
    }

    private void Button_Save() //存檔
    {
        Debug.Log("Button_Save");
    }
}
