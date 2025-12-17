using UnityEngine;
using UnityEngine.UI; 
using System.Collections.Generic;
public class LevelUpManager : MonoBehaviour
{
    public GameObject LevelUpPanel;   
    public GameObject StatsPanel;     
    public GameObject CardsContainer;    

    public UpgradeManager upgradeManager;

    public PlayerStats playerStats;
    
    public int availableStatPoints = 0;

    public Button damageButton;
    public Button moveSpeedButton;
    public Button attackSpeedButton;
    public Button defenseButton;
    public Button critChanceButton;
    public Button healthButton;

    private void Start()
    {
        if (playerStats == null)
            playerStats = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>();

        LevelUpPanel.SetActive(false);
    }

      public void OpenLevelUpPanel()
    {
        LevelUpPanel.SetActive(true);
        Time.timeScale = 0f;

        availableStatPoints += 1; 
        UpdateStatButtons();

        if (upgradeManager != null)
        {
            upgradeManager.Initialize(this);
            upgradeManager.ShowUpgradeChoices(3);
        }
    }

    public void CloseLevelUpPanel()
    {
        LevelUpPanel.SetActive(false);
        Time.timeScale = 1f;
    }

    #region Stat Upgrade Methods
    public void UpgradeDamage()
    {
        if (availableStatPoints <= 0) return;
        playerStats.damage += 1f;
        availableStatPoints--;
        UpdateStatButtons();
    }

    public void UpgradeMoveSpeed()
    {
        if (availableStatPoints <= 0) return;
        playerStats.moveSpeed += 0.2f;
        availableStatPoints--;
        UpdateStatButtons();
    }

    public void UpgradeAttackSpeed()
    {
        if (availableStatPoints <= 0) return;
        playerStats.attackSpeed += 0.1f;
        availableStatPoints--;
        UpdateStatButtons();
    }

    public void UpgradeDefense()
    {
        if (availableStatPoints <= 0) return;
        playerStats.defense += 1f;
        availableStatPoints--;
        UpdateStatButtons();
    }

    public void UpgradeCritChance()
    {
        if (availableStatPoints <= 0) return;
        playerStats.critChance += 0.02f;
        availableStatPoints--;
        UpdateStatButtons();
    }

    public void UpgradeMaxHealth()
    {
        if (availableStatPoints <= 0) return;
        playerStats.maxHealth += 2f;
        playerStats.currentHealth += 2f;

        playerHealth hpUI = playerStats.GetComponent<playerHealth>();
        if (hpUI != null)
        {
            hpUI.HealthBar.setMaxHealth((int)playerStats.maxHealth);
            hpUI.HealthBar.SetHealth((int)playerStats.currentHealth);
        }

        availableStatPoints--;
        UpdateStatButtons();
    }
    #endregion

    private void UpdateStatButtons()
    {
        bool hasPoints = availableStatPoints > 0;

        damageButton.interactable = hasPoints;
        moveSpeedButton.interactable = hasPoints;
        attackSpeedButton.interactable = hasPoints;
        defenseButton.interactable = hasPoints;
        critChanceButton.interactable = hasPoints;
        healthButton.interactable = hasPoints;
    }
}