using UnityEngine;

public class LockPassword : MonoBehaviour
{
    [Header("解鎖物件")]
    public GameObject objectWithScript;

    [Header("密碼")]
    public int one;
    public int two;
    public int three;
    public int four;
    public int five;

    private int _one;
    private int _two;
    private int _three;
    private int _four;
    private int _five;

    private DevicesInteraction devicesInteraction;

    public int minValue = 0;
    public int maxValue = 9;

    public int One
    {
        get { return _one; }
        set { _one = Mathf.Clamp(value, minValue, maxValue); }
    }

    public int Two
    {
        get { return _two; }
        set { _two = Mathf.Clamp(value, minValue, maxValue); }
    }

    public int Three
    {
        get { return _three; }
        set { _three = Mathf.Clamp(value, minValue, maxValue); }
    }

    public int Four
    {
        get { return _four; }
        set { _four = Mathf.Clamp(value, minValue, maxValue); }
    }

    public int Five
    {
        get { return _five; }
        set { _five = Mathf.Clamp(value, minValue, maxValue); }
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
        conditionMet = (_one == one && _two == two && three == _three && four == _four && _five == five);

        if (conditionMet && devicesInteraction != null)
        {
            devicesInteraction.Unlock();
        }
    }
}
