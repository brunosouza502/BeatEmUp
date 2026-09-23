using UnityEngine;
using UnityEngine.SceneManagement; // Manages game scenes.

public class EndLevel : MonoBehaviour
{

    [SerializeField] private string nextLevelName;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.GetComponent<PlayerController>() != null)
        {
            // Finds active enemies in the level. The level ends only when the array is empty.
            EnemyController[] levelEnemies = FindObjectsOfType<EnemyController>();
            
            Debug.Log("Enemies found: " + levelEnemies.Length);

            if (levelEnemies.Length == 0)
            {
                SceneManager.LoadScene(nextLevelName);
            }
        }
    }
}
