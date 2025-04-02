using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] enemyPrefabs; // Array of enemy prefabs (assign in Inspector)
    private List<GameObject> activeEnemies = new List<GameObject>(); // Dynamic list of active enemies

    public Transform player; // Assign the player object
    private bool canSpawn = true; // Prevent multiple coroutines from running

    void Start()
    {
        StartCoroutine(SpawnEnemyRoutine());
    }

    void Update()
    {
        int sanity = PlayerStats.Instance.getSanity();
        
        // Add enemies to the list when sanity reaches specific values
        if (sanity <= 100 && activeEnemies.Count < 1) activeEnemies.Add(enemyPrefabs[0]);
        if (sanity <= 60 && activeEnemies.Count < 2) activeEnemies.Add(enemyPrefabs[1]);
    }

    IEnumerator SpawnEnemyRoutine()
    {
        while (true)
        {
            if (activeEnemies.Count > 0)
            {
                float spawnRate = Mathf.Lerp(2f, 5f, PlayerStats.Instance.getSanity() / 100f); // From 2s (0 sanity) to 5s (100 sanity)
                SpawnEnemy();
                yield return new WaitForSeconds(spawnRate);
            }
            else
            {
                yield return null; // Wait until enemies are added
            }
        }
    }

    void SpawnEnemy()
    {
        if (player == null) return;

        GameObject enemyToSpawn = activeEnemies[Random.Range(0, activeEnemies.Count)];
        Vector2 spawnPos = GetSpawnPosition();

        Instantiate(enemyToSpawn, spawnPos, Quaternion.identity);
        Debug.Log($"Spawned {enemyToSpawn.name} at {spawnPos}");
    }

    Vector2 GetSpawnPosition()
    {
        float angle = Random.Range(0f, 360f) * Mathf.Deg2Rad; // Random angle in radians
        float radius = 10f; // Fixed radius
        return new Vector2(player.position.x + Mathf.Cos(angle) * radius, 
                           player.position.y + Mathf.Sin(angle) * radius);
    }
}

