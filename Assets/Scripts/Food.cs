using UnityEngine;

public class Food : MonoBehaviour
{

    [SerializeField] private int healthAmount; // Amount of health the player gains when collecting the food.

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.GetComponent<PlayerHealth>() != null)
        {
            other.gameObject.GetComponent<PlayerHealth>().GainHealth(healthAmount); // Restores the configured amount of health to the player.

            // Food pickup sound.
            SoundManager.instance.foodPickup.Play();

            Destroy(this.gameObject); // Destroys the food and removes it from the scene.
        }
    }
}
