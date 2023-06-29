using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public Transform[] spawnPoints;
    public float spawnInterval = 2f;

    private float spawnTimer = 0f;

    private void Update()
    {
        spawnTimer += Time.deltaTime;

        if (spawnTimer >= spawnInterval)
        {
            SpawnEnemy();
            spawnTimer = 0f;
        }
    }

    private void SpawnEnemy()
    {
        // Randomly select a spawn point index
        int spawnIndex = Random.Range(0, spawnPoints.Length);

        // Instantiate an enemy at the selected spawn point
        Instantiate(enemyPrefab, spawnPoints[spawnIndex].position, Quaternion.identity);
    }
}

