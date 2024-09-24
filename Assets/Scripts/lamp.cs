using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lamp : MonoBehaviour
{
    public int maxEnergy = 200; // 最大能量
    public int currentEnergy;   // 当前能量

    private float timeSinceLastEnergyLoss = 0f; // 计时器
    public float energyLossInterval = 1f; // 每秒减一次能量

    public Light lampLight;     // 灯光组件
    public Renderer lampRenderer; // 灯光物件的渲染器
    public Color emissionColor;  // 自发光颜色

    void Start()
    {
        if (lampLight == null)
        {
            Debug.LogError("LampLight reference is not set in Lamp script");
        }
        else
        {
            currentEnergy = maxEnergy; // 初始化能量
        }

        if (lampRenderer == null)
        {
            Debug.LogError("LampRenderer reference is not set in Lamp script");
        }
    }

    void Update()
    {
        // 计时器增加时间
        timeSinceLastEnergyLoss += Time.deltaTime;

        // 如果计时器达到1秒，则减少1点能量
        if (timeSinceLastEnergyLoss >= energyLossInterval)
        {
            ReduceEnergy(1);
            timeSinceLastEnergyLoss = 0f; // 重置计时器
        }
    }

    public void ReduceEnergy(int amount)
    {
        currentEnergy -= amount; // 减少能量
        if (currentEnergy < 0)
        {
            currentEnergy = 0; // 确保能量不会低于0
        }
        UpdateLampState(); // 更新灯的状态
    }

    public void RechargeEnergy(int amount)
    {
        currentEnergy += amount; // 增加能量
        if (currentEnergy > maxEnergy)
        {
            currentEnergy = maxEnergy; // 确保能量不会超过最大值
        }
        UpdateLampState(); // 更新灯的状态
    }

    private void UpdateLampState()
    {
        AdjustBrightness(); // 调整亮度

        if (currentEnergy > 0)
        {
            lampLight.enabled = true; // 打开灯
        }
        else
        {
            lampLight.enabled = false; // 关闭灯
        }
    }

    private void AdjustBrightness()
    {
        if (currentEnergy >= 30)
        {
            lampLight.intensity = 1f; // 正常亮度
            SetEmissionIntensity(1f);
        }
        else if (currentEnergy < 1)
        {
            lampLight.intensity = 0f; // 亮度降低
            SetEmissionIntensity(0f);
        }
        else
        {
            lampLight.intensity = 0.6f; // 亮度降低
            SetEmissionIntensity(0.6f);
        }
    }

    private void SetEmissionIntensity(float intensity)
    {
        if (lampRenderer != null)
        {
            Material mat = lampRenderer.material;
            mat.SetColor("_EmissionColor", emissionColor * intensity);
            DynamicGI.SetEmissive(lampRenderer, emissionColor * intensity);
        }
    }
}
