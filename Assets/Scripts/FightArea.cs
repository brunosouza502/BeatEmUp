using UnityEngine;

public class FightArea : MonoBehaviour
{
    [Header("Checks")]
    private bool canCheckPlayer; // Checks whether the player entered the fight area so enemies can spawn.
    private bool canSpawnEnemies; // Prevents enemies from spawning indefinitely.

    [Header("Spawn Timer")]
    [SerializeField] private float maxSpawnInterval; // Maximum time between enemy spawns, configured in the Unity Inspector.
    private float currentSpawnInterval; // Time elapsed since the last enemy spawn.

    [Header("Spawn Control")]
    [SerializeField] private Transform[] spawnPoints; // Spawn locations.
    [SerializeField] private GameObject[] enemiesToSpawn; // Enemy prefabs to spawn.
    private int spawnedEnemies; // Number of enemies already spawned.
    private int currentEnemy; // Index of the current enemy in the array.
    private int currentPoint;

    private void Start()
    {
        canCheckPlayer = true; // Allows the player to be checked as soon as the game starts.
        canSpawnEnemies = false;

        spawnedEnemies = 0;
        currentEnemy = 0;

        currentPoint = 0;
    }

    private void Update()
    {
        // The timer starts when the player enters the fight area.
        // Checks whether spawning is enabled and not all configured enemies have spawned.
        if(canSpawnEnemies && spawnedEnemies < enemiesToSpawn.Length)
        {
            RunSpawnTimer();
        }
    }

    private void RunSpawnTimer()
    {
        // Controls the number of spawned enemies.
        currentSpawnInterval -= Time.deltaTime;
        if(currentSpawnInterval <= 0)
        {
            SpawnEnemy();
            currentSpawnInterval = maxSpawnInterval; // Resets the timer.
        }
    }

    private void SpawnEnemy()
    {
        // Spawns at a random location configured in the Inspector.
        Transform randomPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];

        //Transform randomPoint = spawnPoints[currentPoint];
        Debug.Log("Total spawn points: " + spawnPoints.Length);
        
        // Spawns enemies in order. currentEnemy is the index of the first element.
        GameObject newEnemy = enemiesToSpawn[currentEnemy];
        
        // Spawns the new enemy at the selected location.
        Instantiate(newEnemy, randomPoint.position, randomPoint.rotation);
        currentEnemy++; // Advances to the next enemy.
        spawnedEnemies++; // Number of enemies already spawned.
        //currentPoint++;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (canCheckPlayer)
        {
            if(other.gameObject.GetComponent<PlayerController>() != null)
            {
                canSpawnEnemies = true; // Allows enemies to spawn because the player entered the fight area.
                canCheckPlayer = false; // Prevents repeated checks and infinite enemy spawning.
                Debug.Log("Player");
            }
        }
        
    }
}
