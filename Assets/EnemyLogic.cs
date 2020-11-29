using System.Collections;
using UnityEngine;

public class EnemyLogic : MonoBehaviour
{
    [Header("Moving")]
    public float distanceToAttack = 5f;
    public float idleDistance = 3f;
    public float enemySpeed = 3f;
    [Header("Attacking")] 
    public Transform bulletSpawnPosition;
    public GameObject bulletPrefab;
    public bool _canAttack = true;
    private static float cooldownTime = 4;
    private WaitForSeconds _cooldown = new WaitForSeconds(cooldownTime);
   
    private Transform _playerTransform;

    private const string PlayerTag = "Player";
    private void OnEnable()
    {
        if (_playerTransform == null)
            _playerTransform = GameObject.FindWithTag(PlayerTag).transform;
    }

    private void Update()
    {
        if(_playerTransform == null || !_playerTransform.gameObject.activeInHierarchy) return;
        CheckDistanceToPlayer();
    }

    private void CheckDistanceToPlayer()
    {
        if (Vector3.Distance(gameObject.transform.position, _playerTransform.position) > distanceToAttack)
        {
            MoveTowardsPlayer();
        }
        else
        {
            AttackPlayer();
        }
    }

    private void MoveTowardsPlayer()
    {
        gameObject.transform.LookAt(_playerTransform);
        if (Vector3.Distance(gameObject.transform.position, _playerTransform.position) > idleDistance)
        {
            transform.position = Vector3.MoveTowards(transform.position, _playerTransform.position, enemySpeed * Time.deltaTime);
        }
    }

    private void AttackPlayer()
    {
        if(!_canAttack) return;
        gameObject.transform.LookAt(_playerTransform);
        
        Pooler.Spawn(bulletPrefab, bulletSpawnPosition.position, gameObject.transform.rotation);
        
        StartCoroutine(nameof(ShootCooldown));
    }

    private IEnumerator ShootCooldown()
    {
        _canAttack = false;
        yield return _cooldown;
        _canAttack = true;
    }
}
