using UnityEngine;

public class LightDetector : MonoBehaviour
{
    public Light targetLight; // 需要檢測的光源

    public float GetLightIntensity()
    {
        if (targetLight != null)
        {
            return targetLight.intensity;
        }
        return 0f;
    }
}
