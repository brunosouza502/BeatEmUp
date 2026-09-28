using UnityEngine;
using UnityEngine.UI; // Provides user interface components.
using TMPro; // Provides TextMeshPro components.

public class UIManager : MonoBehaviour
{

    public static UIManager instance { get; private set; }

    // Animator used by the level transition image.
    [SerializeField] private Animator transitionAnimator;

    [Header("Game Over UI")]
    [SerializeField] private GameObject gameOverPanel;

    [Header("End Level Panel")]
    [SerializeField] private GameObject endLevelPanel;
    [SerializeField] private TMP_Text maxScoreText;
    public string nextLevel { get; set; }//Gets next level name


    [Header("Player")]
    // The player health bar is always visible, so only its value needs to be updated.
    [SerializeField] private Slider playerHealthBar;

    [Header("Enemy")]
    [SerializeField] private GameObject enemyPanel;
    [SerializeField] private Slider currentEnemyHealthBar;
    [SerializeField] private TMP_Text currentEnemyNameText;

    [Header("Combo Counter")]
    [SerializeField] private TMP_Text comboCounterText;



    private void Awake()
    {
        instance = this;
        nextLevel = endLevelPanel.GetComponent<ScoreLevelPanel>().nextLevel;
    }

    void Start()
    {
        // The panel starts disabled and is enabled when the player attacks the enemy.
        DisableEnemyPanel();
        ClearTransitionImage();
    }

    public void UpdatePlayerHealthBar(int maxValue, int currentValue)
    {
        playerHealthBar.maxValue = maxValue;
        playerHealthBar.value = currentValue;
    }

    private void ClearTransitionImage()
    {
        transitionAnimator.Play("imagem-de-transicao-clareando");
    }

    public void DarkenTransitionImage()
    {
        transitionAnimator.Play("imagem-de-transicao-escurecendo");
    }


    public void EnableEnemyPanel()
    {
        enemyPanel.SetActive(true);
    }

    public void DisableEnemyPanel()
    {
        enemyPanel.SetActive(false);
    }

    public void UpdateCurrentEnemyHealthBar(int maxValue, int currentValue, string enemyName)
    {
        currentEnemyHealthBar.maxValue = maxValue;
        currentEnemyHealthBar.value = currentValue;

        currentEnemyNameText.text = enemyName;

        EnableEnemyPanel();
    }

    public void EnableGameOverPanel()
    {
        // Enable the Game Over panel when the player is defeated.
        gameOverPanel.SetActive(true);
    }

    public void EnableCombo()
    {
        comboCounterText.gameObject.SetActive(true);
    }

    public void DisableCombo()
    {
        comboCounterText.gameObject.SetActive(false);
    }

    public void UpdateComboCounter(int comboValue)
    {
        comboCounterText.text = comboValue.ToString() + "X";
    }

    public void MaxCombo(int comboMax)
    {
        maxScoreText.text = comboMax.ToString() + "X";
    }

    public void EnableEndLevelPanel()
    {
        endLevelPanel.SetActive(true);
        endLevelPanel.GetComponent<ScoreLevelPanel>().nextLevel = nextLevel;

        //ScoreLevelPanel.nextLevel = nextLevel;
    }

    //Load next level, defined in EndLevel
    /*public void EnableEndLevelPanel(string _nextLevel)
    {
        ScoreLevelPanel.nextLevel = _nextLevel;
        endLevelPanel.SetActive(true);
    }*/
}
