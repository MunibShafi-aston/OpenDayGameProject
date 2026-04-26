using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UnlockUIItem : MonoBehaviour
{
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI stackText;
    public Button button;
    public Image iconImage;

    private ability abilityData;
    private PauseAbilitiesUI parentUI;

    public void Setup(ability abil, int stacks, PauseAbilitiesUI ui)
    {
        abilityData = abil;
        parentUI = ui;

        iconImage.sprite = abil.icon;
        nameText.text = abil.name;
        stackText.text = "x" + stacks;

        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(OnClicked);
    }

    void OnClicked()
    {
        parentUI.ShowDescription(abilityData);
    }
}
