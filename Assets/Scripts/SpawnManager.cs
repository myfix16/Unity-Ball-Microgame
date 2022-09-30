using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject enemyPrefab;

    public GameObject powerupPrefab;

    public float spawnRange = 7;

    public int enemyCount;

    private int waveNumber = 1;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        enemyCount = FindObjectsOfType<Enermy>().Length;
        if (enemyCount == 0)
        {
            SpawnEnemyWave(waveNumber++);
            SpawnPowerup();   
        }
    }

    void SpawnEnemyWave(int numEnemies)
    {
        for (int i = 0; i < numEnemies; i++)
        {
            Instantiate(enemyPrefab, GenerateSpawnPosition(), enemyPrefab.transform.rotation);
        }
    }

    void SpawnPowerup()
    {
        Instantiate(powerupPrefab, GenerateSpawnPosition(), powerupPrefab.transform.rotation);
    }

    Vector3 GenerateSpawnPosition()
    {
        float spawnPosX = Random.Range(-spawnRange, spawnRange);
        float spawnPosZ = Random.Range(-spawnRange, spawnRange);
        return new Vector3(spawnPosX, 0, spawnPosZ);
    }
}
