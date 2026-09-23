//using System.Diagnostics;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{

    [Header("Checks")]
    private bool enemyAlive;

    [Header("Parameters")]
    [SerializeField] private string enemyName; // Enemy name displayed on the enemy health bar.

    [Header("Health Control")]
    [SerializeField] private int maxHealth;
    private int currentHealth;
    [SerializeField] private float disappearDelay; // Recommended value is the animation duration, usually two seconds.

    [Header("Defeat Drop Chance")]
    [SerializeField] private int foodDropChance;
    [SerializeField] private GameObject[] foodDrops; // Array of food types.

    void Start()
    {
        // Initializes the enemy as alive and sets current health to maximum health.
        enemyAlive = true;
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damageAmount)
    {
        if (enemyAlive)
        {
            currentHealth -= damageAmount;
            GetComponent<EnemyController>().PlayDamageAnimation(); // Accesses EnemyController to play the enemy damage animation.
            
            // Updates the enemy health bar through UIManager.
            UIManager.instance.UpdateCurrentEnemyHealthBar(maxHealth, currentHealth, enemyName);

            // Enemy damage sound.
            SoundManager.instance.enemyTakingDamage.Play();

            if (currentHealth <= 0)
            {
                enemyAlive = false;
                GetComponent<EnemyController>().PlayDefeatAnimation(); // Accesses EnemyController to play the enemy defeat animation.
                //Debug.Log("Enemy defeated");

                UIManager.instance.DisableEnemyPanel(); // Accesses UIManager to disable the enemy health panel.

                SpawnFood(); // Spawns food if the enemy is defeated.
                Destroy(this.gameObject, disappearDelay); // Removes the enemy after the configured delay.
            }
        }
    }

    public bool IsEnemyAlive()
    {
        return enemyAlive;
    }

    private void SpawnFood()
    {
        int randomNumber = Random.Range(0, 100); // Generates a random number between 0 and 100.
        //Debug.Log("Random number: " + randomNumber);

        // Checks whether the random number is within the food drop chance.
        if(randomNumber <= foodDropChance)
        {
            GameObject selectedFood = foodDrops[Random.Range(0, foodDrops.Length)]; // Randomly selects a food type.
            Debug.Log("Selected food: " + selectedFood.name);
            Instantiate(selectedFood, transform.position, transform.rotation); // Instantiates the selected food at the enemy's position.
        }
    }

}
