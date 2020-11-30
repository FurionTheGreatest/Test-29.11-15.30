using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider healthBar;
    public Health playerHealth;
    private void Awake()
    {
        healthBar.maxValue = playerHealth.maxHealth;
        healthBar.value = healthBar.maxValue;
    }

    public void UpdateHealth()
    {
        healthBar.value = playerHealth.currentHealth;
    }
}
