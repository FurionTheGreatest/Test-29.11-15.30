using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthBar : MonoBehaviour
{
    public Slider healthBar;
    public EnemyHealth enemyHealth;
    private void Awake()
    {
        healthBar.maxValue = enemyHealth.maxHealth;
        healthBar.value = healthBar.maxValue;
    }

    public void UpdateHealth()
    {
        healthBar.value = enemyHealth.currentHealth;
    }
}
