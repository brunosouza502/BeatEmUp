using UnityEngine;
using UnityEngine.SceneManagement;

public class ScoreLevelPanel : MonoBehaviour
{

    public string nextLevel { get; set; }

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
        if (Input.GetKeyDown(KeyCode.R))
        {
            // Restarts the game.
            //RestartGame();
            Debug.Log("Next level: " + nextLevel);
        }
        else if (Input.GetKeyDown(KeyCode.Escape))
        {
            // Returns to the main menu.
            //ReturnToMenu();
            NextLevel();
            Debug.Log("Next level: " + nextLevel);
        }
    }

    public void NextLevel()
    {
        SceneManager.LoadScene(nextLevel);
    }

}
