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
        // 獲取 ControllerMovement3D 腳本的實例
        MoveSp = player.GetComponent<ControllerMovement3D>();
        CameraSp = Camera.GetComponent<CameraController>();
        
        Exit.onClick.AddListener(CloseCanva);
    }

    public void OpenCanva()        // 打開UI
    {
        OpenUI.SetActive(true);
        Exit.gameObject.SetActive(true);
    }

    public void MsOFF()            // 停止角色移動，禁用B腳本
    {
        MoveSp.enabled = false;
        CameraSp.enabled = false;
        
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void CloseCanva()
    {
        // 關閉UI
        OpenUI.SetActive(false);
        Exit.gameObject.SetActive(false);
    }

    public void MsON()
    {
        // 重新啟用角色移動
        MoveSp.enabled = true;
        CameraSp.enabled = true;
        
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        
    }
}