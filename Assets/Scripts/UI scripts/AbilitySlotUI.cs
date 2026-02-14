using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.InputSystem;

public class AbilitySlotUI : MonoBehaviour
{
    public int abilityIndex; // 0=Dash, 1=A1, 2=A2, 3=A3

    public Image icon;
    public Image cooldownOverlay;
    public TextMeshProUGUI cooldownText;
    public TextMeshProUGUI keyText;

    private abilityHolder holder;
    private PlayerInput input;
    private ability currentAbility;

    void Start()
    {
        holder = FindFirstObjectByType<abilityHolder>();

        if (holder == null) return;

        input = holder.GetComponent<PlayerInput>();

        holder.OnAbilitiesChanged += RefreshSlot;

        RefreshSlot();
    }

    void OnDestroy()
    {
        if (holder != null)
            holder.OnAbilitiesChanged -= RefreshSlot;
    }

    void RefreshSlot()
    {
        currentAbility = holder.GetAbilityByIndex(abilityIndex);

        if (currentAbility != null)
        {
            icon.sprite = currentAbility.icon;
            icon.enabled = true;
        }
        else
        {
            icon.enabled = false;
        }

        string actionName = GetActionName();
        keyText.text = input.actions[actionName]
            .GetBindingDisplayString();
    }

    void Update()
    {
        if (holder == null || currentAbility == null) return;

        float remaining = holder.GetCooldownRemaining(abilityIndex);
        float total = currentAbility.cooldownTime;

        if (remaining > 0 && total > 0)
        {
            cooldownOverlay.fillAmount = remaining / total;
            cooldownText.text = Mathf.Ceil(remaining).ToString();
        }
        else
        {
            cooldownOverlay.fillAmount = 0;
            cooldownText.text = "";
        }
    }

    string GetActionName()
    {
        switch (abilityIndex)
        {
            case 0: return "Dash";
            case 1: return "Ability1";
            case 2: return "Ability2";
            case 3: return "Ability3";
        }
        return "";
    }
}
