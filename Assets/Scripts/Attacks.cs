//using System.Diagnostics;
using UnityEngine;


// Defines player and enemy attacks used to damage opponents.
// Checks the owner of the attack to prevent enemies from hitting other enemies.
public class Attacks : MonoBehaviour
{
    [SerializeField] private int attackDamage;
    private PlayerController playerController;
    
    private void Awake()
    {
        playerController = GetComponentInParent<PlayerController>(); // Gets the PlayerController from the parent object.
    }

    // This trigger is configured on the player's and enemy's attack Collider2D and activates during an attack.
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.GetComponent<PlayerHealth>() != null)
        {
            other.gameObject.GetComponent<PlayerHealth>().TakeDamage(attackDamage); // Causes damage to the player.
            //Debug.Log("Player took damage");
        }
        else if(other.gameObject.GetComponent<EnemyHealth>() != null)
        {
            other.gameObject.GetComponent<EnemyHealth>().TakeDamage(attackDamage); // Causes damage to the enemy.
            playerController.CountCombo(); // Calls PlayerController.CountCombo() to count the player's combo.
            playerController.currentCombo.Advance(); // Advances the current hit in the combo.
            Debug.Log("Hit advance: " + playerController.currentCombo.currentHit);
            //Debug.Log("Enemy took damage");
        }
        
    }

}
