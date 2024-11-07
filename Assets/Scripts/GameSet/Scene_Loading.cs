using UnityEngine;
using UnityEngine.SceneManagement;

public class Scene_Loading : MonoBehaviour
{
    private int _scene = 0;

    public void LoadScene()
    {
        switch (_scene)
        {
            case 1:
                SceneManager.LoadScene("S1"); 
                break;
            case 2:
                SceneManager.LoadScene("S2");
                break;
            case 3:
                SceneManager.LoadScene("S3");
                break;
            case 4:
                SceneManager.LoadScene("S4");
                break;
            case 5:
                SceneManager.LoadScene("S5");
                break;
            default:
                Debug.Log("無法載入場景");
                break;
        }
    }

    public void SetScene()
    {
        _scene++;
    }
}
