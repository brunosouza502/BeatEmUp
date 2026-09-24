using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    [Header("Sound Effects")]
    [SerializeField] public AudioSource kickImpact;
    [SerializeField] public AudioSource punchImpact;
    [SerializeField] public AudioSource foodPickup;
    [SerializeField] public AudioSource enemyTakingDamage;
    [SerializeField] public AudioSource playerTakingDamage;

    [Header("Music")]
    [SerializeField] private AudioSource gameOverMusic;
    [SerializeField] private AudioSource backgroundMusic;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        PlayBackgroundMusic();
    }

    public void PlayBackgroundMusic()
    {
        // Game Over music must stop while background music is playing.
        gameOverMusic.Stop();
        backgroundMusic.Play();
    }

    public void PlayGameOverMusic()
    {
        // Background music must stop while Game Over music is playing.
        backgroundMusic.Stop();
        gameOverMusic.Play();
    }

}
