using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] Enemy enemyPrefab;
    [SerializeField] GameObject powerupPrefab;
    [SerializeField] float spawnRange = 9;

    private int enemyCount;
    private int waveNumber = 1;

    private void Start()
    {
        if (enemyPrefab == null) return;
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
        if (enemyPrefab == null) return;
        for (int i = 0; i < enemiesToSpawn; i++)
        {
            Instantiate(enemyPrefab, GenerateSpawnPosition(), enemyPrefab.transform.rotation);
        }
    }

    private void SpawnPowerup()
    {
        if (powerupPrefab == null) return;
        Instantiate(powerupPrefab, GenerateSpawnPosition(), powerupPrefab.transform.rotation);
    }
}
