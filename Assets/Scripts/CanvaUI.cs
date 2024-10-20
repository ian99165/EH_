using UnityEngine;
using UnityEngine.UI;

public class CanvaUI : MonoBehaviour
{
    [SerializeField] 
    private Button Exit;
    
    public GameObject OpenUI;
    public GameObject player;
    public GameObject Camera;
    
    private ControllerMovement3D MoveSp;
    private CameraController CameraSp;

    
    void Start()
    {
        Exit.onClick.AddListener(CloseCanva);
    }

    public void OpenCanva()        // 打開UI
    {
        OpenUI.SetActive(true);
        Exit.gameObject.SetActive(true);
    }

    public void MsOFF()            // 停止角色移動，禁用B腳本
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void CloseCanva()
    {
        OpenUI.SetActive(false);
        Exit.gameObject.SetActive(false);
    }

    public void MsON()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        
    }
}