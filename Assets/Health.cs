using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    public float maxHealth = 100;

    public float currentHealth;

    public static Action OnPlayerDeath;
    private void Start()
    {
        currentHealth = maxHealth;
    }

    public void LoseHealth(float amountToLose)
    {
        if (currentHealth - amountToLose > 0)
        {
            currentHealth -= amountToLose;
        }
        else
        {
            currentHealth = 0;
            Die();
        }
    }
    
    public void AddHealth(float amountToAdd)
    {
        currentHealth += amountToAdd <= maxHealth ? currentHealth += amountToAdd : currentHealth = maxHealth;
    }

    public void RestoreHealth()
    {
        currentHealth = maxHealth;
    }
    
    private void Die()
    {
        OnPlayerDeath?.Invoke();
        Pooler.Despawn(gameObject);
    }
    
    private void OnEnable()
    {
        Bullet.OnPlayerHit += LoseHealth;
    }

    private void OnDisable()
    {
        Bullet.OnPlayerHit -= LoseHealth;
    }
}
