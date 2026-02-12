using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CharacterDetailsUI : MonoBehaviour
{
    [Header("Description")]
    [SerializeField] private TMP_Text descriptionText;

    [Header("Abilities")]
    [SerializeField] private Image[] abilityIcons;
    [SerializeField] private TMP_Text abilityNameText;
    [SerializeField] private TMP_Text abilityDescriptionText;

    private ability[] currentAbilities;

    public void UpdateDetails(CharacterData data)
    {
        if (data == null)
        {
            Debug.LogError("CharacterData is NULL");
            return;
        }

        if (descriptionText == null)
        Debug.LogError("descriptionText is NULL");

        if (abilityIcons == null)
        Debug.LogError("abilityIcons array is NULL");

        if (abilityNameText == null)
        Debug.LogError("abilityNameText is NULL");

        if (abilityDescriptionText == null)
        Debug.LogError("abilityDescriptionText is NULL");


        descriptionText.text = data.characterDescription;

        currentAbilities = new ability[]
        {
            data.Ability1,
            data.Ability2,
            data.Ability3
        };

        for (int i = 0; i < abilityIcons.Length; i++)
        {
            Button button = abilityIcons[i].GetComponent<Button>();
            button.onClick.RemoveAllListeners();

            if (i < currentAbilities.Length && currentAbilities[i] != null)
            {
                abilityIcons[i].sprite = currentAbilities[i].icon;
                abilityIcons[i].gameObject.SetActive(true);

                int index = i;
                button.onClick.AddListener(() => ShowAbility(index));
            }
            else
            {
                abilityIcons[i].gameObject.SetActive(false);
            }
        }

        ClearAbilityText();
    }

    private void ShowAbility(int index)
    {
        ability ability = currentAbilities[index];
        abilityNameText.text = ability.name;
        abilityDescriptionText.text = ability.description;
    }

    private void ClearAbilityText()
    {
        abilityNameText.text = "";
        abilityDescriptionText.text = "";
    }
}