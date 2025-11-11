using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    GameObject enemyPrefab;
    [SerializeField]
    float maxSpawnCooldown = 0.3f;
    [SerializeField]
    float minSpawnCooldown = 0.1f;
    float timeSinceLastSpawn = 0;
    float timeToNextSpawn = 0.2f;


    void Update()
    {
        timeSinceLastSpawn += Time.deltaTime;
        if (timeToNextSpawn <= timeSinceLastSpawn)
        {
            Instantiate(enemyPrefab);
            timeToNextSpawn = Random.Range(minSpawnCooldown, maxSpawnCooldown);
            timeSinceLastSpawn = 0;
        }
    }
}
