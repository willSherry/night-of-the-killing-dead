using UnityEngine;

public class Pause : MonoBehaviour
{
    public static bool isPaused = false;
    [SerializeField] GameObject MusicPlayer;
    private AudioSource musicSource;
    void Start()
    {
         musicSource = MusicPlayer.GetComponent<AudioSource>();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }
    public void PauseGame()
    {
        isPaused = true;
        Time.timeScale = 0f;
        musicSource.volume = 0.2f;
    }

    public void ResumeGame()
    {
        isPaused = false;
        Time.timeScale = 1f;
        musicSource.volume = 1.0f;
    }
}
