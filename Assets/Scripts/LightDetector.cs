using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightDetector : MonoBehaviour
{
    public Light targetLight; // 需要检测的光源

    public float GetLightIntensity()
    {
        if (targetLight != null)
        {
            return targetLight.intensity;
        }
        return 0f;
    }
}
