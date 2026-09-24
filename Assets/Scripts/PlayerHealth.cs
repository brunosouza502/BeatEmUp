using System.Collections;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [Header("Checks")]
    public bool playerAlive;

    [Header("Parameters")]
    [SerializeField] private float gameOverDelay; // Time before the player dies, configured in the Unity Inspector.

    [Header("Health Control")]
    [SerializeField] private int maxHealth;
    private int currentHealth;

    private void Start()
    {
        // Initializes the player as alive and sets current health to maximum health.
        playerAlive = true;
        currentHealth = maxHealth;

        UIManager.instance.UpdatePlayerHealthBar(maxHealth, currentHealth);
    }

    public bool IsPlayerAlive()
    {
        return playerAlive;
    }

    // Restores health to the player when collecting food.
    public void GainHealth(int healthAmount)
    {
        if(playerAlive)
        {
            currentHealth += healthAmount;

            if(currentHealth > maxHealth)
            {
                currentHealth = maxHealth; // Prevents current health from exceeding maximum health.
            }

            UIManager.instance.UpdatePlayerHealthBar(maxHealth, currentHealth);// Updates the player's health bar through UIManager.
        }
        /*
        if (currentHealth + healthAmount <= maxHealth)
        {
            currentHealth += healthAmount;
        }
        else
        {
            currentHealth = maxHealth; // Prevents current health from exceeding maximum health.
        }

        UIManager.instance.UpdatePlayerHealthBar(maxHealth, currentHealth);// Updates the player's health bar through UIManager.
        */
    }

    // Applies damage to the player. If current health reaches zero, the player dies.
    public void TakeDamage(int damageAmount)
    {
        if(playerAlive)
        {
           currentHealth -= damageAmount;
            // Accesses PlayerController to play the player's damage animation.
            GetComponent<PlayerController>().PlayDamageAnimation(); 

            UIManager.instance.UpdatePlayerHealthBar(maxHealth, currentHealth); // Updates the player's health bar through UIManager.

            // Player damage sound.
            SoundManager.instance.playerTakingDamage.Play();


            if (currentHealth <= 0)
           {
                playerAlive = false;
                GetComponent<PlayerController>().PlayDefeatAnimation(); // Accesses PlayerController to play the player's defeat animation.
                StartCoroutine(EnableGameOver()); // Enables the Game Over panel after the configured delay.

                Debug.Log("Game Over");
           }
        }
    }

    // Coroutines.
    private IEnumerator EnableGameOver()
    {
        yield return new WaitForSeconds(gameOverDelay); // Waits for the defeat animation before showing the Game Over panel.
        UIManager.instance.EnableGameOverPanel(); // Accesses UIManager to enable the Game Over panel.
    }

}
