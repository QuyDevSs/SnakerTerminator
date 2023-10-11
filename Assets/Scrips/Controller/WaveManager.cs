using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LTAUnityBase.Base.DesignPattern;

[System.Serializable]
public class EnemyType
{
    public EnemyTypes enemyType;
    public float firstSpawnTime = 0f;
    public float spawnInterval = 0f;
    public int maxEnemies = 20;
    public int level;
    [HideInInspector]
    public int spawnedEnemy = 0;
    [HideInInspector]
    public float lastSpawnedTime = 0;
    [HideInInspector]
    public bool hasSpawned = false;
}
[System.Serializable]
public class Wave
{
    public EnemyType[] enemyTypes;
    public int CountEnemyNumber(EnemyTypes _enemyType)
    {
        int count = 0;
        foreach (EnemyType element in enemyTypes)
        {
            if (element.enemyType == _enemyType)
            {
                count += element.maxEnemies;
            }
        }
        return count;
    }
}

public class WaveManager : MonoBehaviour
{
    Transform player;
    public float minDistance;
    public float maxDistance;
    private bool isPause;
    public bool completeHandle;
    public Wave[] waves;
    public int currentWave, tempCurrentWay;
    private float lastCompetedWaveTime;
    private List<EnemyController> enemies = new List<EnemyController>();
    public int score;
    public static float totalDamageFire, totalDamagePlants, totalDamageLight, totalDamageDark, totalDamageNormal, totalDamageCircle;
    void Start()
    {
        
    }
    private void OnEnable()
    {
        player = CreatePlayer.Instance.transform;
        CreateGameController.Instance.waveManager = this;
        CreateGameController.Instance.gameState = GameStates.Playing;
        tempCurrentWay = 0;
        currentWave = 0;
        completeHandle = false;
        CreateGameController.Instance.DisplayWave(currentWave + 1);

        totalDamageFire = 0;
        totalDamagePlants = 0;
        totalDamageLight = 0;
        totalDamageDark = 0;
        totalDamageNormal = 0;
        totalDamageCircle = 0;

        Observer.Instance.AddObserver(TOPICNAME.PAUSE, PauseHandle);
        Observer.Instance.AddObserver(TOPICNAME.ENEMY_DIE, OnEnemyDie);
    }
    private void OnDisable()
    {
        Observer.Instance.RemoveObserver(TOPICNAME.PAUSE, PauseHandle);
        Observer.Instance.RemoveObserver(TOPICNAME.ENEMY_DIE, OnEnemyDie);
    }
    void SpawnEnemy(EnemyType enemyType)
    {
        if (isPause)
        {
            enemyType.lastSpawnedTime = Time.time;
        }
        if (!enemyType.hasSpawned)
        {
            enemyType.hasSpawned = true;
            enemyType.lastSpawnedTime = Time.time;
            enemyType.spawnedEnemy = 0;
        }
        else
        {
            if (enemyType.spawnedEnemy < enemyType.maxEnemies)
            {
                if (((enemyType.spawnedEnemy == 0 && Time.time - enemyType.lastSpawnedTime > enemyType.firstSpawnTime) ||
                         (enemyType.spawnedEnemy != 0 && Time.time - enemyType.lastSpawnedTime > enemyType.spawnInterval)) && enemyType.spawnedEnemy < enemyType.maxEnemies)
                {
                    Vector3 randomOffset = Random.insideUnitCircle.normalized * Random.Range(minDistance, maxDistance);
                    Vector3 enemyPosition = player.position + randomOffset;
                    EnemyController enemy = Create.Instance.CreateEnemy(enemyPosition);
                    enemies.Add(enemy);
                    enemy.levelController.Level = enemyType.level;

                    enemyType.lastSpawnedTime = Time.time;
                    enemyType.spawnedEnemy++;
                }
            }
            else
            {
                if (enemyType.spawnedEnemy == enemyType.maxEnemies)
                {
                    CheckEndWave();
                    enemyType.spawnedEnemy++;
                }
            }
        }
    }
    void CheckEndWave()
    {
        bool waveComplete = true;
        for (int i = 0; i < waves[currentWave].enemyTypes.Length; i++)
        {
            if (waves[currentWave].enemyTypes[i].spawnedEnemy < waves[currentWave].enemyTypes[i].maxEnemies)
            {
                waveComplete = false;
                break;
            }
        }
        if (waveComplete)
        {
            if (tempCurrentWay == currentWave)
            {
                tempCurrentWay++;
            }
            lastCompetedWaveTime = Time.time;
        }
    }
    void Update()
    {
        if (isPause)
        {
            lastCompetedWaveTime += Time.deltaTime;
        }
        if (tempCurrentWay >= waves.Length && enemies.Count == 0 && !completeHandle)
        {
            completeHandle = true;
            CreateGameController.Instance.YouWin();
        }
        if (currentWave != tempCurrentWay && enemies.Count <= 2)
        {
            currentWave = tempCurrentWay;
            if (tempCurrentWay < waves.Length)
            {
                CreateGameController.Instance.DisplayWave(currentWave + 1);
            }
        }
        if (currentWave < waves.Length)
        {
            for (int i = 0; i < waves[currentWave].enemyTypes.Length; i++)
            {
                SpawnEnemy(waves[currentWave].enemyTypes[i]);
            }
        }
    }
    void PauseHandle(object data)
    {
        isPause = CreateGameController.Instance.IsPause();
    }
    void OnEnemyDie(object data)
    {
        EnemyController enemy = (EnemyController)data;
        enemies.Remove(enemy);
    }
}
