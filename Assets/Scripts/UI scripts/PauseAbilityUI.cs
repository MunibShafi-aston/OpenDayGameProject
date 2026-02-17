using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PauseAbilityUI : MonoBehaviour
{
    [Header("Description")]
    [Header("Abilities")]
    [SerializeField] private Image[] abilityIcons;
    [SerializeField] private TMP_Text abilityNameText;
    [SerializeField] private TMP_Text abilityDescriptionText;

    private ability[] currentAbilities;

    public void LoadFromPlayer(PlayerStats stats)
    {
        if (stats == null) return;

        CharacterData data = stats.characterData;
        if (data == null) return;


        currentAbilities = new ability[]
        {
            stats.Ability1,
            stats.Ability2,
            stats.Ability3
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

        abilityNameText.text = "";
        abilityDescriptionText.text = "";
    }

    private void ShowAbility(int index)
    {
        ability ability = currentAbilities[index];
        abilityNameText.text = ability.name;
        abilityDescriptionText.text = ability.description;
    }
}
