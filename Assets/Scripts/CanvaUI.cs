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
        CameraSp.enabled = false;
        
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
        CameraSp.enabled = true;
        
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        
    }
}