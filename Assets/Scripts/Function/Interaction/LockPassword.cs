using UnityEngine;

public class password : MonoBehaviour
{
    public string Name = "Name";
    public GameObject objectWithScript; // 將 object 改為 GameObject，方便存取其組件

    public int _one_a;
    public int _two_a;
    public int _three_a;
    public int _four_a;
    public int _five_a;

    private int _one;
    private int _two;
    private int _three;
    private int _four;
    private int _five;

    private DevicesInteraction devicesInteraction; // 存取 DevicesInteraction 腳本的變數

    public void Start()
    {
        _one = 0;
        _two = 0;
        _three = 0;
        _four = 0;
        _five = 0;

        // 嘗試從 objectWithScript 取得 DevicesInteraction 腳本
        if (objectWithScript != null)
        {
            devicesInteraction = objectWithScript.GetComponent<DevicesInteraction>();
            if (devicesInteraction == null)
            {
                Debug.LogWarning("DevicesInteraction 腳本不存在於指定的物件上！");
            }
        }
        else
        {
            Debug.LogWarning("objectWithScript 尚未設定！");
        }
    }

    public void Update()
    {
        inspection();
    }

    public void inspection()
    {
        bool conditionMet = false;

        switch (Name)
        {
            case "1":
                conditionMet = (_one == _one_a);
                break;
            case "2":
                conditionMet = (_one == _one_a && _two == _two_a);
                break;
            case "3":
                conditionMet = (_one == _one_a && _two == _two_a && _three == _three_a);
                break;
            case "4":
                conditionMet = (_one == _one_a && _two == _two_a && _three == _three_a && _four == _four_a);
                break;
            case "5":
                conditionMet = (_one == _one_a && _two == _two_a && _three == _three_a && _four == _four_a && _five == _five_a);
                break;
        }

        // 當條件符合時，執行 Interact_Devices 方法
        if (conditionMet && devicesInteraction != null)
        {
            devicesInteraction.Unlock();
            //devicesInteraction.Interact_Devices();
        }
    }
}
