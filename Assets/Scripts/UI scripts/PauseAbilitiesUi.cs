using UnityEngine;
using TMPro;

public class PauseAbilitiesUI : MonoBehaviour
{
    public Transform contentParent;
    public GameObject abilityItemPrefab;

    public TextMeshProUGUI nameText;
    public TextMeshProUGUI descriptionText;
    public TextMeshProUGUI stackText;

    abilityHolder holder;

    void OnEnable()
    {
        holder = FindFirstObjectByType<abilityHolder>();
        RefreshUI();
    }

    public void RefreshUI()
    {
        // Clear old
        foreach (Transform child in contentParent)
        {
            Destroy(child.gameObject);
        }

        if (holder == null) return;

        foreach (var pair in holder.unlockedAbilities)
        {
            ability abil = pair.Key;
            int stacks = pair.Value;

            GameObject obj = Instantiate(abilityItemPrefab, contentParent);
            UnlockUIItem item = obj.GetComponent<UnlockUIItem>();

            item.Setup(abil, stacks, this);
        }
    }

    public void ShowDescription(ability abil)
    {
        if (abil == null) return;

        nameText.text = abil.name;

        descriptionText.text = abil.description;

        int stacks = 0;
        if (holder.unlockedAbilities.ContainsKey(abil))
            stacks = holder.unlockedAbilities[abil];

        stackText.text = "X" + stacks;
    }
}
