using UnityEngine;

public class LockPassword : MonoBehaviour
{
    public string Name = "Name";
    public GameObject objectWithScript;

    public int _one_a;
    public int _two_a;
    public int _three_a;
    public int _four_a;
    public int _five_a;

    public int _one;
    public int _two;
    public int _three;
    public int _four;
    public int _five;

    private DevicesInteraction devicesInteraction;

    // 新增的最大最小值設定
    public int minValue = 0;
    public int maxValue = 9;

    public int One
    {
        get { return _one; }
        set { _one = Mathf.Clamp(value, minValue, maxValue); } // 設定範圍
    }

    public int Two
    {
        get { return _two; }
        set { _two = Mathf.Clamp(value, minValue, maxValue); } // 設定範圍
    }

    public int Three
    {
        get { return _three; }
        set { _three = Mathf.Clamp(value, minValue, maxValue); } // 設定範圍
    }

    public int Four
    {
        get { return _four; }
        set { _four = Mathf.Clamp(value, minValue, maxValue); } // 設定範圍
    }

    public int Five
    {
        get { return _five; }
        set { _five = Mathf.Clamp(value, minValue, maxValue); } // 設定範圍
    }

    public void Start()
    {
        _one = 0;
        _two = 0;
        _three = 0;
        _four = 0;
        _five = 0;

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

        if (conditionMet && devicesInteraction != null)
        {
            devicesInteraction.Unlock();
        }
    }
}
