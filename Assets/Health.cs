using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    public float maxHealth = 100;

    public float currentHealth;
    private bool _isHealthChanged;

    public static Action OnPlayerDeath;
    private void Start()
    {
        currentHealth = maxHealth;
    }

    private void Update()
    {
        if(!_isHealthChanged) return;
        GetComponentInChildren<HealthBar>()?.UpdateHealth();
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
    
    public void AddHealth(float amountToAdd)
    {
        currentHealth += amountToAdd <= maxHealth ? currentHealth += amountToAdd : currentHealth = maxHealth;
        _isHealthChanged = true;
    }

    public void RestoreHealth()
    {
        currentHealth = maxHealth;
        _isHealthChanged = true;
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
