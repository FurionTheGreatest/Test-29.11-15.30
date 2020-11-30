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

    private void LoseHealth(float amountToLose)
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
    
    private void AddHealth(float amountToAdd)
    {
        if (currentHealth + amountToAdd <= maxHealth)
            currentHealth += amountToAdd;
        else
            currentHealth = maxHealth;
        
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
        AidKit.OnKitPickUp += AddHealth;
    }

    private void OnDisable()
    {
        Bullet.OnPlayerHit -= LoseHealth;
        AidKit.OnKitPickUp -= AddHealth;
    }
}
