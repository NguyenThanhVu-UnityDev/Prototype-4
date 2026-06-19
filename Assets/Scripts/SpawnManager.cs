using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] List<Enemy> enemyPrefabs = new();
    [SerializeField] List<PowerupItem> powerupItemPrefabs = new();
    [SerializeField] float spawnRange = 9;

    private int enemyCount;
    private int waveNumber = 1;

    private void Start()
    {
        SpawnEnemyWave(waveNumber);
        SpawnPowerup();
    }

    private void Update()
    {
        enemyCount = FindObjectsByType<Enemy>(FindObjectsSortMode.None).Length;

        if (enemyCount == 0)
        {
            waveNumber++;
            SpawnEnemyWave(waveNumber);
            SpawnPowerup();
        }
    }

    private Vector3 GenerateSpawnPosition()
    {
        float spawnPosX = Random.Range(-spawnRange, spawnRange);
        float spawnPosZ = Random.Range(-spawnRange, spawnRange);

        return new(spawnPosX, 0, spawnPosZ);
    }

    private void SpawnEnemyWave(int enemiesToSpawn)
    {
        if (enemyPrefabs == null || enemyPrefabs.Count == 0) return;

        for (int i = 0; i < enemiesToSpawn; i++)
        {
            int randomIndex = Random.Range(0, enemyPrefabs.Count);
            Instantiate(enemyPrefabs[randomIndex], GenerateSpawnPosition(), enemyPrefabs[randomIndex].transform.rotation);
        }
    }

    private void SpawnPowerup()
    {
        if (powerupItemPrefabs == null || powerupItemPrefabs.Count == 0) return;
        var randomIndex = Random.Range(0, powerupItemPrefabs.Count);
        Instantiate(powerupItemPrefabs[randomIndex], GenerateSpawnPosition(), powerupItemPrefabs[randomIndex].transform.rotation);
    }
}
