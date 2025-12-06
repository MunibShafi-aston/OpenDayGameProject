using UnityEngine;
using TMPro; 

public class LevelUI : MonoBehaviour
{
    public PlayerStats playerStats;
    public TMP_Text levelText; 

    private void Start()
    {
        if (playerStats == null)
            playerStats = GameObject.FindGameObjectWithTag("Player")?.GetComponent<PlayerStats>();

        UpdateLevelUI();
    }


    public void UpdateLevelUI()
    {
        if (playerStats != null && levelText != null)
            levelText.text = "Level " + playerStats.level;
    }
}