using UnityEngine;

public class ScoreLevelPanel : MonoBehaviour
{

    private string nextLevel;

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
        }
        else if (Input.GetKeyDown(KeyCode.Escape))
        {
            // Returns to the main menu.
            //ReturnToMenu();
        }
    }

}
