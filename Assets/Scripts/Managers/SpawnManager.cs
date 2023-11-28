using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] enemies;

    private int yEnemySpawn = 11;
    private int xSpawnMax = 14;
    private int xSpawnMin = 0;

    private float enemySpawnTime = 1f;
    private float startDelay = 1f;

    private GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        
        InvokeRepeating("SpawnEnemy", startDelay, enemySpawnTime);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SpawnEnemy()
    {
        if (gameManager.isGameActive)
        {
            int randomX = Random.Range(xSpawnMax, xSpawnMin);
            int randomEnemyIndex = Random.Range(0, enemies.Length);

            Vector3 spawnPos = new Vector3(randomX, yEnemySpawn, 0);

            Instantiate(enemies[randomEnemyIndex], spawnPos, enemies[randomEnemyIndex].gameObject.transform.rotation);
        }
    }
}
