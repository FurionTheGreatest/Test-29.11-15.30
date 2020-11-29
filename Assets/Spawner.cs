using System;
using Cinemachine;
using UnityEngine;
using Random = UnityEngine.Random;

public class Spawner : MonoBehaviour
{
    [Header("Player")]
    public Transform playerSpawn;
    public GameObject playerPrefab;
    [Header("Enemy")]
    public Transform[] enemySpawns;

    public GameObject enemyPrefab;

    public int enemiesLeft;

    [SerializeField] private int _countToSpawn = 1;

    private CinemachineFreeLook _camera;
    private void Start()
    {
        _camera = FindObjectOfType<CinemachineFreeLook>();
    }

    private void Update()
    {
        if(enemiesLeft != 0) return;
        SpawnEnemy(_countToSpawn);
    }

    private void SpawnEnemy(int countToSpawn)
    {
        for (int i = 0; i < countToSpawn; i++)
        {
            var randomSpawnPosition = Random.Range(0, enemySpawns.Length);
            Pooler.Spawn(enemyPrefab, enemySpawns[randomSpawnPosition].position, Quaternion.identity);
        }

        enemiesLeft = countToSpawn;

        if (_countToSpawn % 2 == 0)
            _countToSpawn += 2;
        else _countToSpawn++;
    }
    
    [ContextMenu("SpawnPlayer")]
    public void SpawnPlayer()
    {
        Pooler.DespawnAllPools();
        //get player from pool
        var instance = Pooler.Spawn(playerPrefab, playerSpawn.position, Quaternion.identity);
        //restore health to max HP
        instance.GetComponent<Health>().RestoreHealth();
        //follow 
        _camera.Follow = instance.transform;
        _camera.LookAt = instance.transform;
    }

    private void ReduceEnemiesCount()
    {
        enemiesLeft--;
    }

    private void OnEnable()
    {
        EnemyHealth.OnEnemyDie += ReduceEnemiesCount;
    }
    
    private void OnDisable()
    {
        EnemyHealth.OnEnemyDie -= ReduceEnemiesCount;
    }
}
