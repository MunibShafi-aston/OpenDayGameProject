using UnityEngine;
using System.Collections.Generic;

public class UpgradeManager : MonoBehaviour
{
    public GameObject UpgradeCardPrefab;
    public Transform CardsContainer;
    public List<upgradeBase> allUpgrades = new List<upgradeBase>();
    public List<upgradeBase> repeatUpgrades = new List<upgradeBase>();

    private LevelUpManager levelUpManager;
    private abilityHolder playerAbilityHolder;

    public void Initialize(LevelUpManager manager)
    {
        levelUpManager = manager;

        playerAbilityHolder = FindFirstObjectByType<abilityHolder>();
    }

    public void ShowUpgradeChoices(int numberOfChoices = 3)
    {
        ClearCards();

        abilityHolder holder = FindFirstObjectByType<abilityHolder>();

        if (holder == null)
        {
            Debug.LogError("No abilityHolder found in scene!");
            return;
        }

        List<upgradeBase> availableUpgrades = new List<upgradeBase>();

    foreach (upgradeBase upgrade in allUpgrades)
    {
        bool requirementsMet = true;

        if (upgrade.requiredUpgrades != null && upgrade.requiredUpgrades.Length > 0)
        {
            foreach (ability requiredAbility in upgrade.requiredUpgrades)
            {
                if (!holder.IsAbilityUnlocked(requiredAbility))
                {
                    requirementsMet = false;
                    break;
                }
            }
        }

        if (!requirementsMet)
            continue;

        abilityUnlockUpgrade abilityUpgrade = upgrade as abilityUnlockUpgrade;

        if (abilityUpgrade != null)
        {
            if (holder.IsAbilityUnlocked(abilityUpgrade.abilityToUnlock))
                continue;
        }

        availableUpgrades.Add(upgrade);
    }
    
    availableUpgrades.AddRange(repeatUpgrades);

    if(availableUpgrades.Count == 0)
    {
        Debug.LogWarning("No available upgrades to show!");
        return;
    }

    numberOfChoices = Mathf.Min(numberOfChoices, availableUpgrades.Count);

    for (int i = 0; i < numberOfChoices; i++)
    {
        int randomIndex = Random.Range(0, availableUpgrades.Count);
        upgradeBase chosenUpgrade = availableUpgrades[randomIndex];

        if(!repeatUpgrades.Contains(chosenUpgrade))
        {
            availableUpgrades.RemoveAt(randomIndex);
        }
        GameObject cardGO = Instantiate(UpgradeCardPrefab, CardsContainer);
        UpgradeCardUI cardUI = cardGO.GetComponent<UpgradeCardUI>();

        if (cardUI != null)
            cardUI.Setup(chosenUpgrade, levelUpManager);

        Debug.Log("Showing upgrade card: " + chosenUpgrade.upgradeName);
        }
    }



    public void ClearCards()
    {
        foreach (Transform child in CardsContainer)
        {
            Destroy(child.gameObject);
        }
    }
}
