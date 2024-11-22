using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject[] enemyPrefabs; // Tablica różnych przeciwników (strzelający, wybuchający)
    public Transform[] spawnPoints;  // Możesz podać wiele punktów spawn
    public float spawnInterval = 5f; // Czas między spawnami
    public int maxEnemies = 10;      // Maksymalna liczba przeciwników w grze

    private float nextSpawnTime = 0f;

    void Update()
    {
        if (Time.time >= nextSpawnTime && CountEnemies() < maxEnemies)
        {
            SpawnEnemy();
            nextSpawnTime = Time.time + spawnInterval;
        }
    }

    void SpawnEnemy()
    {
        int randomEnemyIndex = Random.Range(0, enemyPrefabs.Length);
        int randomSpawnPointIndex = Random.Range(0, spawnPoints.Length);

        Instantiate(enemyPrefabs[randomEnemyIndex], 
                    spawnPoints[randomSpawnPointIndex].position, 
                    spawnPoints[randomSpawnPointIndex].rotation);
    }

    int CountEnemies()
    {
        return GameObject.FindGameObjectsWithTag("Enemy").Length;
    }
}
