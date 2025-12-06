using UnityEngine;

public class LevelUpManager : MonoBehaviour
{
    public GameObject levelUpUI;
    public PlayerStats playerStats;

    private void Start()
    {
        if (playerStats == null)
            playerStats = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>();

        levelUpUI.SetActive(false);
    }

    public void OpenLevelUpUI()
    {
        levelUpUI.SetActive(true);
        Time.timeScale = 0f; 
    }

    public void CloseLevelUpUI()
    {
        levelUpUI.SetActive(false);
        Time.timeScale = 1f;
    }

    public void UpgradeDamage()
    {
        playerStats.damage += 1f; 
        CloseLevelUpUI();
    }

    public void UpgradeMoveSpeed()
    {
        playerStats.moveSpeed += 0.2f;
        CloseLevelUpUI();
    }

    public void UpgradeAttackSpeed()
    {
        playerStats.attackSpeed += 0.1f;
        CloseLevelUpUI();
    }

    public void UpgradeDefense()
    {
        playerStats.defense += 1f;
        CloseLevelUpUI();
    }

    public void UpgradeCritChance()
    {
        playerStats.critChance += 0.02f;
        CloseLevelUpUI();
    }

    public void UpgradeMaxHealth()
    {
        playerStats.maxHealth += 2f;
        playerStats.currentHealth += 2f; 
        playerStats.GetComponent<playerHealth>().HealthBar.setMaxHealth((int)playerStats.maxHealth);
        playerStats.GetComponent<playerHealth>().HealthBar.SetHealth((int)playerStats.currentHealth);

        CloseLevelUpUI();
    }
}
