using System;
using System.Collections;
using UnityEngine;

public class AidKit : MonoBehaviour
{
    public float regenerationAmount = 50f;
    public static Action<float> OnKitPickUp;

    private const string PlayerTag = "Player";
    private WaitForSeconds _destroyTime = new WaitForSeconds(30f);

    private IEnumerator Start()
    {
        yield return _destroyTime;
        
        Pooler.Despawn(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals(PlayerTag))
        {
            OnKitPickUp?.Invoke(regenerationAmount);

            Pooler.Despawn(gameObject);
        }
    }
}
