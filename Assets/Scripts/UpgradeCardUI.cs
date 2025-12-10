using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UpgradeCardUI : MonoBehaviour
{
    public Image iconImage;
    public TMP_Text titleText;
    public TMP_Text descriptionText;
    public Button selectButton;

    private upgradeBase upgradeData;
    private LevelUpManager levelUpManager;

    public void Setup(upgradeBase upgrade, LevelUpManager manager)
    {
        upgradeData = upgrade;
        levelUpManager = manager;

        iconImage.sprite = upgrade.icon;
        titleText.text = upgrade.upgradeName;
        descriptionText.text = upgrade.description;

        selectButton.onClick.RemoveAllListeners();
        selectButton.onClick.AddListener(OnSelected);
    }

    void OnSelected()
    {
        PlayerStats player = levelUpManager.playerStats;
        upgradeData.Apply(player);

        levelUpManager.CloseLevelUpPanel();
    }
}
