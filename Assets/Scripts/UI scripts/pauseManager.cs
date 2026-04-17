using UnityEngine;
using UnityEngine.SceneManagement;

public class pauseManager : MonoBehaviour
{
    public GameObject PauseMenu;
    public PausePlayerStatsUI statsUI;

    public static pauseManager Instance;
    public bool IsPaused { get; private set; }

    public GameObject settingsPanel;

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

    public void OpenSettings()
    {
        settingsPanel.SetActive(true);
    }

    public void CloseSettings()
    {
        settingsPanel.SetActive(false);
    }

    public void ResumeGame()
    {
        LevelUpManager levelUp = FindFirstObjectByType<LevelUpManager>();

        PauseMenu.SetActive(false);
        if (levelUp != null && levelUp.IsLevelUpActive)
        {
            Time.timeScale = 0f; 
        }
        else
        {
            Time.timeScale = 1f;
        }
        IsPaused = false;
    }

    public void ReturnToMainMenu()
    {
        Time.timeScale = 1f;
        soundManager.Instance.PlayMusic("MainMenuMusic");
        SceneManager.LoadScene(0);
    }
}