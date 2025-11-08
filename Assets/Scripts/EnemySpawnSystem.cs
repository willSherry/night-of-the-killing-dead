using UnityEngine;

public class EnemySpawnSystem : MonoBehaviour
{
    public GameObject enemyPrefab;
    public float spawnInterval = 3f;
    private float spawnCounter;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spawnCounter = spawnInterval;
    }

    // Update is called once per frame
    void Update()
    {
        spawnCounter -= Time.deltaTime;
        if (spawnCounter <= 0f)
        {
            SpawnEnemy();
            spawnCounter = spawnInterval;
        }
    }

    void SpawnEnemy()
    {
        Vector2 spawnPosition = new Vector2(Random.Range(-40f, 40f), Random.Range(-20f, 20f));
        Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
    }
}
