using UnityEngine;
using System.Collections.Generic;

public class EnemyStatus : MonoBehaviour
{
    private Enemy enemy;

    private class StatusEffect
    {
        public string type;
        public int stacks;
        public int maxStacks;
        public float duration;
        public float timer;
        public float damagePerSecond; //burn
        public bool frozen;           //freeze
    }

    private List<StatusEffect> activeEffects = new List<StatusEffect>();

    void Awake()
    {
        enemy = GetComponent<Enemy>();
    }

    void Update()
    {
        for (int i = activeEffects.Count - 1; i >= 0; i--)
        {
            var effect = activeEffects[i];
            effect.timer -= Time.deltaTime;

            if (effect.type == "Burn")
            {
                enemy.TankDamage(effect.damagePerSecond * effect.stacks * Time.deltaTime);
            }
            else if (effect.type == "Freeze" && effect.frozen)
            {
                // Freeze could reduce speed, for example
                var chase = GetComponent<enemyChase>();
                if (chase != null)
                    chase.SetMoveSpeed(0f);
            }

            if (effect.timer <= 0f)
            {
                if (effect.type == "Freeze")
                {
                    var chase = GetComponent<enemyChase>();
                    if (chase != null)
                        chase.SetMoveSpeed(enemy.enemyData.moveSpeed); // restore speed
                }

                activeEffects.RemoveAt(i);
            }
        }
    }

    public void ApplyBurn(float dps, float duration, int maxStacks)
    {
        var burn = activeEffects.Find(e => e.type == "Burn");
        if (burn == null)
        {
            burn = new StatusEffect() { type = "Burn", stacks = 1, maxStacks = maxStacks, damagePerSecond = dps, timer = duration };
            activeEffects.Add(burn);
        }
        else
        {
            burn.stacks = Mathf.Min(burn.stacks + 1, maxStacks);
            burn.timer = duration;
        }
    }

    public void ApplyFreeze(float chance, float duration)
    {
        if (Random.value > chance) return;

        var freeze = activeEffects.Find(e => e.type == "Freeze");
        if (freeze == null)
        {
            freeze = new StatusEffect() { type = "Freeze", frozen = true, timer = duration };
            activeEffects.Add(freeze);
        }
        else
        {
            freeze.timer = duration;
        }
    }
}
