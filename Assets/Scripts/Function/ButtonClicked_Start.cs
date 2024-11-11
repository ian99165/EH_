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
        Debug.Log("Button_Start");
    }

    private void Button_Exit() //離開遊戲
    {
        Debug.Log("Button_Exit");
    }
}
