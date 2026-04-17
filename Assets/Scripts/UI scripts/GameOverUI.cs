using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverUI : MonoBehaviour
{
    public static GameOverUI Instance;

    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private GameObject gameWonPanel;

    private bool gameEnded = false;

    void Awake()
    {
        Instance = this;
        gameOverPanel.SetActive(false);
        gameWonPanel.SetActive(false);

    }

    public void TriggerGameOver()
    {
        if (gameEnded) return;
        gameEnded = true;

        Time.timeScale = 0f;
        gameOverPanel.SetActive(true);

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

        public void TriggerGameWon()
    {
        Debug.Log("TriggerGameWon called"); 
        if (gameEnded) return;
        gameEnded = true;

        Time.timeScale = 0f;

        gameWonPanel.SetActive(true);

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void ReturnToStart()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }
}
