using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public float maxHealth = 100;

    public float currentHealth;
    public static Action OnEnemyDie;
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
    
    private void Die()
    {
        OnEnemyDie?.Invoke();
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
