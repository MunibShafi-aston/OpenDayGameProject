using UnityEngine;
using UnityEngine.SceneManagement;

public class pauseManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public GameObject PauseMenu;
    public PausePlayerStatsUI statsUI;

    public static pauseManager Instance;
    public bool IsPaused { get; private set; }

    void Awake()
    {
        Instance=this;
        ResumeGame();
    }
    public void OpenPauseMenu()
    {    
        PauseMenu.SetActive(true);
        Time.timeScale = 0f;
        IsPaused = true;

        if (statsUI != null)
        {
            statsUI.Refresh();
        }

        PlayerStats player = FindFirstObjectByType<PlayerStats>();
        PauseAbilityUI ui = FindFirstObjectByType<PauseAbilityUI>();

        if (player != null && ui != null)
            ui.LoadFromPlayer(player);
    }

    public void ResumeGame()
    {
        PauseMenu.SetActive(false);
        Time.timeScale = 1f;
        IsPaused = false;
    }

    public void ReturnToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }
}