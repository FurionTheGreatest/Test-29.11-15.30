using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    [Header("Attacking")] 
    public Transform bulletSpawnPosition;
    public GameObject bulletPrefab;
    public bool _canAttack = true;
    private WaitForSeconds _cooldown = new WaitForSeconds(.2f);

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SpawnBullet();
        }
    }

    public void SpawnBullet()
    {
        if(!_canAttack) return;
        
        var bulletInstance = Pooler.Spawn(bulletPrefab, bulletSpawnPosition.position, gameObject.transform.rotation);
        bulletInstance.GetComponent<Bullet>().ignorePlayer = true;
        
        StartCoroutine(nameof(ShootCooldown));
    }

    private IEnumerator ShootCooldown()
    {
        _canAttack = false;
        yield return _cooldown;
        _canAttack = true;
    }
}
