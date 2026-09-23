using UnityEngine;
using UnityEngine.SceneManagement; // Manages game scenes.


public class GameOverPanel : MonoBehaviour
{

    [Header("Parameters")]
    [SerializeField] private string menuName; // Main menu scene name, configured in the Unity Inspector.

    private void Start()
    {
        SoundManager.instance.PlayGameOverMusic();
    }

    private void Update()
    {
        ReadInputs();
    }

    private void ReadInputs()
    {
        if(Input.GetKeyDown(KeyCode.R))
        {
            // Restarts the game.
            RestartGame();
        }
        else if(Input.GetKeyDown(KeyCode.Escape))
        {
            // Returns to the main menu.
            ReturnToMenu();
        }
    }

    private void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Reloads the current scene and restarts the game.
    }

    private void ReturnToMenu()
    {
        SceneManager.LoadScene(menuName); // Loads the main menu scene.
    } 

}
