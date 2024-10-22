using UnityEngine;

public class Health : MonoBehaviour
{
    public int maxHealth = 100; // 最大血量
    public int currentHealth;   // 当前血量

    public HealthBar healthBar; // 引用HealthBar类

    private float timeSinceLastDamage = 0f; // 计时器
    public float damageInterval = 5f; // 每5秒扣除一次血
    public float lightThreshold = 1f; // 亮度阈值

    public Renderer lampRenderer; // 灯光物件的渲染器
    public Color emissionColor;  // 自发光颜色
    public Color fullHealthColor = Color.green;  // 100% 血量的颜色
    public Color zeroHealthColor = Color.black; // 0% 血量的颜色

    void Start()
    {
        currentHealth = maxHealth; // 初始化血量
        healthBar.SetMaxHealth(maxHealth); // 初始化血条
        
        if (lampRenderer == null)
        {
            Debug.LogError("LampRenderer reference is not set in Lamp script");
        }

        UpdateEmissionColor(); // 初始化时更新颜色
    }

    void Update()
    {
        // 实时检查亮度
        float surroundingLight = GetSurroundingLight();

        if (surroundingLight < lightThreshold)
        {
            // 亮度不足时，计时器增加时间
            timeSinceLastDamage += Time.deltaTime;

            // 如果计时器达到5秒，则减少5点血量
            if (timeSinceLastDamage >= damageInterval)
            {
                TakeDamage(5);
                timeSinceLastDamage = 0f; // 重置计时器
            }
        }
        else
        {
            // 如果亮度足够，重置计时器
            timeSinceLastDamage = 0f;
        }
    }

    float GetSurroundingLight()
    {
        // 创建一个球形探测器，检测角色周围一定范围内的所有光源
        float radius = 10f; // 可以调整这个值
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, radius);
        float totalLightIntensity = 0f;

        foreach (Collider hitCollider in hitColliders)
        {
            // 获取碰撞物体上的Light组件
            Light light = hitCollider.GetComponent<Light>();
            if (light != null)
            {
                totalLightIntensity += light.intensity;
            }
        }

        return totalLightIntensity;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage; // 减少血量
        if (currentHealth < 0)
        {
            currentHealth = 0; // 确保血量不会低于0
        }
        healthBar.SetHealth(currentHealth); // 更新血条
        UpdateEmissionColor(); // 更新颜色
    }

    public void Heal(int amount)
    {
        currentHealth += amount; // 增加血量
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth; // 确保血量不会超过最大值
        }
        healthBar.SetHealth(currentHealth); // 更新血条
        UpdateEmissionColor(); // 更新颜色
    }

    private void UpdateEmissionColor()
    {
        if (lampRenderer != null)
        {
            Material mat = lampRenderer.material;

            Color targetColor = fullHealthColor;
            float healthPercentage = (float)currentHealth / maxHealth;

             if (healthPercentage == 1f)
            {
                SetEmissionIntensity(1f);
                targetColor = fullHealthColor;
            }
            else if (healthPercentage >= 0.75f)
            {
                SetEmissionIntensity(0.75f);
            }
            else if (healthPercentage >= 0.25f)
            {
                SetEmissionIntensity(0.5f);
            }
            else if (healthPercentage > 0f)
            {
                SetEmissionIntensity(0.25f);
            }
            else
            {
                SetEmissionIntensity(0f);
                targetColor = zeroHealthColor;
            }
            
            mat.SetColor("_EmissionColor", targetColor * Mathf.LinearToGammaSpace(healthPercentage));
            DynamicGI.SetEmissive(lampRenderer, targetColor);
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
