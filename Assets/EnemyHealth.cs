using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public float maxHealth = 100;

    public float currentHealth;
    public static Action OnEnemyDie;
    
    private bool _isHealthChanged;
    private void Start()
    {
        currentHealth = maxHealth;
    }
    private void Update()
    {
        if(!_isHealthChanged) return;
        GetComponentInChildren<EnemyHealthBar>()?.UpdateHealth();
        _isHealthChanged = false;
    }
    public void LoseHealth(float amountToLose)
    {
        if (currentHealth - amountToLose > 0)
        {
            currentHealth -= amountToLose;
            PopupSpawner.instance.DisplayDamagePopup(amountToLose,gameObject.transform);
            _isHealthChanged = true;
        }
        else
        {
            currentHealth = 0;
            _isHealthChanged = true;
            Die();
        }
    }
    
    private void Die()
    {
        OnEnemyDie?.Invoke();
        Pooler.Despawn(gameObject);
    }
}
