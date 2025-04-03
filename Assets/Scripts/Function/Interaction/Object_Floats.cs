using UnityEngine;

public class Object_Floats : MonoBehaviour
{
    public float floatSpeed = 1f; // 晃動速度
    public float floatAmplitude = 0.2f; // 晃動幅度

    private Vector3 startPos;

    private bool _APT_1 = false, _APT_2 = false, _APT_3 = false;
    private bool _Hospital_1 = false, _Hospital_2 = false, _Hospital_3 = false,_Hospital_4 = false, _Hospital_5 = false;

    void Start()
    {
        startPos = transform.position;
    }

    void Update()
    {
        float newY = startPos.y + Mathf.Sin(Time.time * floatSpeed) * floatAmplitude;
        transform.position = new Vector3(startPos.x, newY, startPos.z);
    }
}