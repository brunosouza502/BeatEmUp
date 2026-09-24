using UnityEngine;
using UnityEngine.SceneManagement; // Manages game scenes.
using System.Collections;

public class EndLevel : MonoBehaviour
{

    [SerializeField] private string nextLevelName;

    [SerializeField] private float fadeOutTime;
    [SerializeField] private float LoadLevelTime;

    private IEnumerator FadeOutScreen()
    {
        //yield return new WaitForSeconds(fadeOutTime); //Time to fade out
        //UIManager.instance.DarkenTransitionImage(); //

        yield return new WaitForSeconds(LoadLevelTime); //Time to fade out
        UIManager.instance.EnableEndLevelPanel(); //

        //yield return new WaitForSeconds(LoadLevelTime); // Time to load new level
        //SceneManager.LoadScene(nextLevelName);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.GetComponent<PlayerController>() != null)
        {
            // Finds active enemies in the level. The level ends only when the array is empty.
            EnemyController[] levelEnemies = FindObjectsOfType<EnemyController>();
            
            Debug.Log("Enemies found: " + levelEnemies.Length);

            if (levelEnemies.Length == 0)
            {
                //SceneManager.LoadScene(nextLevelName);
                StartCoroutine(FadeOutScreen());
            }
        }
    }

}
