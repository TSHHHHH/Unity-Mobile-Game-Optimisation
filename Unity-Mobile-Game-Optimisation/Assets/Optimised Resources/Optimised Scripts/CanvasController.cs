using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CanvasController : MonoBehaviour
{
    public AudioClip openSound;

    [SerializeField]
    private AudioSource audioManager;

    public GameObject menu;
    public GameObject pauseMenu;
    public GameObject settingsMenu;
    public GameObject FPSPanel;
    public GameObject gameOverMenu;

    public bool FPSCounterToggle;
    public float musicVolume;

    public AudioMixer audioMixer;
    public TextMeshProUGUI volumeText;

    public Slider volumeSilder;

    private void Start()
    {
        Time.timeScale = 1f;

        menu.SetActive(false);

        volumeText.text = (volumeSilder.value * 100f).ToString("F0") + "%";
    }

    public void Update()
    {
        if (TimePanel.timeLeft <= 0)
        {
            gameOver();
        }
        else if (PlayerHealth.health < 0)
        {
            FrameManager.gameEnd = true;

            Invoke("gameOver", 2f);
        }
    }

    public void gameOver()
    {
        Time.timeScale = 0f;

        gameOverMenu.SetActive(true);
    }

    public void Pause()
    {
        if (FrameManager.initialised)
        {
            menu.SetActive(true);

            Time.timeScale = 0;

            audioManager.PlayOneShot(openSound);
        }
    }

    public void Resume()
    {
        menu.SetActive(false);

        Time.timeScale = 1;
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void RestartGame()
    {
        PlayerHealth.health = 3;

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}