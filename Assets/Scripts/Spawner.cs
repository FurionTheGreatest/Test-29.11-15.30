﻿using System;
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
    [Header("AidKit")]
    public GameObject aidKitPrefab;

    public float kitSpawnRepeatRate = 30;

    private const int EnemiesCountToWon = 10;

    private CinemachineFreeLook _camera;

    public static Action WonGame;
    private void Start()
    {
        _camera = FindObjectOfType<CinemachineFreeLook>();
        InvokeRepeating(nameof(SpawnAidKit),kitSpawnRepeatRate, kitSpawnRepeatRate);
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
            var enemy = Pooler.Spawn(enemyPrefab, enemySpawns[randomSpawnPosition].position, Quaternion.identity);
            var enemyHealth = enemy.GetComponent<EnemyHealth>();
            enemyHealth.currentHealth = enemyHealth.maxHealth;
        }

        enemiesLeft = countToSpawn;

        if (_countToSpawn % 2 == 0)
            _countToSpawn += 2;
        else _countToSpawn++;
        
        if(_countToSpawn > EnemiesCountToWon)
            WonGame?.Invoke();
    }
    
    [ContextMenu("SpawnPlayer")]
    public void SpawnPlayer()
    {
        CancelInvoke(nameof(SpawnAidKit));
        //clear UI
        FindObjectOfType<UIGameState>().HideScreens();
        //hide all objects to pools
        Pooler.DespawnAllPools();
        //get player from pool
        var instance = Pooler.Spawn(playerPrefab, playerSpawn.position, Quaternion.identity);
        //restore health to max HP
        instance.GetComponent<Health>().RestoreHealth();
        //follow 
        _camera.Follow = instance.transform;
        _camera.LookAt = instance.transform;

        _countToSpawn = 1;
        SpawnEnemy(_countToSpawn);
        InvokeRepeating(nameof(SpawnAidKit),kitSpawnRepeatRate, kitSpawnRepeatRate);
    }

    public void SpawnAidKit()
    {
        for (int i = 0; i < 2; i++)
        {
            var randomSpawnPosition = Random.Range(0, enemySpawns.Length);
            var kit = Pooler.Spawn(aidKitPrefab, enemySpawns[randomSpawnPosition].position, Quaternion.identity);
        }
        
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
