using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UpgradeCardUI : MonoBehaviour
{
    public Image iconImage;
    public TMP_Text titleText;
    public TMP_Text descriptionText;
    public Button selectButton;

    public GameObject selectionHighlight;

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


        levelUpManager.SelectUpgrade(this);
    }

    public void ApplyUpgrade()
    {
        PlayerStats player = levelUpManager.playerStats;
        upgradeData.Apply(player);
    }

    public void SetSelected(bool selected)
    {
        if (selectionHighlight != null)
            selectionHighlight.SetActive(selected);
    }
}
