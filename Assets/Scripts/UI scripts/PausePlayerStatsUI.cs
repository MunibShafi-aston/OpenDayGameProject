using UnityEngine;
using TMPro;

public class PausePlayerStatsUI : MonoBehaviour
{
    public TMP_Text hp;
    public TMP_Text damage;
    public TMP_Text speed;
    public TMP_Text attackSpeed;
    public TMP_Text crit;
    public TMP_Text cdr;
    public TMP_Text defense;

    PlayerStats stats;

    void OnEnable()
    {
        stats = FindFirstObjectByType<PlayerStats>();
        Refresh();
    }

    public void Refresh()
    {
        if (stats == null) return;

        hp.text = "HP: " + Mathf.RoundToInt(stats.currentHealth) + " / " + Mathf.RoundToInt(stats.maxHealth);
        damage.text = "Damage: " + stats.damage;
        speed.text = "Move Speed: " + stats.moveSpeed;
        attackSpeed.text = "Attack Speed: " + stats.attackSpeed;
        crit.text = "Crit Chance: " + Mathf.RoundToInt(stats.critChance * 100) + "%";
        cdr.text = "Cooldown Reduction: " + Mathf.RoundToInt(stats.cooldownReduction * 100) + "%";
        defense.text = "Defense: " + stats.defense;
    }
}