using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab; // Prefab przeciwnika
    public Transform player; // Gracz, wokół którego będą generowani przeciwnicy
    public float spawnRadius = 10f; // Promień generowania przeciwników wokół gracza
    public float spawnInterval = 5f; // Czas pomiędzy generowaniem kolejnych przeciwników

    void Start()
    {
        // Uruchom generowanie przeciwników w określonym interwale czasowym
        InvokeRepeating(nameof(SpawnEnemy), spawnInterval, spawnInterval);
    }

    void SpawnEnemy()
    {
        // Generuj losową pozycję wokół gracza
        Vector3 spawnPosition = player.position + Random.insideUnitSphere * spawnRadius;
        spawnPosition.y = 1f; // Ustawienie pozycji na wysokości ziemi (Y)

        // Tworzenie przeciwnika
        Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
    }
}
