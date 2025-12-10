using UnityEngine;
using System.Collections.Generic;

public class UpgradeManager : MonoBehaviour
{
    public GameObject UpgradeCardPrefab; 
    public Transform CardsContainer; 
    public List<upgradeBase> allUpgrades = new List<upgradeBase>();

    private LevelUpManager levelUpManager;

    public void Initialize(LevelUpManager manager)
    {
        levelUpManager = manager;
    }

 public void ShowUpgradeChoices(int numberOfChoices = 3)
{
    ClearCards();

    if (allUpgrades.Count == 0)
    {
        Debug.LogWarning("No upgrades in allUpgrades list!");
        return;
    }

    List<upgradeBase> availableUpgrades = new List<upgradeBase>(allUpgrades);

    numberOfChoices = Mathf.Min(numberOfChoices, availableUpgrades.Count);

    for (int i = 0; i < numberOfChoices; i++)
    {
        int randomIndex = Random.Range(0, availableUpgrades.Count);
        upgradeBase chosenUpgrade = availableUpgrades[randomIndex];

        availableUpgrades.RemoveAt(randomIndex);

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
