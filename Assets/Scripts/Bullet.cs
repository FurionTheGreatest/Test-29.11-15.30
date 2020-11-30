using System;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float bulletSpeed = 5f;
    public float damage = 20f;
    public static Action<float> OnPlayerHit;
    public bool ignorePlayer = false;

    private const string PlayerTag = "Player";
    private const string EnemyTag = "Enemy";
    
    private void Update()
    {
        transform.Translate(Vector3.forward * (bulletSpeed * Time.deltaTime));
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals(PlayerTag) && !ignorePlayer)
        {
            OnPlayerHit?.Invoke(damage);

            Pooler.Despawn(gameObject);
        }

        if (other.tag.Equals(EnemyTag))
        {
            other.GetComponent<EnemyHealth>()?.LoseHealth(damage);
            
            Pooler.Despawn(gameObject);
        }        
    }
}
