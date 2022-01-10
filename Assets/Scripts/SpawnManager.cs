using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject[] spawnPoints =default(GameObject[]);
    [SerializeField] private GameObject currentPoint = default(GameObject);
    [SerializeField] private int index = 0;

    [SerializeField] private GameObject[] enemies = default(GameObject[]);
    [SerializeField] private float minSpawnTime = default(float);
    [SerializeField] private float maxSpawnTime = default(float);


    [SerializeField] private bool isSpawning = false;
    [SerializeField] private float spawnTime = default(float);
    public int enemiesSpawned = default(int);
    [SerializeField] private bool spawnComplete = false;
    [SerializeField] private GameObject spawnDoneGameObj = default(GameObject);

 
    public interface IEnemyAttack
    {
        void RegisterActive(IEnemy ai);
        void UnregisterActive(IEnemy ai);
        IEnumerable<IEnemyAttack> GetEnemies();
        //void GetEnemies<IEnemy>();

    }

    public interface IEnemy
    {
        State CurrentState { get; }
        void Think();
        void ChangeState(State newState);
    }

    public enum State
    {
        PATROL,
        SEARCH_EGG,
        ATTACK,
        RETURN
    }

    private void Start()
    {
        Invoke("SpawnEnemy", .5f);
    }

    private void Update()
    {
        if(isSpawning)
        {
            spawnTime -= Time.deltaTime;
            if(spawnTime <= 0)
            {
                spawnComplete = true;
                isSpawning = false;
            }
        }
    }

    private void SpawnEnemy()
    {
        // Choose a random enemy to spawn per enemy array
        index = Random.Range(0, spawnPoints.Length);
        currentPoint = spawnPoints[index];
        
        
        float timeBtwSpawn = Random.Range(minSpawnTime, maxSpawnTime);

        if(isSpawning)
        {
            Instantiate(enemies[Random.Range(0, enemies.Length)], currentPoint.transform.position, Quaternion.identity);
            enemiesSpawned++;
        }

        Invoke("SpawnEnemy", timeBtwSpawn);
        if(spawnComplete)
        {
            // Done spawning
            spawnDoneGameObj.SetActive(true);
        }
    }

    public bool EnemyDestroyed(GameObject gameObject)
    {
        if (gameObject == null)
            return false;
        else
        {
            Destroy(gameObject);
            enemiesSpawned--;
            return true;
        }
        
    }
}
